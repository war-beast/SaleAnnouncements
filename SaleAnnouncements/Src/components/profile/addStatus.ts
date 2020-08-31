import { Vue, Component, Prop } from "vue-property-decorator";
import ApiRequest from "Util/request";
import { ApiResult } from "Models/apiResult";
import StatusSelectorComponent from "Components/profile/statusSelector.vue";
import { bus } from "Util/bus";
import { Status, OfferPageOptions } from "Models/application";

const addStatusesUrl = "/api/profile/addStatus";
const getStatusesUrl = "/api/common/getOfferStatuses";

@Component({
	components: {
		statusSelectorComponent: StatusSelectorComponent
	}
})
export default class AddStatusComponent extends Vue {
	private offerPageOptions: OfferPageOptions = globalWindowObject.pageOptions;

	private readonly apiRequest: ApiRequest;
	private successMessage: string = "";
	private addingError: string = "";
	private addingInProcess: boolean = false;
	private formValid: boolean = true;
	private offerExistingStatuses: Array<Status> = [];

	private paidStatus: Array<string> = [];

	constructor() {
		super();

		this.apiRequest = new ApiRequest();
		setTimeout(() => this.reloadStatuses(), 0);
	}

	public created() {
		bus.$on("statusSelected", this.statusSelected);
	}

	private checkValidation(): boolean {
		return this.paidStatus.length !== 0;
	}

	private statusSelected(value: Array<string>) {
		this.paidStatus = value;
		this.addingError = "";
	}

	private submit() {
		this.formValid = this.checkValidation();
		this.addingInProcess = false;
		this.addingError = "";
		this.successMessage = "";

		if (!this.formValid) {
			this.addingError = "Выберите хотя бы 1 статус";
			return;
		}

		this.sendRequest();
	}

	private async sendRequest() {
		this.addingInProcess = true;
		var data = {
			id: this.offerPageOptions.offerId,
			selectedStatusIds: this.paidStatus
		}

		await this.apiRequest.postData(addStatusesUrl, JSON.stringify(data))
			.then((result: ApiResult) => {
				if (result.success) {
					this.paidStatus = [];
					this.successMessage = "Статусы добавлены к объявлению";
					bus.$emit("clearStatusSelections");

					this.reloadStatuses();
				} else {
					this.addingError = "Произошла ошибка на сервере, новые статусы не добавлены";
				}

				this.addingInProcess = false;
			});
	}

	private async reloadStatuses() {
		await this.apiRequest.getData(`${getStatusesUrl}?id=${this.offerPageOptions.offerId}`)
			.then((result: ApiResult) => {
				if (result.success) {
						this.offerExistingStatuses = JSON.parse(result.value);
				} else {
					console.error("Произошла ошибка на сервере, не удалось получить статусы для объявления");
				}
			});
	}
}