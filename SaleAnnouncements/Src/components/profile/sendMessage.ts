import { Vue, Component, Prop } from "vue-property-decorator";
import ApiRequest from "Util/request";
import { ApiResult } from "Models/apiResult";

const sendMessageUrl = "/api/profile/sendMessage";

@Component
export class SendMessageComponent extends Vue {
	@Prop()
	private customerId: string;
	@Prop()
	private companionId: string;
	@Prop()
	private parentMessageId: string;

	private readonly apiRequest: ApiRequest;
	private message: string;
	private sendError: string = "";
	private sendSuccess: string = "";

	constructor() {
		super();

		this.apiRequest = new ApiRequest();
	}

	private async send() {
		this.sendError = "";
		this.sendSuccess = "";

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
					this.sendSuccess = "Сообщение отправлено успешно";
					this.message = "";
				} else {
					this.sendError = "Произошла ошибка на сервере, не удалось отправить сообщение";
				}
			});
	}
}