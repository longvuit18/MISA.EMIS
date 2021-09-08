import VTooltip from "v-tooltip";
import Vue from "vue";
import { axiosConfig } from "./api/axiosConfig";
import App from "./App.vue";
import Button from "./components/base/Button";
import ButtonIcon from "./components/base/ButtonIcon";
import Checkbox from "./components/base/Checkbox";
import Col from "./components/base/Col";
import Combobox from "./components/base/Combobox";
import DatePicker from "./components/base/DatePicker";
import Dialog from "./components/base/Dialog";
import DropdownButton from "./components/base/DropdownButton";
import Input from "./components/base/Input";
import Popup from "./components/base/Popup";
import Radio from "./components/base/Radio";
import RadioGroup from "./components/base/RadioGroup";
import Row from "./components/base/Row";
import Spin from "./components/base/Spin";
import ToastMessage from "./components/base/ToastMessage";
import router from "./router";
import store from "./store";

// file config axios này luôn phải để ở file main.js
axiosConfig();

// register
Vue.component("ToastMessage", ToastMessage);
Vue.component("BaseButton", Button);
Vue.component("BaseDropdownButton", DropdownButton);
Vue.component("BaseInput", Input);
Vue.component("BaseDialog", Dialog);
Vue.component("BaseCol", Col);
Vue.component("BaseRow", Row);
Vue.component("BasePopup", Popup);
Vue.component("BaseCombobox", Combobox);
Vue.component("BaseButtonIcon", ButtonIcon);
Vue.component("BaseSpin", Spin);
Vue.component("BaseCheckbox", Checkbox);
Vue.component("BaseRadio", Radio);
Vue.component("BaseRadioGroup", RadioGroup);
Vue.component("BaseDatePicker", DatePicker);
Vue.use(VTooltip);

Vue.config.productionTip = false;
new Vue({
	router,
	store,
	render: h => h(App)
}).$mount("#app");
