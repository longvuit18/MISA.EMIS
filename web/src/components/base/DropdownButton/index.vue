<template>
    <div
        class="base"
        ref="dropdownButton"
    >
        <div
            class="main-button"
            @click="isOpen = !isOpen"
        >
            <button
                v-bind:class="{'secondary-button': secondaryButton, 'color-red': colorRed, 'disabled': disabled}"
                v-on="$listeners"
                v-bind="$attrs"
                :disabled="disabled || loading"
            >
                <img
                    v-if="startIcon"
                    :src="require('../../../assets/icon/'+ startIcon)"
                    alt="start icon"
                >
                <div style="white-space: nowrap;">{{!loading ? buttonName : ''}}</div>
                <BaseSpin v-if="loading" />
                <div style="padding-left:4px;">
                    <div
                        v-if="secondaryButton"
                        class="angle-down-icon--black"
                    />
                </div>
            </button>
        </div>
        <div
            class="dropdown-icon"
            v-if="!secondaryButton"
        >
            <div class="line" />
            <button>
                <div class="angle-down-icon" />
            </button>
        </div>
        <portal to="dropdown-button">
            <div
                class="dropdown-button-options"
                ref="options"
                :style="{...styleOption}"
                v-if="isOpen"
            >
                <slot></slot>
            </div>
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
        }
    },

    data() {
        return {
            isOpen: false,
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
            return this.positionDropdownButton?.left + "px";
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
                ? { top: this.top, right: this.right, width: "auto", "min-width": this.width }
                : { bottom: this.bottom, right: this.right, width: "auto", "min-width": this.width };
        }
    },

    watch: {
        isOpen() {
            console.log(this.positionDropdownButton);
            this.positionDropdownButton = this.$refs.dropdownButton.getBoundingClientRect();
        }
    },
    // Lắng nghe sự kiện click ra bên ngoài combobox
    mounted() {
        window.addEventListener("scroll", this.handleScrollOutSide, true);
        // document.addEventListener("click", this.handleClickOutside);
    },
    // xóa sự kiện này khi thoát khỏi xóa component
    destroyed() {
        // document.removeEventListener("click", this.handleClickOutside);
        window.removeEventListener("scroll", this.handleScrollOutSide, true);
    },
    methods: {
        // // phương thức khi người dùng click ra bên ngoài combobox
        // handleClickOutside(event) {
        //     console.log(this.$root.);
        //     if (!this.$el.contains(event.target) && !this.$refs.options?.contains(event.target)) {
        //         this.isOpen = false;
        //     }
        // },
        // phương thức khi người dùng scroll ở bên ngoài combobox
        handleScrollOutSide(event) {
            if (!this.$el.contains(event.target)) {
                this.isOpen = false;
            }
        }
    }

};
</script>

<style scoped src="./style.css">
</style>
