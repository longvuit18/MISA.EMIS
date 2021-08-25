import { BaseApi } from "../base";

class DepartmentApi extends BaseApi {
	constructor() {
		const route = "api/v1/departments";
		super(route);
	}
}

export default new DepartmentApi();
