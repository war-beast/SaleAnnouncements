export class OfferModel {
	private photos: Array<File> = [];

	public title: string = "";
	public description: string = "";
	public price: number = 0;
	public phoneNumber: string = "";
	public offerStatuses: Array<string> = [];
	public categoryId: string = "";

	constructor(subject: string, description: string, price: number, phone: string) {
		this.title = subject;
		this.description = description;
		this.price = price;
		this.phoneNumber = phone;
	}

	public setPhotoFiles(photos: Array<File>) {
		this.photos = photos;
	}
}

export class Category {
	private _name: string;
	private _id: string;

	constructor(id: string, name: string) {
		this._id = id;
		this._name = name;
	}

	get name(): string {
		return this._name;
	}

	get id(): string {
		return this._id;
	}
}

export class Status {
	private _id: string;
	private _name: string;
	private _price: number;

	constructor(id: string, name: string, price: number) {
		this._id = id;
		this._name = name;
		this._price = price;
	}

	get name(): string {
		return this._name;
	}

	get id(): string {
		return this._id;
	}

	get price(): number {
		return this._price;
	}
}

export class OfferPageOptions {
	public offerId: string;

	constructor(offerId: string) {
		this.offerId = offerId;
	}
}