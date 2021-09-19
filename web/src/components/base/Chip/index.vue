<template>
    <div class="chip">
        <div class="text">{{value.text}}</div>
        <div
            class="icon icon-size-16 mi-close--small close-icon"
            @click="deleteChip"
        ></div>
    </div>
</template>

<script>
export default {
    props: {
        // giá trị của chip phải có 2 thuốc tính là key và text
        value: {
            type: Object,
            default: () => ({ key: null, text: "" })
        }
    },

    methods: {
        /**
         * xóa chip hiện tại
         * Created byL VLVU (16/9/2021)
         */
        deleteChip() {
            this.$emit("deleteChip", this.value);
        }
    }
};
</script>

<style scoped src="./style.css">
</style>
