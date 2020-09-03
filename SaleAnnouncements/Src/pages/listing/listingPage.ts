﻿import Vue from "vue";
import BasePage from "Pages/basePage";
import PhoneLink from "Components/listing/phoneLinkComponent.vue";
import PhotoItemComponent from "Components/listing/photoItemComponent.vue";
import PhotoModalComponent from "Components/listing/photoModal.vue";
import OfferMessageButtonComponent from "Components/listing/offerMessageButtonComponent.vue";
import MessageModal from "Components/listing/messageModal.vue"

export default class ListingPage extends BasePage {
	constructor() {
		super();
		this.init();
	}

	private init() {
		this.instance = new Vue({
			el: "#vue-app-container",
			store: this.store,
			components: {
				"phone-link": PhoneLink,
				"photo-item": PhotoItemComponent,
				"photo-modal": PhotoModalComponent,
				"message-button": OfferMessageButtonComponent,
				"message-modal": MessageModal
			}
		});
	}
}