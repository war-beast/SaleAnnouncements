import { Vue, Component, Prop } from "vue-property-decorator";
const logoutUrl = "/api/account/logout";

@Component
export default class ClearPage extends Vue {
	constructor() {
		super();
	}
};