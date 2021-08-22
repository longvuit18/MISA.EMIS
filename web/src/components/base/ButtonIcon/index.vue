<template>
    <button
        v-bind:class="{'secondary-button': secondaryButton, 'disabled': disabled}"
        v-on="$listeners"
        :disabled="disabled"
    >
        <img
            v-if="icon"
            :src="require('../../../assets/icon/'+ icon)"
            alt="start icon"
        >
        {{!loading ? buttonName : ''}}
        <BaseSpin v-if="loading" />
    </button>
</template>

<script>
/**
 * Base Button Icon
 * Created by: VLVU (22/7/2021)
 */
export default {
    name: "BaseButtonIcon",
    props: {
        buttonName: String,
        icon: String,
        secondaryButton: {
            type: Boolean,
            default: () => false
        },
        disabled: {
            type: Boolean,
            default: () => false
        },
        loading: {
            type: Boolean,
            default: () => false
        }
    },

    methods: {
    }
};
</script>

<style scoped src="./style.css">
</style>
