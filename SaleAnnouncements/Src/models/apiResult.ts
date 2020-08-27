export class ApiResult {
	public success: boolean;
	public value: any;

	constructor(success: boolean, value: any) {
		this.success = success;
		this.value = value;
	}
}

export class ServerOperationResult {
	private _isSuccess: boolean;
	private _error: string;
	private _entityId: string;

	constructor(isSuccess: boolean, error: string, entityId: string) {
		this._isSuccess = isSuccess;
		this._error = error;
		this._entityId = entityId;
	}

	public get isSuccess(): boolean {
		return this._isSuccess;
	}

	public get error(): string {
		return this._error;
	}

	public get entityId(): string {
		return this._entityId;
	}
}