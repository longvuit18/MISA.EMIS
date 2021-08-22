<template>
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
        {{!loading ? buttonName : ''}}
        <BaseSpin v-if="loading" />
    </button>
</template>

<script>
/**
    Base button
    created by: VLVU (22/7/2021)
 */
export default {
    name: "BaseButton",
    props: {
        buttonName: String,
        startIcon: String,
        secondaryButton: Boolean,
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
        }
    }

};
</script>

<style scoped src="./style.css">
</style>
