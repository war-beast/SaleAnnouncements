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
import StatusSelectorComponent from "Components/profile/statusSelector.vue";
import { bus } from "Util/bus";
const addStatusesUrl = "/api/profile/addStatus";
const getStatusesUrl = "/api/common/getOfferStatuses";
let AddStatusComponent = class AddStatusComponent extends Vue {
    constructor() {
        super();
        this.offerPageOptions = globalWindowObject.pageOptions;
        this.successMessage = "";
        this.addingError = "";
        this.addingInProcess = false;
        this.formValid = true;
        this.offerExistingStatuses = [];
        this.paidStatus = [];
        this.apiRequest = new ApiRequest();
        setTimeout(() => this.reloadStatuses(), 0);
    }
    created() {
        bus.$on("statusSelected", this.statusSelected);
    }
    checkValidation() {
        return this.paidStatus.length !== 0;
    }
    statusSelected(value) {
        this.paidStatus = value;
        this.addingError = "";
    }
    submit() {
        this.formValid = this.checkValidation();
        this.addingInProcess = false;
        this.addingError = "";
        this.successMessage = "";
        if (!this.formValid) {
            this.addingError = "Выберите хотя бы 1 статус";
            return;
        }
        this.sendRequest();
    }
    sendRequest() {
        return __awaiter(this, void 0, void 0, function* () {
            this.addingInProcess = true;
            var data = {
                id: this.offerPageOptions.offerId,
                selectedStatusIds: this.paidStatus
            };
            yield this.apiRequest.postData(addStatusesUrl, JSON.stringify(data))
                .then((result) => {
                if (result.success) {
                    this.paidStatus = [];
                    this.successMessage = "Статусы добавлены к объявлению";
                    bus.$emit("clearStatusSelections");
                    this.reloadStatuses();
                }
                else {
                    this.addingError = "Произошла ошибка на сервере, новые статусы не добавлены";
                }
                this.addingInProcess = false;
            });
        });
    }
    reloadStatuses() {
        return __awaiter(this, void 0, void 0, function* () {
            yield this.apiRequest.getData(`${getStatusesUrl}?id=${this.offerPageOptions.offerId}`)
                .then((result) => {
                if (result.success) {
                    this.offerExistingStatuses = JSON.parse(result.value);
                }
                else {
                    console.error("Произошла ошибка на сервере, не удалось получить статусы для объявления");
                }
            });
        });
    }
};
AddStatusComponent = __decorate([
    Component({
        components: {
            statusSelectorComponent: StatusSelectorComponent
        }
    })
], AddStatusComponent);
export default AddStatusComponent;
//# sourceMappingURL=addStatus.js.map