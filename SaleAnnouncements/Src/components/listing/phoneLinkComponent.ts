import { Vue, Component, Prop } from "vue-property-decorator";
import ApiRequest from "Util/request";
import { ApiResult } from "Models/apiResult";

const phoneLoadUrl = "/api/common/getOfferPhoneNumber";

@Component
export default class PhoneLink extends Vue {
	@Prop()
	private id: string;

	private anchor: string = "показать";
	private isLoaded: boolean = false;

	private readonly apiRequest: ApiRequest;

	constructor() {
		super();
		this.apiRequest = new ApiRequest();
	}

	private async showPhoneNumber() {
		if (this.isLoaded)
			return;

		await this.apiRequest.getData(`${phoneLoadUrl}?id=${this.id}`)
			.then((result: ApiResult) => {
				if (result.success) {
					this.anchor = result.value;
					this.isLoaded = true;
				} else {
					console.error(`Произошла ошибка на сервере, не удалось получить номер телефона id=${this.id}`);
				}
			});
	}
}