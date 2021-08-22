<template>
    <div
        class="row"
        v-on="$listeners"
        v-bind="$attrs"
    >
        <slot />
    </div>
</template>

<script>
export default {
    name: "BaseRow"
};
</script>

<style scoped>
.row {
    display: flex;
    width: 100%;
}
</style>
