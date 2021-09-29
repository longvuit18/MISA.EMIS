<template>
    <div
        class="base"
        ref="dropdownButton"
    >
        <div class="main-button">
            <button
                v-bind:class="{ 'disabled-secondary-button': disabled && secondaryButton,'secondary-button': !disabled && secondaryButton, 'color-red': colorRed}"
                v-on="$listeners"
                v-bind="$attrs"
                :disabled="disabled"
            >
                <img
                    v-if="startIcon"
                    :src="require('../../../assets/icon/'+ startIcon)"
                    alt="start icon"
                >
                <div style="white-space: nowrap;">{{buttonName}}</div>
                <div style="padding-left:4px;">
                    <div
                        v-if="secondaryButton"
                        class="angle-down-icon--black"
                        :style="styleIconDisabled"
                    />
                </div>
            </button>
        </div>
        <div
            class="dropdown-icon"
            v-if="!secondaryButton"
            @click="onOpenBox"
        >
            <div class="line" />
            <button>
                <div class="angle-down-icon" />
            </button>
        </div>
        <portal to="dropdown-button">
            <transition name="fade">
                <div
                    class="dropdown-button-options"
                    ref="options"
                    :style="{...styleOption}"
                    v-show="isOpen"
                    v-click-outside="handleClickOutside"
                >
                    <slot></slot>
                </div>
            </transition>

        </portal>
    </div>
</template>

<script>
/**
    Base dropdown button
    created by: VLVU (8/9/2021)
 */
export default {
    name: "BaseDropdownButton",
    props: {
        buttonName: String,
        startIcon: String,
        secondaryButton: {
            type: Boolean,
            default: () => false
        },
        colorRed: {
            type: Boolean,
            default: () => false
        },
        loading: {
            type: Boolean,
            default: () => false
        },
        disabled: {
            type: Boolean,
            default: () => false
        },
        // vị trí của hộp option có 2 vị trí là top hoặc bottom, default: bottom
        positionOption: {
            type: String,
            default: () => "bottom"
        },

        isOpen: {
            type: Boolean,
            default: () => false
        }
    },

    data() {
        return {
            positionDropdownButton: null
        };
    },

    computed: {
        /**
         * Lấy vị trí top hiện tại của dropdownButton
         * Created by: VLVU(10/9/2021)
         */
        top() {
            return this.positionDropdownButton?.top + this.positionDropdownButton?.height + 4 + "px";
        },
        /**
         * Lấy vị trí bottom hiện tại của dropdownButton
         * Created by: VLVU(10/9/2021)
         */
        bottom() {
            return `calc(100% - ${this.positionDropdownButton?.top}px + 4px)`;
        },
        /**
         * Lấy vị trí left hiện tại của dropdownButton
         * Created by: VLVU(10/9/2021)
         */
        left() {
            return this.positionDropdownButton?.left + "px";
        },

        /**
         * Lấy vị trí right hiện tại của dropdownButton
         * Created by: VLVU(10/9/2021)
         */
        right() {
            return `calc(100% - ${this.positionDropdownButton?.right}px)`;
        },
        /**
         * Lấy độ dài hiện tại của dropdownButton
         * Created by: VLVU(10/9/2021)
         */
        width() {
            return this.positionDropdownButton?.width + "px";
        },
        /**
         * set style cho option
         * Created by: VLVU(10/9/2021)
         */
        styleOption() {
            return this.positionOption === "bottom"
                ? { top: this.top, right: this.right, width: "auto" }
                : { bottom: this.bottom, right: this.right, width: "auto" };
        },

        styleIconDisabled() {
            return this.disabled ? { opacity: 0.5 } : {};
        }
    },

    watch: {
        isOpen() {
            this.positionDropdownButton = this.$refs.dropdownButton.getBoundingClientRect();
        }
    },
    methods: {
        // phương thức khi người dùng click ra bên ngoài combobox
        handleClickOutside(event) {
            if (this.$refs.dropdownButton?.contains(event.target) || event.target.className === "combobox-result") {
                return;
            }
            this.$emit("close");
        },

        onOpenBox() {
            this.$emit("openBox");
        }
    }

};
</script>

<style scoped src="./style.css">
</style>
