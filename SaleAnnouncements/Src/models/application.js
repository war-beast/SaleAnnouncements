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
export class MessageThread extends ListingItem {
    constructor(id, name, messages, companionId) {
        super(id, name);
        this.messages = [];
        this.messages = messages;
        this.companionId = companionId;
    }
}
export class MessagesPageOptions {
}
export class OfferMessage {
    constructor() {
        this.currentCustomerId = null;
        this.offerId = null;
        this.offerOwnerId = null;
        this.message = null;
    }
    static createBuilder() {
        return new OfferMessageBuilder();
    }
}
export class OfferMessageBuilder {
    constructor() {
        this.offerMessage = new OfferMessage();
    }
    setCustomerId(customerId) {
        this.offerMessage.currentCustomerId = customerId;
        return this;
    }
    setOfferId(offerId) {
        this.offerMessage.offerId = offerId;
        return this;
    }
    setOfferOwnerId(ownerId) {
        this.offerMessage.offerOwnerId = ownerId;
        return this;
    }
    build() {
        return this.offerMessage;
    }
}
//# sourceMappingURL=application.js.map