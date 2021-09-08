<template>
    <div class="base">
        <div class="main-button">
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
                <span>{{!loading ? buttonName : ''}}</span>
                <BaseSpin v-if="loading" />
                <div style="padding-left:4px;">
                    <div
                        v-if="secondaryButton"
                        class="angle-down-icon--black"
                    />
                </div>
            </button>
        </div>
        <div
            class="dropdown-icon"
            v-if="!secondaryButton"
        >
            <div class="line" />
            <button>
                <div class="angle-down-icon" />
            </button>
        </div>
    </div>
</template>

<script>
/**
    Base dropdown button
    created by: VLVU (8/9/2021)
 */
export default {
    name: "BaseDropdownButton",
    props: {
        buttonName: String,
        startIcon: String,
        secondaryButton: {
            type: Boolean,
            default: () => false
        },
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
