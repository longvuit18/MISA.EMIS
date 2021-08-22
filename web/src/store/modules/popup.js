import resources from "../../resources";
/**
 * module popup
 * Created by: VLVU (15/8/2021)
 */
export const popup = {
	namespaced: true,

	state: {
		popupRef: null
	},

	mutations: {
		setPopupRef(state, ref) {
			state.popupRef = ref;
		}
	},

	actions: {
		setPopupRef(context, ref) {
			context.commit("setPopupRef", ref);
		},

		/**
		 * Đây là hàm xác nhận popup
		 * @param {obj} state state của name space popup
		 * @param {*} props nội dung của popup
		 * @returns true/false nếu true nghĩa là người dùng đã confirm ngược lại là false
		 */
		async confirmPopup(context, props) {
			if (!props) {
				props = resources.popup.defaultPopup;
			}
			const hasConfirm = await context.state.popupRef.show(props);
			return hasConfirm;
		}
	}
};
