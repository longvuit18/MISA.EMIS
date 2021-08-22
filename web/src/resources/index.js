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
	 * thông báo thêm nhân viên thành công
	 * @param {string} employeeCode mã nhân viên
	 * @returns object toast message
	 */
	addEmployeeSuccess(employeeCode) {
		return {
			content: `Bạn đã thêm nhân viên <${employeeCode}> thành công!`,
			type: "success"
		};
	},
	/**
	 * thông báo sửa nhân viên thành công
	 * @param {string} employeeCode mã nhân viên
	 * @returns object toast message
	 */
	updateEmployeeSuccess(employeeCode) {
		return {
			content: `Bạn đã sửa nhân viên <${employeeCode}> thành công!`,
			type: "success"
		};
	},
	/**
	 * thông báo xóa nhân viên thành công
	 * @param {string} employeeCode mã nhân viên
	 * @returns object toast message
	 */
	deleteEmployeeSuccess(employeeCode) {
		return {
			content: `Bạn đã xóa nhân viên <${employeeCode}> thành công!`,
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

	confirmCloseEmployeeDetailForm: {
		content: "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?",
		state: "hyper-confirm",
		type: "info",
		primaryButtonName: "Cất",
		secondaryButtonName: "Không"
	}
};
export default {
	DURATION_TOAST_MESSAGE,
	popup,
	toast,
	serverErrorMessageDefault: "Đã có lỗi xảy ra, vui lòng liên hệ misa để biết thêm chi tiết!"
};
