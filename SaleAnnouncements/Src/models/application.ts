import { IMessageTitle, ListingItem } from "Interfaces/messages/types";

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

export class Category extends ListingItem {
}

export class Status extends ListingItem {
	private _price: number;

	constructor(id: string, name: string, price: number) {
		super(id, name);

		this._price = price;
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

export class MessageTitle implements IMessageTitle {
	public name: string;
	public date: string;

	public messageId: string;

	constructor(name: string, date: string, messageId: string) {
		this.name = name;
		this.date = date;
		this.messageId = messageId;
	}
}

export class SingleMessage implements IMessageTitle {
	public name: string;
	public date: string;
	public message: string;
	public isMyMessage: boolean;

	constructor(name: string, date: string, message: string, isMyMessage: boolean) {
		this.name = name;
		this.date = date;
		this.message = message;
		this.isMyMessage = isMyMessage;
	}
}

export class MessageThread extends ListingItem {
	public messages: Array<SingleMessage> = [];

	constructor(id: string, name: string, messages: Array<SingleMessage>) {
		super(id, name);

		this.messages = messages;
	}
}

export class MessagesPageOptions {
	public currentCustomerId: string;
}