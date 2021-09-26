<template>
    <div ref="option">
        <slot></slot>
    </div>
</template>
<script>
export default {
    name: "Option",

    props: {
        scrollPosition: {
            type: Number,
            default: () => 0
        }
    },

    watch: {
        scrollPosition() {
            this.$refs.option.scrollTo(0, this.scrollPosition);
        }
    },

    mounted() {
        this.$refs.option.scrollTo(0, this.scrollPosition);
    }
};
</script>
