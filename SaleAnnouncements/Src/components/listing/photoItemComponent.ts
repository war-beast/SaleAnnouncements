import { Vue, Component, Prop } from "vue-property-decorator";
import { bus } from "Util/bus";
import ApiRequest from "Util/request";
import { ApiResult } from "Models/apiResult";

const getPhotoUrl = "/api/common/getPhoto";

@Component
export default class PhotoItemComponent extends Vue {
	@Prop()
	private id: string;

	private imageBytes: string = "";
	private readonly apiRequest: ApiRequest;

	constructor() {
		super();
		this.apiRequest = new ApiRequest();
	}

	public created() {
		this.loadImage();
	}

	private async loadImage() {

		await this.apiRequest.getData(`${getPhotoUrl}?id=${this.id}`)
			.then((result: ApiResult) => {
				if (result.success) {
					this.imageBytes = result.value;
				} else {
					console.error(`Произошла ошибка на сервере, не удалось получить фото id=${this.id}`);
				}
			});
	}

	private zoom() {
		bus.$emit("zoomPhoto", this.imageBytes);
	}
};