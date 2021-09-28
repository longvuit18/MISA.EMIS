<template>
    <div ref="box-popup">
        <portal to="box-popup">
            <div
                :style="{...styleOption}"
                v-on="$listeners"
                v-bind="$attrs"
                class="content"
                ref="content"
            >
                <slot></slot>
            </div>
        </portal>
    </div>
</template>

<script>
export default {
    name: "BaseBoxPopup",
    props: {
        positionOption: {
            type: String,
            default: () => "bottom"
        }
    },
    data() {
        return {
            position: null

        };
    },

    watch: {
        /**
         * lắng nghe mỗi khi isOpen thay đổi để xét lại vị trí của hộp thoại options
         * Created by: VLVU (19/9/2021)
         */
        // isOpen(value) {
        //     this.position = this.$refs?.["box-popup"].getBoundingClientRect();
        //     // this.$refs.options.scrollTo(0, this.scrollPosition);
        // }
    },

    computed: {
        /**
         * Lấy vị trí top hiện tại của combobox
         * Created by: VLVU(10/9/2021)
         */
        top() {
            return this.position?.top + this.position?.height + 4 + "px";
        },
        /**
         * Lấy vị trí bottom hiện tại của combobox
         * Created by: VLVU(10/9/2021)
         */
        bottom() {
            return `calc(100% - ${this.position?.top}px + 4px)`;
        },
        /**
         * Lấy vị trí right hiện tại của dropdownButton
         * Created by: VLVU(10/9/2021)
         */
        right() {
            return `calc(100% - ${this.position?.right}px)`;
        },
        /**
         * Lấy vị trí left hiện tại của combobox
         * Created by: VLVU(10/9/2021)
         */
        left() {
            return this.position?.left + "px";
        },
        /**
         * Lấy độ dài hiện tại của combobox
         * Created by: VLVU(10/9/2021)
         */
        width() {
            return this.position?.width + "px";
        },
        /**
         * set style cho option
         * Created by: VLVU(10/9/2021)
         */
        styleOption() {
            return this.positionOption === "bottom"
                ? { top: this.top, right: this.right }
                : { bottom: this.bottom, right: this.right };
        }
    },
    mounted() {
        this.position = this.$refs?.["box-popup"].getBoundingClientRect();
    }

};
</script>
<style scoped>
.content {
    position: absolute;
    z-index: 500;
    background: #fff;
    min-width: 350px;
    width: -webkit-fit-content;
    width: -moz-fit-content;
    width: fit-content;
}
</style>
