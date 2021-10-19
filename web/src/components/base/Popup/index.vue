
<template>
    <BaseDialog :isOpen="dataOpen">
        <div class="popup">
            <div class="popup-content">
                <div class="content-icon">
                    <div :class="getIcon" />
                </div>
                <div
                    class="content-text"
                    v-html="dataContent"
                >
                </div>
            </div>
            <div class="popup-space">
            </div>
            <div
                v-if="dataState === 'alert'"
                class="flex"
                :class="dataType === 'warning' ? 'justify-end' : 'justify-center'"
            >
                <BaseButton
                    buttonName="Đồng ý"
                    @click="closePopup"
                />
            </div>

            <div
                v-if="dataState === 'confirm'"
                class="flex justify-space-between"
            >
                <BaseButton
                    buttonName="Không"
                    secondaryButton
                    @click="secondaryButtonClick"
                />

                <BaseButton
                    buttonName="Có"
                    @click="primaryButtonClick"
                />
            </div>
            <div
                v-if="dataState === 'hyper-confirm'"
                class="popup-action"
            >
                <div class="close-button">
                    <BaseButton
                        buttonName="Hủy"
                        secondaryButton
                        @click="closePopup"
                    />
                </div>
                <div class="confirm">
                    <div style="margin-right: 8px">
                        <BaseButton
                            :buttonName="dataSecondaryButtonName"
                            secondaryButton
                            @click="secondaryButtonClick"
                        />
                    </div>
                    <BaseButton
                        :buttonName="dataPrimaryButtonName"
                        @click="primaryButtonClick"
                    />
                </div>
            </div>
        </div>
    </BaseDialog>
</template>

<script>
/* eslint-disable vue/no-reserved-keys */
/**
 * Base popup
 * Hướng dẫn sử dụng:
 * - Sử dụng thông qua ref và sử dụng method show để có thể set các giá trị cho component
 * Đồng thời đây là 1 promise trả về true khi người dùng comfirm
 * - Cách 2: Sử dụng bằng cách các
 * Created by: VLVU (23/7/2021)
 */
export default {
    name: "BasePopup",
    props: {
        // nội dung của popup
        content: {
            type: String,
            default: () => ""
        },
        // popup open hay không true/false
        open: {
            type: Boolean,
            default: () => false
        },
        // tên của secondary button
        primaryButtonName: {
            type: String,
            default: () => ""
        },

        // tên của secondary button
        secondaryButtonName: {
            type: String,
            default: () => ""
        },

        // Thông báo là thông báo loại gì: info, warning, success, error
        type: {
            type: String,
            default: () => "info"
        },
        // Trang thái của popup: alert, confirm, hyper confirm(ngoài trả về true hoặc false, thì còn trả về null nghĩa là chỉ tắt confirm đó)
        state: {
            type: String,
            default: () => "alert"
        }
    },

    data() {
        return {
            // các biến trong đây cho phép truyền các giá trị của popup từ prop
            dataContent: this.content,

            dataPrimaryButtonName: this.primaryButtonName,

            dataSecondaryButtonName: this.secondaryButtonName,
            dataOpen: this.open,

            dataType: this.type,
            dataState: this.state,

            resolvePromise: null,
            rejectPromise: null
        };
    },

    computed: {
        getIcon() {
            switch (this.dataType) {
                case "error":
                    return "popup--error";
                case "info":
                    return "popup--info";
                case "warning":
                    return "popup--warning";
                case "success":
                    return "popup--success";
                default:
                    return "popup--info";
            }
        }
    },

    methods: {
        /**
         * @param { object } obj chứa các giá trị của popup { title, content, namePrimaryButton, nameSecondaryButton, primaryColor }
         * Đây là hàm để set các giá trị của popup nhằm hiển thị cho người dùng
         * @returns boolean promise nếu ấn vào thoát (button x hoặc button close) thì trả về false, ấn vào xác nhận (button confirm) thì trả về true
         */
        show({ content, primaryButtonName, secondaryButtonName, type, state }) {
            this.dataContent = content;
            this.dataPrimaryButtonName = primaryButtonName;
            this.dataSecondaryButtonName = secondaryButtonName;
            this.dataOpen = true;

            this.dataType = type;
            this.dataState = state;

            return new Promise((resolve, reject) => {
                this.resolvePromise = resolve;
                this.rejectPromise = reject;
            });
        },
        /**
          * Sự kiên đóng popup
          * Created by: VLVU (19/8/2021)
          */
        closePopup() {
            this.dataOpen = false;
            const resolve = () => this.resolvePromise === null ? null : this.resolvePromise(null);
            resolve();
            this.$emit("onClose");
        },
        /**
         * Sự kiên ấn vào primary button
         * Created by: VLVU (19/8/2021)
         */
        primaryButtonClick() {
            this.dataOpen = false;
            const resolve = () => this.resolvePromise === null ? null : this.resolvePromise(true);
            resolve();
            this.$emit("onPrimaryClick");
        },

        /**
         * Sự kiên ấn vào secondary button
         * Created by: VLVU (19/8/2021)
         */
        secondaryButtonClick() {
            this.dataOpen = false;
            const resolve = () => this.resolvePromise === null ? null : this.resolvePromise(false);
            resolve();
            this.$emit("onSecondaryClick");
        }

    }

};
</script>

<style scoped src="./style.css">
</style>
