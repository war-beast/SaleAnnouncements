import Vue from "vue";
import NewOfferComponent from "Components/profile/newOffer.vue";
import BasePage from "Pages/basePage";

class AppCore extends BasePage {
	constructor() {
		super();
		this.init();
	}

	private init() {
		this.instance = new Vue({
			el: "#vue-app-container",
			render: (h: any) => h(NewOfferComponent),
			store: this.store
		});
	}
}

new AppCore();