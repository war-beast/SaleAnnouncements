import { Vue, Component } from "vue-property-decorator";
import ApiRequest from "Util/request";
import { OfferModel } from "Models/offer";
import { ApiResult } from "Models/apiResult";

const registerUrl = "/api/profile/addOffer";

@Component
export default class NewOfferComponent extends Vue {
	private offer: OfferModel;
	private createError: string = "";
	private photos: string = "";
	private formValid: boolean = true;

	constructor() {
		super();

		this.offer = new OfferModel("", "", 0, "");
	}

	private handleFilesUpload() {
		this.photos = "";
	}
}