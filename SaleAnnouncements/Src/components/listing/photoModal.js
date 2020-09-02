var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Vue, Component } from "vue-property-decorator";
import { bus } from "Util/bus";
let PhotoModalComponent = class PhotoModalComponent extends Vue {
    constructor() {
        super(...arguments);
        this.imageBytes = "";
        this.isInfoVisible = false;
    }
    created() {
        bus.$on("zoomPhoto", this.photoSelected);
    }
    photoSelected(imageBytes) {
        this.imageBytes = imageBytes;
        this.isInfoVisible = true;
    }
    hideModal() {
        this.isInfoVisible = false;
        this.imageBytes = "";
    }
};
PhotoModalComponent = __decorate([
    Component
], PhotoModalComponent);
export default PhotoModalComponent;
;
//# sourceMappingURL=photoModal.js.map