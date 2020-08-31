﻿import { Vue, Component, Prop } from "vue-property-decorator";
import ApiRequest from "Util/request";
import { ApiResult } from "Models/apiResult";
import { MessageTitle as MessageAuthor, SingleMessage, MessageHostOffer, MessagesPageOptions } from "Models/application";

const getMessageTitles = "/api/profile/getCustomerMessages";
const getMessageThread = "/api/profile/getMessageThread";

@Component
export default class MessagesComponent extends Vue {
	private pageOptions: MessagesPageOptions = globalWindowObject.pageOptions;

	private readonly apiRequest: ApiRequest;
	private selectedTopicMessages: Array<SingleMessage> = [];
	private messageHostOffer: MessageHostOffer = new MessageHostOffer("", "");
	private messageTitles: Array<MessageAuthor> = [];

	constructor() {
		super();

		this.apiRequest = new ApiRequest();
		setTimeout(() => this.reloadMessages(), 0);
	}

	private async reloadMessages() {
		await this.apiRequest.getData(`${getMessageTitles}?id=${this.pageOptions.currentCustomerId}`)
			.then((result: ApiResult) => {
				if (result.success) {
					this.messageTitles = JSON.parse(result.value);
				} else {
					console.error("Произошла ошибка на сервере, не удалось получить список сообщений");
				}
			});
	}

	private async load(id: string) {
		await this.apiRequest.getData(`${getMessageThread}?id=${this.pageOptions.currentCustomerId}`)
			.then((result: ApiResult) => {
				if (result.success) {
					this.selectedTopicMessages = JSON.parse(result.value);
				} else {
					console.error("Произошла ошибка на сервере, не удалось ветку сообщений");
				}
			});
	}
}