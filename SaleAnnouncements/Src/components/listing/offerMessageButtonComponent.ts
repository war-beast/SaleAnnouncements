import { Vue, Component, Prop } from "vue-property-decorator";
import { bus } from "Util/bus";
import { OfferMessage } from "Models/application";

@Component
export default class OfferMessageButtonComponent extends Vue {
	@Prop()
	private offerOwnerId: string;
	@Prop()
	private offerId: string;
	@Prop()
	private currentCustomerId: string;

	private showModal() {
		if (!this.isModelValid)
			return;

		let messageBuilder = OfferMessage.createBuilder()
			.setCustomerId(this.currentCustomerId)
			.setOfferId(this.offerId)
			.setOfferOwnerId(this.offerOwnerId);

		bus.$emit("showMessageModal", messageBuilder);
	}

	private isModelValid(): boolean {
		return this.offerId !== ""
			&& this.currentCustomerId !== ""
			&& this.offerOwnerId !== "";
	}
}