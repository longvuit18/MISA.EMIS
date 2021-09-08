<template>
    <div class="form-item">
        <label v-if="label">{{label}}{{" "}}<span v-if="required"><span style="color: red;">*</span></span></label>
        <div
            class="box-input"
            v-if="!isTextarea"
        >
            <input
                :required="required"
                :class="{'full-width': fullWidth, 'border-red': error, 'enter-text-right-to-left': hasCurrencyFormat}"
                v-bind="$attrs"
                :tabindex="tabindex"
                :value="valueFormat"
                v-on:input="updateValue($event)"
                @blur="onBlur"
                ref="BaseInput"
                @keypress="keyPress"
                :title="errorMessage"
            >
            <div
                class="end-icon"
                v-if="searchIcon"
            ></div>
            <div
                v-if="hasCurrencyFormat"
                class="currency"
            >
                (VNĐ)
            </div>
        </div>
        <div
            class="box-input"
            v-else
        >
            <textarea
                :required="required"
                style="height: auto;"
                :class="{'full-width': fullWidth, 'border-red': error, 'enter-text-right-to-left': hasCurrencyFormat}"
                v-bind="$attrs"
                :tabindex="tabindex"
                :value="valueFormat"
                v-on:input="updateValue($event)"
                @blur="onBlur"
                ref="BaseInput"
                @keypress="keyPress"
                :title="errorMessage"
            />
            <div
                class="end-icon"
                v-if="searchIcon"
            ></div>
            <div
                v-if="hasCurrencyFormat"
                class="currency"
            >
                (VNĐ)
            </div>
        </div>
    </div>
</template>

<script>
/**
 * Base input
 * Created by: VLVU (18/7/2021)
 */

import enums from "../../../enums";

// message error
const ErrorRequire = (name) => `${name} là trường bắt buộc phải nhập!`;
const ErrorValidateEmail = () => "Bạn cần nhập đúng định dạng email ví dụ: misa@gmail.com";
const ErrorValidatePhoneNumber = () => "Bạn chưa nhập đúng định dạng của số điện thoại. Bắt đầu bằng số 0 và không quá 15 kí tự.";
export default {
    name: "BaseInput",
    props: {
        // có search icon hay không? default: false là không có
        searchIcon: {
            type: Boolean,
            default: false
        },

        // có fullwidth của input hay không? default: false là không fullwidth
        fullWidth: {
            type: Boolean,
            default: false
        },

        // input hiển thị theo định dạng nào
        format: {
            type: Number,
            default: enums.string
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

        // giá trị của input
        value: {
            type: String
        },

        // Tên của input
        name: {
            type: String,
            default: ""
        },

        isTextarea: {
            type: Boolean,
            default: () => false
        }

    },
    data() {
        return {
            error: false,
            errorMessage: ""
        };
    },

    computed: {
        valueFormat() {
            switch (this.format) {
                case enums.format.currency:
                    return this.formatCurrency(this.value);
                default:
                    return this.value;
            }
        },
        hasCurrencyFormat() {
            return this.format === enums.format.currency;
        }
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
                return;
            } else {
                this.error = false;
                this.errorMessage = "";
            }

            // validate email
            if (this.format === enums.format.email && !this.validateEmail(e.currentTarget.value)) {
                this.error = true;
                this.errorMessage = ErrorValidateEmail();
                return;
            } else {
                this.error = false;
                this.errorMessage = "";
            }

            // validate phone number
            if (this.format === enums.format.phoneNumber && !this.validatePhoneNumber(e.currentTarget.value)) {
                this.error = true;
                this.errorMessage = ErrorValidatePhoneNumber();
            } else {
                this.error = false;
                this.errorMessage = "";
            }
        },

        /**
         * handle key press. Cho phép và không cho phép ấn phím nào tùy thuộc vào kiểu format truyền vào
         * Created by: VLVU (1/8/2021)
         */
        keyPress(event) {
            event = (event) || window.event;
            if (this.format === enums.format.currency || this.format === enums.format.number || this.format === enums.format.phoneNumber) {
                var charCode = (event.which) ? event.which : event.keyCode;
                if ((charCode > 31 && (charCode < 48 || charCode > 57)) && charCode !== 46) {
                    event.preventDefault();
                } else {
                    return true;
                }
            }
        },

        /**
         * Validate email
         * Created by: VLVU (20/7/2021)
         */
        validateEmail(email) {
            const regex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return regex.test(email);
        },

        /**
         * Validate phone number
         * Created by: VLVU (20/7/2021)
         */
        validatePhoneNumber(phoneNumber) {
            if (phoneNumber.length > 15 || phoneNumber[0] !== 0) {
                return false;
            }
            return true;
        },

        /**
         * Format tiền
         * Created by: VLVU (20/7/2021)
         */
        formatCurrency(str) {
            return Intl.NumberFormat().format(Number(str?.replaceAll(".", "")?.replaceAll(",", "")) || 0).toString();
        }
    }
};
</script>

<style scoped src="./style.css">
</style>
