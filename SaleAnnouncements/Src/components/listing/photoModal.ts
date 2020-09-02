import { Vue, Component } from "vue-property-decorator";
import { bus } from "Util/bus";

@Component
export default class PhotoModalComponent extends Vue {
	private imageBytes: string = "";
	private isInfoVisible: boolean = false;

	public created() {
		bus.$on("zoomPhoto", this.photoSelected);
	}

	private photoSelected(imageBytes: string) {
		this.imageBytes = imageBytes;
		this.isInfoVisible = true;
	}

	private hideModal() {
		this.isInfoVisible = false;
		this.imageBytes = "";
	}
};