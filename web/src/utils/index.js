/**
 * Chứa những hàm dùng chung, những hàm mở rộng
 * Created by: VLVU (20/8/2021)
 */

import AutoNumeric from "autonumeric";
/**
 * format date local (VN)
 * @param {Date} date ngày tháng năm truyền vào
 * Created by: VLVU (2021)
 * @returns date đã format dạng dd/MM/YYYY
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

/**
 * Hàm format số theo cách hiểu của người Việt
 * @param {number | string} number số cần format
 * @returns giá trị đã format theo cách hiểu của người việt
 * Created by: VLVU (13/10/2021)
 */
const formatNumber = (number) => {
	const settings = {
		digitGroupSeparator: ".",
		decimalCharacter: ",",
		minimumValue: "0",
		decimalCharacterAlternative: ".",
		allowDecimalPadding: "floats"
	};
	return AutoNumeric.format(number, settings);
};

/**
 * Hàm chuyển tiếng việt sang tiếng anh
 * Cre: https://gist.github.com/hu2di/e80d99051529dbaa7252922baafd40e3
 * @param {string} str text cần convert
 * @returns text đã convert
 */
export function removeVietnameseTones(str) {
	str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
	str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
	str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
	str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
	str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
	str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
	str = str.replace(/đ/g, "d");
	str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
	str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
	str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
	str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
	str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
	str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
	str = str.replace(/Đ/g, "D");
	// Some system encode vietnamese combining accent as individual utf-8 characters
	// Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
	str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
	str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
	// Remove extra spaces
	// Bỏ các khoảng trắng liền nhau
	str = str.replace(/ + /g, " ");
	str = str.trim();
	// Remove punctuations
	// Bỏ dấu câu, kí tự đặc biệt
	// str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'|\"|\&|\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g, " ");
	return str;
}

export default {
	formatDateLocal,
	formatDateValueInput,
	formatCurrency,
	formatNumber,
	removeVietnameseTones

};
