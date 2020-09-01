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
import ApiRequest from "Util/request";
import { MessageThread, SingleMessage } from "Models/application";
import SendMessageComponent from "Components/profile/sendMessage.vue";
const getMessageTitles = "/api/profile/getCustomerMessages";
const getMessageThread = "/api/profile/getMessageThread";
let MessagesComponent = class MessagesComponent extends Vue {
    constructor() {
        super();
        this.pageOptions = globalWindowObject.pageOptions;
        this.messageThread = new MessageThread("", "", [], "");
        this.messageTitles = [];
        this.companionId = "";
        this.apiRequest = new ApiRequest();
        setTimeout(() => this.reloadMessages(), 0);
    }
    addMessage(message) {
        let todayDate = new Date();
        let dateString = todayDate.toLocaleDateString();
        const newMessage = new SingleMessage("Я", dateString, message, true);
        this.messageThread.messages.push(newMessage);
    }
    reloadMessages() {
        return __awaiter(this, void 0, void 0, function* () {
            yield this.apiRequest.getData(`${getMessageTitles}?id=${this.pageOptions.currentCustomerId}`)
                .then((result) => {
                if (result.success) {
                    this.messageTitles = JSON.parse(result.value);
                }
                else {
                    console.error("Произошла ошибка на сервере, не удалось получить список сообщений");
                }
            });
        });
    }
    load(id) {
        return __awaiter(this, void 0, void 0, function* () {
            yield this.apiRequest.getData(`${getMessageThread}?customerId=${this.pageOptions.currentCustomerId}&parentMessageId=${id}`)
                .then((result) => {
                if (result.success) {
                    this.messageThread = JSON.parse(result.value);
                }
                else {
                    console.error("Произошла ошибка на сервере, не удалось ветку сообщений");
                }
            });
        });
    }
};
MessagesComponent = __decorate([
    Component({
        components: {
            sendMessage: SendMessageComponent
        }
    })
], MessagesComponent);
export default MessagesComponent;
//# sourceMappingURL=messages.js.map