import { ListingItem } from "Interfaces/messages/types";
export class OfferModel {
    constructor(subject, description, price, phone) {
        this.photos = [];
        this.title = "";
        this.description = "";
        this.price = 0;
        this.phoneNumber = "";
        this.offerStatuses = [];
        this.categoryId = "";
        this.title = subject;
        this.description = description;
        this.price = price;
        this.phoneNumber = phone;
    }
    setPhotoFiles(photos) {
        this.photos = photos;
    }
}
export class Category extends ListingItem {
}
export class Status extends ListingItem {
    constructor(id, name, price) {
        super(id, name);
        this._price = price;
    }
    get price() {
        return this._price;
    }
}
export class OfferPageOptions {
    constructor(offerId) {
        this.offerId = offerId;
    }
}
export class MessageTitle {
    constructor(name, date, messageId) {
        this.name = name;
        this.date = date;
        this.messageId = messageId;
    }
}
export class SingleMessage {
    constructor(name, date, message, isMyMessage) {
        this.name = name;
        this.date = date;
        this.message = message;
        this.isMyMessage = isMyMessage;
    }
}
export class MessageHostOffer extends ListingItem {
}
export class MessagesPageOptions {
}
//# sourceMappingURL=application.js.map