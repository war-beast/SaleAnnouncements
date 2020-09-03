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
import { Vue, Component } from "vue-property-decorator";
import { bus } from "Util/bus";
import ApiRequest from "Util/request";
const sendMessageUrl = "/api/common/saveMessage";
let MessageModal = class MessageModal extends Vue {
    constructor() {
        super();
        this.isInfoVisible = false;
        this.message = "";
        this.submittingError = "";
        this.submittingSuccess = "";
        this.sendingProcess = false;
        this.apiRequest = new ApiRequest();
    }
    created() {
        bus.$on("showMessageModal", this.showModal);
    }
    showModal(value) {
        this.messageBuilder = value;
        this.isInfoVisible = true;
    }
    hideModal() {
        this.isInfoVisible = false;
        this.message = "";
        this.submittingError = "";
        this.submittingSuccess = "";
    }
    sendMessage() {
        return __awaiter(this, void 0, void 0, function* () {
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
            yield this.apiRequest.postData(sendMessageUrl, JSON.stringify(data))
                .then((result) => {
                if (result.success) {
                    this.message = "";
                    this.submittingSuccess = "Сообщение отправлено успешно!";
                }
                else {
                    this.submittingError = "Произошла ошибка на сервере, не удалось отправить сообщение";
                }
                this.sendingProcess = false;
            });
        });
    }
};
MessageModal = __decorate([
    Component
], MessageModal);
export default MessageModal;
//# sourceMappingURL=messageModal.js.map