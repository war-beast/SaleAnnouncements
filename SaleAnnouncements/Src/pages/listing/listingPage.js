import Vue from "vue";
import BasePage from "Pages/basePage";
import PhoneLink from "Components/listing/phoneLinkComponent.vue";
import PhotoItemComponent from "Components/listing/photoItemComponent.vue";
import PhotoModalComponent from "Components/listing/photoModal.vue";
import OfferMessageButtonComponent from "Components/listing/offerMessageButtonComponent.vue";
export default class ListingPage extends BasePage {
    constructor() {
        super();
        this.init();
    }
    init() {
        this.instance = new Vue({
            el: "#vue-app-container",
            store: this.store,
            components: {
                "phone-link": PhoneLink,
                "photo-item": PhotoItemComponent,
                "photo-modal": PhotoModalComponent,
                "message-button": OfferMessageButtonComponent
            }
        });
    }
}
//# sourceMappingURL=listingPage.js.map