import Vue from "vue";
import Vuex from "vuex";
import { popup } from "./modules/popup";
import { toastMessage } from "./modules/toastMessage";

Vue.use(Vuex);

export default new Vuex.Store({
	modules: {
		toastMessage,
		popup
	}
});
