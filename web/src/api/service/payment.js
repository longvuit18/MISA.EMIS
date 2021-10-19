import axios from "axios";
import { BaseApi } from "../base";
class PaymentApi extends BaseApi {
	constructor() {
		const route = "api/v1/receiptPayment";
		super(route);
	}

	/**
	 * Lấy mã mới cho chi tiền
	 * @returns axios promise
	 * Created by: VLVU (6/10/2021)
	 */
	async getPaymentCode() {
		return await axios.get(`${this.route}/newPaymentCode`);
	}

	/**
	 * api filter
	 * @param {object} filter object chứa các trường để filter
	 * @returns axios promise
	 * Created by: VLVU (20/9/2021)
	 */
	async getPaymentFilterPaging(filter) {
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

	/**
	 * api insert payment
	 * @returns axios promise
	 * Created by: VLVU (14/10/2021)
	 */
	async insertPayment(payment, paymentDetails) {
		return await axios.post(`${this.route}/payment`, {
			payment,
			paymentDetails
		});
	}

	/**
	 * api lấy 1 payment
	 * @param {string} paymentId id payment
	 * @returns axios promise
	 * Created by: VLVU (14/10/2021)
	 */
	async getPaymentById(paymentId) {
		return await axios.get(`${this.route}/payment/${paymentId}`);
	}

	// override here
}

export default new PaymentApi();
