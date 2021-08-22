<template>
    <img
        class="loading-animation"
        src="../../../assets/icon/loading.svg"
        alt="loadding-icon"
        :style="{width: size, height: size}"
    >
</template>

<script>
export default {
    name: "Spin",
    props: {
        size: {
            type: String,
            default: () => "40px"
        }
    }
};
</script>

<style scoped>
.loading-animation {
    animation: spin 0.7s linear infinite;
}
</style>
