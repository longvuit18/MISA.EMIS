<template>
    <div class="form-item-datepicker">
        <label v-if="label">{{label}}{{" "}}<span v-if="required"><span style="color: red;">*</span></span></label>
        <div class="input-box">
            <DatePicker
                placeholder="DD/MM/YYYY"
                :input-attr="inputAttribute"
                :input-class="'ms-input-datepicker'"
                v-bind="$attrs"
                v-on="$listeners"
            />
        </div>
    </div>
</template>

<script>
/**
 * Base Date Picker
 * Created by: VLVU (18/8/2021)
 */

import enums from "../../../enums";
import DatePicker from "vue2-datepicker";
import "vue2-datepicker/index.css";

// message error
const ErrorRequire = (name) => `${name} là trường bắt buộc phải nhập!`;
export default {
    name: "BaseDatePicker",
    components: { DatePicker },
    props: {

        // có fullwidth của input hay không? default: false là không fullwidth
        fullWidth: {
            type: Boolean,
            default: false
        },

        // có bắt buộc nhập hay không?. default: false là khôn bắt buộc nhập
        required: {
            type: Boolean,
            default: false
        },

        // label hiển thị bên trên input
        label: String,

        // vị trí của tab
        tabindex: String,

        // Tên của input
        name: {
            type: String,
            default: ""
        }

    },
    data() {
        return {
            error: false,
            errorMessage: "",
            inputAttribute: {
                tabindex: this.tabindex
            }
        };
    },

    methods: {
        /**
         * update value handle
         * Created by: VLVU (2021)
         */
        updateValue(event) {
            let value = event.target.value;
            if (this.format === enums.format.currency) {
                value = this.formatCurrency(value);
                this.$emit("input", value);
                return;
            }
            this.$emit("input", value);
        },

        /**
         * Handle ấn ra ngoài input
         * Created by: VLVU (2021)
         */
        onBlur(e) {
            if (e.currentTarget.value === "" && this.required) {
                this.error = true;
                this.errorMessage = ErrorRequire(this.name);
            } else {
                this.error = false;
                this.errorMessage = "";
            }
        }
    }
};
</script>

<style src="./style.css">
</style>
