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
import { OfferModel } from "Models/application";
const createOfferUrl = "/api/profile/addOffer";
const categoriesUrl = "/api/common/getCategories";
const statusesUrl = "/api/common/getStatuses";
let NewOfferComponent = class NewOfferComponent extends Vue {
    constructor() {
        super();
        this.formValid = true;
        this.photos = [];
        this.creationError = "";
        this.creationInProcess = false;
        this.successMessage = "";
        this.categories = [];
        this.statuses = [];
        this.selectedCategoryId = null;
        this.offer = new OfferModel("", "", 0, "");
        this.apiRequest = new ApiRequest();
        setTimeout(() => {
            this.getAvailableCategories();
            this.getAvailableStatuses();
        }, 0);
    }
    addFiles() {
        this.$refs.files.click();
    }
    handleFilesUpload() {
        const uploaded = this.$refs.files.files;
        for (let i = 0; i < uploaded.length; i++) {
            this.photos.push(uploaded[i]);
        }
    }
    removeFile(key) {
        this.photos.splice(key, 1);
    }
    checkValidation() {
        return this.offer.title !== ""
            && this.offer.description !== ""
            && this.selectedCategoryId !== null
            && this.offer.phoneNumber !== ""
            && this.photos.length > 0;
    }
    submit() {
        return __awaiter(this, void 0, void 0, function* () {
            this.creationError = "";
            this.successMessage = "";
            this.formValid = this.checkValidation();
            if (!this.formValid) {
                this.creationError = "Проверьте правильность заполнени формы";
                this.creationInProcess = false;
                return;
            }
            this.creationInProcess = true;
            let formData = new FormData();
            for (let i = 0; i < this.photos.length; i++) {
                formData.append(`photos`, this.photos[i]);
            }
            this.offer.setPhotoFiles(this.photos);
            formData.append(`title`, this.offer.title);
            formData.append(`description`, this.offer.description);
            formData.append(`categoryId`, this.selectedCategoryId);
            formData.append(`phoneNumber`, this.offer.phoneNumber);
            formData.append(`price`, this.offer.price.toString().replace(".", ","));
            yield this.apiRequest.postMultipartData(createOfferUrl, formData)
                .then((result) => {
                if (result.success) {
                    const resultData = result.value;
                    if (resultData.isSuccess) {
                        this.successMessage = `Объявление "${this.offer.title}" сохранено успешно!`;
                        this.offer = new OfferModel("", "", 0, "");
                        this.photos = [];
                    }
                    else {
                        this.creationError = resultData.error;
                    }
                }
                else {
                    this.creationError = `На сервере произошла ошибка создания объявления: "${this.offer.title}"`;
                }
                this.creationInProcess = false;
            });
        });
    }
    getAvailableCategories() {
        return __awaiter(this, void 0, void 0, function* () {
            yield this.apiRequest.getData(categoriesUrl)
                .then((result) => {
                if (result.success) {
                    this.categories = JSON.parse(result.value);
                }
                else {
                    console.log(`Ошибка загрузки данных по url: ${categoriesUrl}`);
                }
            });
        });
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
};
NewOfferComponent = __decorate([
    Component
], NewOfferComponent);
export default NewOfferComponent;
//# sourceMappingURL=newOffer.js.map