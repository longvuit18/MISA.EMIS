<template>
    <div
        class="radio-group"
        :class="{'horizontal': horizontal}"
    >
        <BaseRadio
            v-for="(value, key) in values"
            :key="value.key"
            :label="value.text"
            :value="value.value"
            @change="onChangeValue"
            :checked="checked[key]"
            :style="{'margin-right': 20 + 'px'}"
            :disabled="disabled"
        />
    </div>
</template>

<script>

/**
 * Base radio group
 * Created by: VLVU (8/9/2021)
 */
export default {
    name: "BaseRadioGroup",
    props: {
        // chứa các mục cần chọn dạng array object {value, text}
        values: {
            type: Array,
            default: () => []
        },
        // gia tri chọn hiện tại của radio
        value: {
            type: [Number, String]
        },
        // khoảng cách giữa các item
        marginItem: {
            type: Number,
            default: () => 20
        },

        horizontal: {
            type: Boolean,
            default: () => false
        },

        disabled: {
            type: Boolean,
            default: () => false
        }
    },
    model: {
        prop: "value",
        event: "change"
    },

    data() {
        return {
            checked: this.values.map(item => {
                if (item.value === this.value) {
                    return true;
                }
                return false;
            })
        };
    },

    watch: {
        // thay đổi value mỗi khi prop thay đổi
        value: {
            handler(_value) {
                this.checked = this.values.map(item => {
                    // eslint-disable-next-line eqeqeq
                    if (item.value == this.value) {
                        return true;
                    }
                    return false;
                });
            },
            deep: true
        }
    },

    methods: {
        onChangeValue(e) {
            this.$emit("change", e.target.value);
        }
    }
};
</script>

<style scoped>
.horizontal {
    display: flex;
    align-items: center;
}
</style>
