<template>
    <label class="radio">
        <input
            type="radio"
            v-on="$listeners"
            v-bind="$attrs"
            :tabindex="tabindex"
        >
        <span v-if="label">{{label}}</span>
    </label>
</template>
<script>
export default {
    name: "BaseCheckbox",
    props: {
        label: {
            type: String,
            default: () => ""
        },
        tabindex: {
            type: String
        }
    }
};
</script>
<style scoped src="./style.css">
</style>
