import Vue from "vue";
import AddStatusComponent from "Components/profile/addStatus.vue";
import BasePage from "Pages/basePage";
class AppCore extends BasePage {
    constructor() {
        super();
        this.init();
    }
    init() {
        this.instance = new Vue({
            el: "#vue-app-container",
            render: (h) => h(AddStatusComponent)
        });
    }
}
new AppCore();
//# sourceMappingURL=addStatus.js.map