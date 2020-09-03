var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Vue, Component, Prop } from "vue-property-decorator";
import { bus } from "Util/bus";
import { OfferMessage } from "Models/application";
let OfferMessageButtonComponent = class OfferMessageButtonComponent extends Vue {
    showModal() {
        if (!this.isModelValid)
            return;
        let messageBuilder = OfferMessage.createBuilder()
            .setCustomerId(this.currentCustomerId)
            .setOfferId(this.offerId)
            .setOfferOwnerId(this.offerOwnerId);
        bus.$emit("showMessageModal", messageBuilder);
    }
    isModelValid() {
        return this.offerId !== ""
            && this.currentCustomerId !== ""
            && this.offerOwnerId !== "";
    }
};
__decorate([
    Prop()
], OfferMessageButtonComponent.prototype, "offerOwnerId", void 0);
__decorate([
    Prop()
], OfferMessageButtonComponent.prototype, "offerId", void 0);
__decorate([
    Prop()
], OfferMessageButtonComponent.prototype, "currentCustomerId", void 0);
OfferMessageButtonComponent = __decorate([
    Component
], OfferMessageButtonComponent);
export default OfferMessageButtonComponent;
//# sourceMappingURL=offerMessageButtonComponent.js.map