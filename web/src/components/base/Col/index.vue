<template>
    <div
        :class="col"
        :style="{'padding': padding + 'px'}"
        v-on="$listeners"
        v-bind="$attrs"
    >
        <slot />
    </div>
</template>

<script>
/**
 * Base Col
 * Chia cột layout
 * Created by: VLVU (19/7/2021)
 */
export default {
    name: "BaseCol",
    props: {
        cols: {
            type: String,
            default: () => 12
        },
        padding: {
            type: Number,
            default: () => 6
        }
    },
    data() {
        return {
            col: `col-${this.cols}`
        };
    }
};
</script>

<style scoped src="./style.css">
</style>
