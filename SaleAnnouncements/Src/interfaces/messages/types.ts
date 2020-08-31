export interface IMessageTitle {
	name: string;
	date: string;
}

export abstract class ListingItem {
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