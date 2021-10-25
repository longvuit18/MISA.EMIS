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
	},

	/**
	 * thông báo xóa  thành công
	 * @param {string} code mã
	 * @param {string} name tên toast
	 * @returns object toast message
	 */
	deleteMultipleSuccess(name) {
		return {
			content: `Bạn đã xóa tất cả ${name} đã chọn thành công!`,
			type: "success"
		};
	},

	/// /// Account /// ///

	/**
	 * thông báo số tài khoản không hợp lệ
	 * @returns object toast message
	 */
	accountNumberInvalid() {
		return {
			content: "Số tài khoản không hợp lệ. Số tài khoản chi tiết phải bắt đầu bằng số của Tài khoản tổng hợp",
			type: "error"
		};
	},

	/**
	 * Thông báo tài khoản không hợp lệ
	 * @param {*} startChildAccount string bắt đầu của con
	 * @returns object toast message
	 */
	accountNumberDuplicate() {
		return {
			content: "Số tài khoản đã tồn tại. Vui lòng kiểm tra lại.",
			type: "error"
		};
	},

	/**
	 * Thông báo tài khoản không hợp lệ
	 * @param {*} startChildAccount string bắt đầu của con
	 * @returns object toast message
	 */
	accountNumberDuplicateChild(startChildAccount) {
		return {
			content: `Số tài khoản không hợp lệ. Số tài khoản có phần đầu <${startChildAccount}> trùng với tiết khoản con <${startChildAccount}> cùng cấp.`,
			type: "error"
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
	 * popup hỏi muốn xóa 1 không
	 * @param {string} code mã
	 * @param {string} name tên đối tượng xóa
	 * @returns object popup message
	 */
	deleteOne(code, name) {
		return {
			content: `Bạn có thực sự muốn xóa ${name} &lt;${code}&gt; này không?`,
			state: "confirm",
			type: "warning"
		};
	},

	/**
	 * popup thông báo có phát sinh
	 * @param {string} name tên của đối tượng
	 * @param {string} action tên của hành động
	 * @returns object popup message
	 */
	arisingRelatedProvider(name, action) {
		return {
			content: `
				<b>${action} không thành công.</b><br/><br/>
				Nhà cung cấp <b>${name}</b> đã <b>có phát sinh.</b> Bạn phải xóa các phát sinh liên quan trước khi xóa nhà cung cấp.			
			`,
			type: "error",
			state: "alert"
		};
	},

	/**
	 * popup thông báo xóa tài khoản bị lỗi
	 * @returns object popup message
	 */
	deleteAccountFail() {
		return {
			content: `
				<b>Xóa không thành công.</b><br/><br/>
				Không thể xóa danh mục cha nếu chưa xóa tất cả các danh mục con.		
			`,
			type: "error",
			state: "alert"
		};
	},

	/**
	 * popup hỏi muốn xóa nhiều không
	 * @param {string} name tên đối tượng
	 * @returns object popup message
	 */
	deleteMultiple(name) {
		return {
			content: `Bạn có thực sự muốn xóa tất cả ${name} đã chọn không?`,
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
