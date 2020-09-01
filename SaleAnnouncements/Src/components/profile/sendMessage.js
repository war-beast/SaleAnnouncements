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
const sendMessageUrl = "/api/profile/sendMessageReply";
let SendMessageComponent = class SendMessageComponent extends Vue {
    constructor() {
        super();
        this.message = "";
        this.sendError = "";
        this.apiRequest = new ApiRequest();
    }
    send() {
        return __awaiter(this, void 0, void 0, function* () {
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
            };
            yield this.apiRequest.postData(sendMessageUrl, JSON.stringify(data))
                .then((result) => {
                if (result.success) {
                    this.addMessage(this.message);
                    this.message = "";
                }
                else {
                    this.sendError = "Произошла ошибка на сервере, не удалось отправить сообщение";
                }
            });
        });
    }
};
__decorate([
    Prop()
], SendMessageComponent.prototype, "customerId", void 0);
__decorate([
    Prop()
], SendMessageComponent.prototype, "companionId", void 0);
__decorate([
    Prop()
], SendMessageComponent.prototype, "parentMessageId", void 0);
__decorate([
    Prop()
], SendMessageComponent.prototype, "addMessage", void 0);
SendMessageComponent = __decorate([
    Component
], SendMessageComponent);
export default SendMessageComponent;
//# sourceMappingURL=sendMessage.js.map