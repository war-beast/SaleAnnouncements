import Vue from "vue";
import MessagesComponent from "Components/profile/messages.vue";
import BasePage from "Pages/basePage";

class AppCore extends BasePage {
	constructor() {
		super();
		this.init();
	}

	private init() {
		this.instance = new Vue({
			el: "#vue-app-container",
			render: (h: any) => h(MessagesComponent)
		});
	}
}

new AppCore();