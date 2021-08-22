<template>

    <div
        class="dialog"
        v-if="isOpen"
    >
        <div class="dialog-content">
            <slot />
        </div>
    </div>

</template>

<script>
/**
 * Base Dialog
 * Created by: VLVU (17/7/2021)
 */
export default {
    name: "BaseDialog",
    props: {
        isOpen: {
            type: Boolean,
            default: () => false
        }
    }
};
</script>

<style scoped src="./style.css">
</style>
