import { Vue, Component, Prop } from "vue-property-decorator";
import ApiRequest from "Util/request";
import { ApiResult } from "Models/apiResult";

@Component
export default class MessagesComponent extends Vue {
	private readonly apiRequest: ApiRequest;

	constructor() {
		super();

		this.apiRequest = new ApiRequest();
		setTimeout(() => this.reloadMessages(), 0);
	}

	private async reloadMessages() {

	}
}