
import { BaseApi } from "../base";
class AccountApi extends BaseApi {
	constructor() {
		const route = "api/v1/accounts";
		super(route);
	}

	// override here
}

export default new AccountApi();
