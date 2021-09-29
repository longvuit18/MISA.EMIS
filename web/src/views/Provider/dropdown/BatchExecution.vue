<template>
    <ul class="popup-bottom">
        <li
            @click="() =>handleClickDeleteMulti()"
            style="cursor: pointer;"
        >Xóa</li>
        <li style="cursor: pointer;">Gộp</li>
    </ul>
</template>
<script>
export default {
    name: "BatchExecution",

    methods: {
        handleClickDeleteMulti() {
            this.$emit("handleClickDeleteMulti");
        }
    }
};
</script>
<style scoped>
.popup-bottom {
    width: 100px;
    background-color: #fff;
    border-radius: 2px;
    border: 1px solid #c7c7c7;
    list-style: none;
    text-align: left;
}

ul li {
    padding: 5px 10px;
}

ul li:hover {
    background-color: #e8e9ec;
    color: #08bf1e;
    transition: all 0.1s ease;
}
</style>
