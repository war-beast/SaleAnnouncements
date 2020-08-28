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
import { bus } from "Util/bus";
const statusesUrl = "/api/common/getStatuses";
let StatusSelectorComponent = class StatusSelectorComponent extends Vue {
    constructor() {
        super();
        this.statuses = [];
        this.paidStatus = [];
        this.apiRequest = new ApiRequest();
        setTimeout(() => {
            this.getAvailableStatuses();
        }, 0);
    }
    created() {
        bus.$on("clearStatusSelections", this.statusSelected);
    }
    statusSelected() {
        this.paidStatus = [];
    }
    getAvailableStatuses() {
        return __awaiter(this, void 0, void 0, function* () {
            yield this.apiRequest.getData(statusesUrl)
                .then((result) => {
                if (result.success) {
                    this.statuses = JSON.parse(result.value);
                }
                else {
                    console.log(`Ошибка загрузки данных по url: ${statusesUrl}`);
                }
            });
        });
    }
    totalAmount() {
        var selectedStatuses = this.getSelectedStatuses();
        return selectedStatuses.reduce((sum, item) => sum + item.price, 0);
    }
    getSelectedStatuses() {
        return this.statuses.filter((item) => {
            return this.paidStatus.indexOf(item.id) !== -1;
        });
    }
    selectionChanged() {
        bus.$emit("statusSelected", this.paidStatus);
    }
};
StatusSelectorComponent = __decorate([
    Component
], StatusSelectorComponent);
export default StatusSelectorComponent;
//# sourceMappingURL=statusSelector.js.map