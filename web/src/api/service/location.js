import axios from "axios";

class LocationApi {
	route = "api/v1/location";

	/**
	 * api lấy tất cả nước
	 * @returns axios promise
	 * Created by: VLVU (25/9/2021)
	 */
	getCountries() {
		return axios.get(`${this.route}/countries`);
	}

	/**
	 * Lấy tất cả các tỉnh của 1 nước
	 * @param {uuid} countryId id của nước
	 * @returns axios promise
	 * Created by: VLVU (25/9/2021)
	 */
	getProvinces(countryId) {
		return axios.get(`${this.route}/provinces?countryId=${countryId}`);
	}

	/**
	 * Lấy tất các các huyện
	 * @param {uuid} countryId id của nước
	 * @param {int} provinceId id của tỉnh
	 * @returns  axios promise
	 * Created by: VLVU (25/9/2021)
	 */
	getDistricts(countryId, provinceId) {
		return axios.get(`${this.route}/districts?countryId=${countryId}&provinceId=${provinceId}`);
	}

	/**
	 * Lấy tất cả cá xã
	 * @param {uuid} countryId id của nước
	 * @param {int} provinceId id của tỉnh
	 * @param {*} districtId id của huyện
	 * Created by: VLVU (25/9/2021)
	 * @returns axios promise
	 */
	getWards(countryId, provinceId, districtId) {
		return axios.get(`${this.route}/wards?countryId=${countryId}&provinceId=${provinceId}&districtId=${districtId}`);
	}
}

export default new LocationApi();
