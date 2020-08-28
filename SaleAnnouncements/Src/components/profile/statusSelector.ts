import { Vue, Component } from "vue-property-decorator";
import { Status } from "Models/application";
import ApiRequest from "Util/request";
import { bus } from "Util/bus";
import { ApiResult } from "Models/apiResult";

const statusesUrl = "/api/common/getStatuses";

@Component
export default class StatusSelectorComponent extends Vue {
	private readonly apiRequest: ApiRequest;
	private statuses: Array<Status> = [];
	private paidStatus: Array<string> = [];

	constructor() {
		super();
		this.apiRequest = new ApiRequest();

		setTimeout(() => {
			this.getAvailableStatuses();
		}, 0);
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

	private totalAmount(): number {
		var selectedStatuses = this.getSelectedStatuses();

		return selectedStatuses.reduce((sum, item) => sum + item.price, 0);
	}

	private getSelectedStatuses(): Array<Status> {
		return this.statuses.filter((item) => {
			return this.paidStatus.indexOf(item.id) !== -1;
		});
	}

	private selectionChanged() {
		bus.$emit("statusSelected", this.paidStatus);
	}
}