import Vue from "vue";
import Component from "vue-class-component";
import ApiRequest from "Util/request";
import { ApiResult, ServerOperationResult } from "Models/apiResult";
import { OfferModel, Category } from "Models/application";
import StatusSelectorComponent from "Components/profile/statusSelector.vue";
import { bus } from "Util/bus";

const createOfferUrl = "/api/profile/addOffer";
const categoriesUrl = "/api/common/getCategories";

@Component({
	components: {
		statusSelectorComponent: StatusSelectorComponent
	}
})
export default class NewOfferComponent extends Vue {
	$refs!: {
		files: HTMLInputElement
	}

	private readonly apiRequest: ApiRequest;
	private offer: OfferModel;
	private formValid: boolean = true;
	private photos: Array<File> = [];
	private creationError: string = "";
	private creationInProcess: boolean = false;
	private successMessage: string = "";
	private categories: Array<Category> = [];

	private selectedCategoryId: string | null = null;
	private paidStatus: Array<string> = [];

	constructor() {
		super();

		this.offer = new OfferModel("", "", 0, "");
		this.apiRequest = new ApiRequest();

		setTimeout(() => {
			this.getAvailableCategories();
		}, 0);
	}

	public created() {
		bus.$on("statusSelected", this.statusSelected);
	}

	private statusSelected(value: Array<string>) {
		this.paidStatus = value;
	}

	private addFiles() {
		this.$refs.files.click();
	}

	private handleFilesUpload() {
		const uploaded = this.$refs.files.files;
		for (let i = 0; i < uploaded.length; i++) {
			this.photos.push(uploaded[i]);
		}
	}

	private removeFile(key: number) {
		this.photos.splice(key, 1);
	}

	private checkValidation(): boolean {
		return this.offer.title !== ""
			&& this.offer.description !== ""
			&& this.selectedCategoryId !== null
			&& this.offer.phoneNumber !== ""
			&& this.photos.length > 0;
	}

	private async submit() {
		this.formValid = this.checkValidation();

		if (!this.formValid) {
			this.creationError = "Проверьте правильность заполнени формы";
			this.creationInProcess = false;
			return;
		}

		this.sendOffer(this.buildFormData());
	}

	private buildFormData(): FormData {
		let formData = new FormData();

		for (let i = 0; i < this.photos.length; i++) {
			formData.append(`photos`, this.photos[i]);
		}

		for (let i = 0; i < this.paidStatus.length; i++) {
			formData.append(`selectedStatusIds`, this.paidStatus[i]);
		}

		this.offer.setPhotoFiles(this.photos);
		formData.append(`title`, this.offer.title);
		formData.append(`description`, this.offer.description);
		formData.append(`categoryId`, this.selectedCategoryId);
		formData.append(`price`, this.offer.price.toString().replace(".", ","));
		formData.append(`phoneNumber`, this.offer.phoneNumber);

		return formData;
	}

	private async sendOffer(formData: FormData) {
		this.creationError = "";
		this.successMessage = "";
		this.creationInProcess = true;

		await this.apiRequest.postMultipartData(createOfferUrl, formData)
			.then((result: ApiResult) => {
				if (result.success) {
					const resultData = result.value as ServerOperationResult;

					if (resultData.isSuccess) {
						this.successMessage = `Объявление "${this.offer.title}" сохранено успешно!`;
						this.offer = new OfferModel("", "", 0, "");
						this.photos = [];
						this.paidStatus = [];
						bus.$emit("clearStatusSelections");
					} else {
						this.creationError = resultData.error;
					}
				} else {
					this.creationError = `На сервере произошла ошибка создания объявления: "${this.offer.title}"`;
				}

				this.creationInProcess = false;
			});
	}

	private async getAvailableCategories() {
		await this.apiRequest.getData(categoriesUrl)
			.then((result: ApiResult) => {
				if (result.success) {
					this.categories = JSON.parse(result.value);
				} else {
					console.log(`Ошибка загрузки данных по url: ${categoriesUrl}`);
				}
			});
	}
}