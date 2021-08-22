<template>
    <div
        class="toast-message"
        :style="{...durationAnimation, border: setBorderColor(currentToast)}"
        v-if="currentToast"
    >
        <div class="toast-icon">
            <div
                v-if="currentToast.type === 'success'"
                :class="setColor(currentToast)"
            />
            <div
                v-if="currentToast.type === 'warning'"
                :class="setColor(currentToast)"
            />
            <div
                v-if="currentToast.type === 'info'"
                :class="setColor(currentToast)"
            />
            <div
                v-if="currentToast.type === 'error'"
                :class="setColor(currentToast)"
            />
        </div>
        <div class="toast-content">{{currentToast.content}}</div>
        <div
            v-if="hasCloseButton"
            class="toast-close"
            :class="setColor(currentToast)"
            @click="onClose"
        >
            <div icon="times" />
        </div>
    </div>
</template>

<script>
import { DURATION_TOAST_MESSAGE } from "../../../resources";
export default {

    name: "ToastItem",
    props: {
        toast: {
            type: Object
        },
        hasCloseButton: {
            type: Boolean,
            default: false
        }
    },

    data() {
        return {
            durationAnimation: {},
            currentToast: this.toast,
            setTimeoutId: null
        };
    },
    created() {
        this.currentToast = this.toast;
        this.setTimeoutId = setTimeout(() => {
            this.currentToast = null;
            this.$emit("removeToast");
        }, this.toast?.duration || DURATION_TOAST_MESSAGE);
        // set thời gian delay cho toast -0.5s là do thời gian animation fadeOut mất 0.5s
        this.durationAnimation = {
            animation: `slideInLeft ease 0.3s, fadeOut linear 0.5s ${((this.toast?.duration || DURATION_TOAST_MESSAGE - 500) / 1000).toFixed(2)}s forwards`
        };
    },

    methods: {
        setColor(toast) {
            switch (toast.type) {
                case "error":
                    return "toast--error";
                case "info":
                    return "toast--info";
                case "warning":
                    return "toast--warning";
                case "success":
                    return "toast--success";
                default:
                    return "toast--info";
            }
        },

        setBorderColor(toast) {
            switch (toast.type) {
                case "error":
                    return "1px solid rgb(255, 119, 119)";
                case "info":
                    return "1px solid #0075C0";
                case "warning":
                    return "1px solid #FCE100";
                case "success":
                    return "1px solid #2ca01c";
                default:
                    return "1px solid #0075C0";
            }
        },

        onClose() {
            this.currentToast = null;
            this.$emit("removeToast");
            clearTimeout(this.setTimeoutId);
        }
    }
};
</script>

<style scoped src="./style.css">
</style>
