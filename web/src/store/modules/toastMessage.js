
export const toastMessage = {
	namespaced: true,

	state: {
		toast: {}
	},

	mutations: {
		/**
		 * Thực viện lưu và bắt toast cho người dùng
		 * @param {object} state state hiện tại của module
		 * @param {object} newToast toast cần hiện thông báo
		 * Created by: VLVU (30/7/2021)
		 */
		setToast(state, newToast) {
			state.toast = newToast;
		}
	}
};
