import { Vue, Component } from "vue-property-decorator";
import ApiRequest from "Util/request";
import { ApiResult, ServerOperationResult } from "Models/apiResult";
import { OfferModel, Category, Status } from "Models/application";

const createOfferUrl = "/api/profile/addOffer";
const categoriesUrl = "/api/common/getCategories";
const statusesUrl = "/api/common/getStatuses";

@Component
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
	private statuses: Array<Status> = [];

	private selectedCategoryId: string | null = null;

	constructor() {
		super();

		this.offer = new OfferModel("", "", 0, "");
		this.apiRequest = new ApiRequest();

		setTimeout(() => {
			this.getAvailableCategories();
			this.getAvailableStatuses();
		}, 0);
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
		this.creationError = "";
		this.successMessage = "";
		this.formValid = this.checkValidation();

		if (!this.formValid) {
			this.creationError = "Проверьте правильность заполнени формы";
			this.creationInProcess = false;
			return;
		}

		this.creationInProcess = true;
		let formData = new FormData();

		for (let i = 0; i < this.photos.length; i++) {
			formData.append(`photos`, this.photos[i]);
		}

		this.offer.setPhotoFiles(this.photos);
		formData.append(`title`, this.offer.title);
		formData.append(`description`, this.offer.description);
		formData.append(`categoryId`, this.selectedCategoryId);
		formData.append(`phoneNumber`, this.offer.phoneNumber);
		formData.append(`price`, this.offer.price.toString().replace(".", ","));

		await this.apiRequest.postMultipartData(createOfferUrl, formData)
			.then((result: ApiResult) => {
				if (result.success) {
					const resultData = result.value as ServerOperationResult;

					if (resultData.isSuccess) {
						this.successMessage = `Объявление "${this.offer.title}" сохранено успешно!`;
						this.offer = new OfferModel("", "", 0, "");
						this.photos = [];
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

	private async getAvailableStatuses() {
		await this.apiRequest.getData(statusesUrl)
			.then((result: ApiResult) => {
				if (result.success) {
					this.statuses = JSON.parse(result.value);
				} else {
					console.log(`Ошибка загрузки данных по url: ${statusesUrl}`);
				}
			});
	}
}