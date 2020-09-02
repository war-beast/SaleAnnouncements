var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
import { Vue, Component, Prop } from "vue-property-decorator";
import ApiRequest from "Util/request";
const phoneLoadUrl = "/api/common/getOfferPhoneNumber";
let PhoneLink = class PhoneLink extends Vue {
    constructor() {
        super();
        this.anchor = "показать";
        this.isLoaded = false;
        this.apiRequest = new ApiRequest();
    }
    showPhoneNumber() {
        return __awaiter(this, void 0, void 0, function* () {
            if (this.isLoaded)
                return;
            yield this.apiRequest.getData(`${phoneLoadUrl}?id=${this.id}`)
                .then((result) => {
                if (result.success) {
                    this.anchor = result.value;
                    this.isLoaded = true;
                }
                else {
                    console.error(`Произошла ошибка на сервере, не удалось получить номер телефона id=${this.id}`);
                }
            });
        });
    }
};
__decorate([
    Prop()
], PhoneLink.prototype, "id", void 0);
PhoneLink = __decorate([
    Component
], PhoneLink);
export default PhoneLink;
//# sourceMappingURL=phoneLinkComponent.js.map