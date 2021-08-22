/**
 * Chứa những hàm dùng chung, những hàm mở rộng
 * Created by: VLVU (20/8/2021)
 */

/**
 * format date local (VN)
 * @param {Date} date ngày tháng năm truyền vào
 * @returns date đã format dạng dd/MM/YYYY
 * Created by: VLVU (2021)
 */
const formatDateLocal = (date) => {
	if (!date) {
		return "";
	}
	let day = new Date(date).getDate();
	let month = new Date(date).getMonth() + 1;
	const year = new Date(date).getFullYear();

	if (day < 10) day = "0" + day;
	if (month < 10) month = "0" + month;
	return day + "/" + month + "/" + year;
};

/**
 * format date dạng input element value có thể hiểu được
 * @param {Date} date ngày tháng năm truyền vào
 * @returns date đã format dạng YYYY-MM-dd
 */
const formatDateValueInput = (date) => {
	if (!date) {
		return null;
	}

	let day = new Date(date).getDate();
	let month = new Date(date).getMonth() + 1;
	const year = new Date(date).getFullYear();

	if (day < 10) day = "0" + day;
	if (month < 10) month = "0" + month;
	return year + "-" + month + "-" + day;
};

/**
 * format tiền theo định dạng tiền của Việt Nam
 * @param {[number, string]} currency tiền cần format
 * @returns tiền đã được format
 */
const formatCurrency = (currency) => {
	if (!currency) {
		return "";
	}
	return new Intl.NumberFormat("vn-VN").format(Number(currency));
};

export default {
	formatDateLocal,
	formatDateValueInput,
	formatCurrency

};
