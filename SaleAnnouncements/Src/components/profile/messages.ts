import { Vue, Component } from "vue-property-decorator";
import ApiRequest from "Util/request";
import { ApiResult } from "Models/apiResult";
import { MessageTitle, MessageThread, MessagesPageOptions, SingleMessage } from "Models/application";
import SendMessageComponent from "Components/profile/sendMessage.vue";

const getMessageTitles = "/api/profile/getCustomerMessages";
const getMessageThread = "/api/profile/getMessageThread";

@Component({
	components: {
		sendMessage: SendMessageComponent
	}
})
export default class MessagesComponent extends Vue {
	private pageOptions: MessagesPageOptions = globalWindowObject.pageOptions;

	private readonly apiRequest: ApiRequest;
	private messageThread: MessageThread = new MessageThread("", "", [], "");
	private messageTitles: Array<MessageTitle> = [];
	private companionId: string = "";

	constructor() {
		super();

		this.apiRequest = new ApiRequest();
		setTimeout(() => this.reloadMessages(), 0);
	}

	public addMessage(message: string) {
		let todayDate = new Date();
		let dateString = todayDate.toLocaleDateString();
		const newMessage = new SingleMessage("Я", dateString, message, true);
		this.messageThread.messages.push(newMessage);
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
		await this.apiRequest.getData(`${getMessageThread}?customerId=${this.pageOptions.currentCustomerId}&parentMessageId=${id}`)
			.then((result: ApiResult) => {
				if (result.success) {
					this.messageThread = JSON.parse(result.value);
				} else {
					console.error("Произошла ошибка на сервере, не удалось ветку сообщений");
				}
			});
	}
}