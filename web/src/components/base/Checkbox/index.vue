<template>
    <label class="checkbox">
        <input
            type="checkbox"
            v-on="$listeners"
            v-bind="$attrs"
        >
        <span></span>
        <label v-if="label">{{label}}</label>
    </label>
</template>
<script>
export default {
    name: "BaseCheckbox",
    props: {
        label: {
            type: String,
            default: () => ""
        }
    }
};
</script>
<style scoped src="./style.css">
</style>
