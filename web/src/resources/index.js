/**
 * Đây là nơi chứa resources của app
 * Created by: Vũ Long Vũ (20/7/2021)
 */

// thời gian mà toast message biết mất
export const DURATION_TOAST_MESSAGE = 3500;

const toast = {
	/**
	 * thông báo mặc định
	 */
	defaultToast: {
		content: "Thông báo",
		type: "info"
	},
	/**
	 * thông báo thêm  thành công
	 * @param {string} employeeCode mã
	 * @returns object toast message
	 */
	addSuccess(code, name) {
		return {
			content: `Bạn đã thêm ${name} <${code}> thành công!`,
			type: "success"
		};
	},
	/**
	 * thông báo sửa  thành công
	 * @param {string} code mã
	 * @returns object toast message
	 */
	updateSuccess(code, name) {
		return {
			content: `Bạn đã sửa ${name} <${code}> thành công!`,
			type: "success"
		};
	},
	/**
	 * thông báo xóa  thành công
	 * @param {string} code mã
	 * @param {string} name tên toast
	 * @returns object toast message
	 */
	deleteSuccess(code, name) {
		return {
			content: `Bạn đã xóa ${name} <${code}> thành công!`,
			type: "success"
		};
	}
};
const popup = {
	// thông tin popup default
	defaultPopup: {
		content: "Content popup",
		state: "alert",
		type: "info"
	},
	/**
	 * popup xóa nhân viên thành công
	 * @param {string} employeeCode mã nhân viên
	 * @returns object popup message
	 */
	deleteEmployee(employeeCode) {
		return {
			content: `Bạn có thực sự muốn xóa nhân viên <${employeeCode}> này không?`,
			state: "confirm",
			type: "warning"
		};
	},

	/**
	 * popup xóa nhà cung cấp thành công
	 * @param {string} providerCode mã nhân viên
	 * @returns object popup message
	 */
	deleteProvider(providerCode) {
		return {
			content: `Bạn có thực sự muốn xóa nhà cung cấp <${providerCode}> này không?`,
			state: "confirm",
			type: "warning"
		};
	},

	/**
	 * popup xóa tất cả các dòng đã nhập
	 * @returns object popup message
	 */
	deleteAllRows: {
		content: "Bạn có thực sự muốn xóa tất cả các dòng đã nhập không này không?",
		state: "confirm",
		type: "warning"
	},

	confirmCloseDetailForm: {
		content: "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?",
		state: "hyper-confirm",
		type: "info",
		primaryButtonName: "Có",
		secondaryButtonName: "Không"
	}
};
export default {
	DURATION_TOAST_MESSAGE,
	popup,
	toast,
	serverErrorMessageDefault: "Đã có lỗi xảy ra, vui lòng liên hệ misa để biết thêm chi tiết!"
};
