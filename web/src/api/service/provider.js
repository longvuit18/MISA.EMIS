import axios from "axios";
import { BaseApi } from "../base";
class ProviderApi extends BaseApi {
	constructor() {
		const route = "api/v1/providers";
		super(route);
	}

	/**
	 * Lấy mã mới cho nhà cung cấp
	 * @returns axios promise
	 * Created by: VLVU (20/9/2021)
	 */
	async getProviderCode() {
		return await axios.get(`${this.route}/newProviderCode`);
	}

	/**
	 * api filter
	 * @param {object} filter object chứa các trường để filter
	 * @returns axios promise
	 * Created by: VLVU (20/9/2021)
	 */
	async getProviderFilterPaging(filter) {
		return await axios.post(`${this.route}/filterPaging`, { ...JSON.parse(JSON.stringify(filter)) });
	}

	/**
	 * api filter
	 * @param {Array} listId list id muốn xóa
	 * @returns axios promise
	 * Created by: VLVU (29/9/2021)
	 */
	async deleteMultiple(listId) {
		return await axios.delete(`${this.route}/batch`, {
			data: listId
		});
	}

	// override here
}

export default new ProviderApi();
