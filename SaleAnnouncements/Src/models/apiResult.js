export class ApiResult {
    constructor(success, value) {
        this.success = success;
        this.value = value;
    }
}
export class ServerOperationResult {
    constructor(isSuccess, error, entityId) {
        this._isSuccess = isSuccess;
        this._error = error;
        this._entityId = entityId;
    }
    get isSuccess() {
        return this._isSuccess;
    }
    get error() {
        return this._error;
    }
    get entityId() {
        return this._entityId;
    }
}
//# sourceMappingURL=apiResult.js.map