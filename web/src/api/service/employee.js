import axios from "axios";
import { BaseApi } from "../base";
class EmployeeApi extends BaseApi {
	constructor() {
		const route = "api/v1/employees";
		super(route);
	}

	/**
	 * Lấy mã mới cho nhân viên
	 * @returns axios promise
	 * Created by: VLVU (20/7/2021)
	 */
	async getEmployeeCode() {
		return await axios.get(`${this.route}/newEmployeeCode`);
	}

	/**
	 * api filter
	 * @param {string} filterText text muốn filter: Tên, số điện thoại, mã nhân viên
	 * @param {number} pageNumber page số bao nhiêu
	 * @param {number} pageSize số lượng bản ghi trong page
	 * @returns axios promise
	 * Created by: VLVU (18/8/2021)
	 */
	async getEmployeeFilterPaging(filterText, pageNumber, pageSize) {
		return await axios.get(`${this.route}/filterPaging?employeeFilter=${filterText}&pageNumber=${pageNumber}&pageSize=${pageSize}`);
	}

	// override here
}

export default new EmployeeApi();
