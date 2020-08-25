export class OfferModel {
	public subject: string;
	public description: string;
	public price: number;
	public phone: string;

	constructor(subject: string, description: string, price: number, phone: string) {
		this.subject = subject;
		this.description = description;
		this.price = price;
		this.phone = phone;
	}
}