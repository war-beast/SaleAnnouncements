import Vue from "vue";
import MessagesComponent from "Components/profile/messages.vue";
import BasePage from "Pages/basePage";
class AppCore extends BasePage {
    constructor() {
        super();
        this.init();
    }
    init() {
        this.instance = new Vue({
            el: "#vue-app-container",
            render: (h) => h(MessagesComponent)
        });
    }
}
new AppCore();
//# sourceMappingURL=messages.js.map