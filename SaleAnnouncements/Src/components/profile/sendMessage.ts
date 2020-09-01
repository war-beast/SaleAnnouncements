import { Vue, Component, Prop } from "vue-property-decorator";
import ApiRequest from "Util/request";
import { ApiResult } from "Models/apiResult";

const sendMessageUrl = "/api/profile/sendMessageReply";

@Component
export default class SendMessageComponent extends Vue {
	@Prop()
	private customerId: string;
	@Prop()
	private companionId: string;
	@Prop()
	private parentMessageId: string;
	@Prop()
	private addMessage: Function;

	private readonly apiRequest: ApiRequest;
	private message: string = "";
	private sendError: string = "";

	constructor() {
		super();

		this.apiRequest = new ApiRequest();
	}

	private async send() {
		this.sendError = "";

		if (this.message === "") {
			this.sendError = "Заполните текст сообщения";
			return;
		}

		const data = {
			parentMessageId: this.parentMessageId,
			currentCustomerId: this.customerId,
			companionId: this.companionId,
			message: this.message
		}

		await this.apiRequest.postData(sendMessageUrl, JSON.stringify(data))
			.then((result: ApiResult) => {
				if (result.success) {
					this.addMessage(this.message);
					this.message = "";
				} else {
					this.sendError = "Произошла ошибка на сервере, не удалось отправить сообщение";
				}
			});
	}
}