var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Vue, Component } from "vue-property-decorator";
import { OfferModel } from "Models/offer";
const registerUrl = "/api/profile/addOffer";
let NewOfferComponent = class NewOfferComponent extends Vue {
    constructor() {
        super();
        this.createError = "";
        this.photos = "";
        this.formValid = true;
        this.offer = new OfferModel("", "", 0, "");
    }
    handleFilesUpload() {
        this.photos = "";
    }
};
NewOfferComponent = __decorate([
    Component
], NewOfferComponent);
export default NewOfferComponent;
//# sourceMappingURL=newOffer.js.map