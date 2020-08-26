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
export class Category {
    constructor(id, name) {
        this._id = id;
        this._name = name;
    }
    get name() {
        return this._name;
    }
    get id() {
        return this._id;
    }
}
export class Status {
    constructor(id, name, price) {
        this._id = id;
        this._name = name;
        this._price = price;
    }
    get name() {
        return this._name;
    }
    get id() {
        return this._id;
    }
    get price() {
        return this._price;
    }
}
//# sourceMappingURL=application.js.map