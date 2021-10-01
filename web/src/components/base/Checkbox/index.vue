<template>
    <label class="checkbox">
        <input
            type="checkbox"
            v-on="$listeners"
            v-bind="$attrs"
            :checked="value"
            @change="onChange"
            :tabindex="tabindex"
        >
        <span></span>
        <div v-if="label">{{label}}</div>
    </label>
</template>
<script>
export default {
    name: "BaseCheckbox",
    model: {
        prop: "value",
        event: "onChange"
    },
    props: {
        label: {
            type: String,
            default: () => ""
        },

        value: {
            default: () => false
        },
        tabindex: {
            type: String
        }
    },

    methods: {
        onChange(e) {
            this.$emit("onChange", e.target.checked);
        }
    }
};
</script>
<style scoped src="./style.css">
</style>
