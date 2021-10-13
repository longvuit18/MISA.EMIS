<template>
    <div :class="{'form-item': label !== undefined}">
        <label v-if="label">{{label}}{{" "}}<span v-if="required"><span style="color: red;">*</span></span></label>
        <div
            class="box-input"
            v-if="!isTextarea"
        >
            <input
                :required="required"
                :class="{'full-width': fullWidth, 'border-red': error, 'enter-text-right-to-left': hasCurrencyFormat || enterRightToLeft}"
                v-bind="$attrs"
                :tabindex="tabindex"
                :value="formatValue"
                v-on:input="updateValue($event)"
                @blur="onBlur"
                ref="BaseInput"
                :title="errorMessage"
            >
            <div
                class="end-icon"
                v-if="searchIcon"
            ></div>
        </div>
        <div
            class="box-input"
            v-else
        >
            <textarea
                :required="required"
                style="height: auto;"
                :class="{'full-width': fullWidth, 'border-red': error, 'enter-text-right-to-left': hasCurrencyFormat || enterRightToLeft}"
                v-bind="$attrs"
                :tabindex="tabindex"
                :value="formatValue"
                v-on:input="updateValue($event)"
                @blur="onBlur"
                ref="BaseInput"
                :title="errorMessage"
            />
            <div
                class="end-icon"
                v-if="searchIcon"
            ></div>
        </div>
    </div>
</template>

<script>
/**
 * Base input
 * Created by: VLVU (18/7/2021)
 */
import AutoNumeric from "autonumeric";
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
            type: String,
            default: "string"
        },

        // có bắt buộc nhập hay không?. default: false là khôn bắt buộc nhập
        required: {
            type: Boolean,
            default: false
        },

        // label hiển thị bên trên input
        label: String,

        // vị trí của tab
        tabindex: [String, Number],

        // giá trị của input
        value: {
            type: [String, Number]
        },

        // Tên của input
        name: {
            type: String,
            default: ""
        },

        isTextarea: {
            type: Boolean,
            default: () => false
        },

        enterRightToLeft: {
            type: Boolean,
            default: false
        },

        focusInput: {
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

    watch: {
        value(v) {

        }
    },

    computed: {
        hasCurrencyFormat() {
            return this.format === "currency";
        },

        formatValue() {
            if ((this.format === "number" || this.format === "currency") && this.value) {
                const settings = {
                    digitGroupSeparator: ".",
                    decimalCharacter: ",",
                    minimumValue: "0",
                    decimalCharacterAlternative: ".",
                    allowDecimalPadding: "floats"
                };
                const numberic = AutoNumeric.format(this.value, settings);
                if (numberic.endsWith("0") && numberic.indexOf(",") > -1) {
                    return numberic.slice(0, numberic.length - 1);
                }
                return numberic;
            }
            return this.value;
        }
    },

    mounted() {
        if (this.focusInput === true) {
            this.$refs.BaseInput.focus();
        }
    },
    methods: {
        /**
         * update value handle
         * Created by: VLVU (2021)
         */
        updateValue(event) {
            const value = event.target.value;
            if (this.format === "number" || this.format === "currency") {
                const newValue = value?.replaceAll(".", "")?.replaceAll(",", ".");
                this.$emit("input", Number(newValue));
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
            if (this.format === "email" && !this.validateEmail(e.currentTarget.value)) {
                this.error = true;
                this.errorMessage = ErrorValidateEmail();
                return;
            } else {
                this.error = false;
                this.errorMessage = "";
            }

            // validate phone number
            if (this.format === "phoneNumber" && !this.validatePhoneNumber(e.currentTarget.value)) {
                this.error = true;
                this.errorMessage = ErrorValidatePhoneNumber();
            } else {
                this.error = false;
                this.errorMessage = "";
            }
        },

        /**
         * Validate email
         * Created by: VLVU (20/7/2021)
         */
        validateEmail(email) {
            const regex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return regex.test(email);
        }
    }
};
</script>

<style scoped src="./style.css">
</style>
