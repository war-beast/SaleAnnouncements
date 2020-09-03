import { Vue, Component } from "vue-property-decorator";
import { bus } from "Util/bus";
import ApiRequest from "Util/request";
import { ApiResult } from "Models/apiResult";
import { OfferMessageBuilder } from "Models/application";

const sendMessageUrl = "/api/common/saveMessage";

@Component
export default class MessageModal extends Vue {
	private isInfoVisible: boolean = false;
	private message: string = "";
	private submittingError: string = "";
	private submittingSuccess: string = "";
	private readonly apiRequest: ApiRequest;
	private messageBuilder: OfferMessageBuilder;
	private sendingProcess: boolean = false;

	constructor() {
		super();

		this.apiRequest = new ApiRequest();
	}

	public created() {
		bus.$on("showMessageModal", this.showModal);
	}

	private showModal(value: OfferMessageBuilder) {
		this.messageBuilder = value;
		this.isInfoVisible = true;
	}

	private hideModal() {
		this.isInfoVisible = false;
		this.message = "";
		this.submittingError = "";
		this.submittingSuccess = "";
	}

	private async sendMessage() {
		if (this.message === "") {
			this.submittingError = "Введите текст сообщения";
			return;
		}

		this.sendingProcess = true;
		this.submittingError = "";
		this.submittingSuccess = "";
		const data = this.messageBuilder
			.setMessage(this.message)
			.build();

		await this.apiRequest.postData(sendMessageUrl, JSON.stringify(data))
			.then((result: ApiResult) => {
				if (result.success) {
					this.message = "";
					this.submittingSuccess = "Сообщение отправлено успешно!";
				} else {
					this.submittingError = "Произошла ошибка на сервере, не удалось отправить сообщение";
				}

				this.sendingProcess = false;
			});
	}
}