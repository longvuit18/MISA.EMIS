<template>
    <div class="combobox">
        <div :class="{'form-item': label}">
            <label v-if="label">{{label}}{{" "}}<span v-if="required"><span style="color: red;">*</span></span></label>
            <div
                class="box"
                :class="{'border-red': error}"
            >
                <input
                    type="text"
                    :tabindex="tabindex"
                    @input="onChange"
                    v-model="search"
                    @keydown.down="onArrowDown"
                    @keydown.up="onArrowUp"
                    @keydown.tab="onTab"
                    @click="showOptions"
                    @keydown.enter="onEnter"
                    @blur="onBlur"
                    ref="BaseInput"
                    v-bind="$attrs"
                    :title="errorMessage"
                    placeholder="Nhập/chọn"
                />
                <div
                    class="combobox-icon"
                    @click="showOptions"
                >
                    <div
                        v-if="!isOpen"
                        class="angle-down-icon"
                    />
                    <div
                        v-else
                        class="angle-up-icon"
                    />
                </div>
                <ul
                    id="combobox-options"
                    v-show="isOpen"
                    style="display: none;"
                    :class="'combobox-options' + '-' + positionOption"
                >
                    <li
                        v-for="(option) in options"
                        :key="option.value"
                        @click="setResult(option)"
                        class="combobox-result"
                        :class="{ 'is-active': result && result.text === option.text }"
                    >
                        {{ option.text }}
                    </li>
                </ul>
            </div>
        </div>
    </div>
</template>

<script>

/**
 * Cmbobox
 * CreatedBy: VLVU (15/7/2021)
 */

// thông báo lỗi bắt buộc nhập
const ErrorRequire = (name) => `${name} là trường bắt buộc phải nhập!`;
export default {
    name: "BaseCombobox",
    props: {
        // là 1 array object {value: string, text: string}
        items: {
            type: Array,
            required: false,
            default: () => []
        },
        label: String,
        tabindex: String,
        defaultItem: {
            type: Object,
            default: () => null
        },

        // vị trí của hộp option có 2 vị trí là top hoặc bottom, default: bottom
        positionOption: {
            type: String,
            default: () => "bottom"
        },

        required: {
            type: Boolean,
            default: () => false
        },

        name: {
            type: String

        }
    },
    data() {
        return {
            isOpen: false,
            options: this.items,
            search: this.defaultItem?.text,
            result: this.defaultItem,
            arrowCounter: -1,
            error: false,
            errorMessage: ""
        };
    },
    watch: {
        defaultItem(newValue) {
            this.result = newValue;
            this.search = newValue?.text || "";
        }
    },
    // Lắng nghe sự kiện click ra bên ngoài combobox
    mounted() {
        document.addEventListener("click", this.handleClickOutside);
    },
    // xóa sự kiện này khi thoát khỏi xóa component
    destroyed() {
        document.removeEventListener("click", this.handleClickOutside);
    },
    methods: {
        // khi người dùng ấn vào 1 kết quả
        setResult(option) {
            this.search = option.text;
            this.isOpen = false;
            this.result = option;
            this.$emit("result", this.result);
            // chắc chắn người dùng đã chọn
            this.error = false;
            this.errorMessage = "";
        },
        // lọc kết quả của options khi người dùng nhập search
        filterOptions() {
            const filters = this.items.filter((item) => {
                return item.text?.toLowerCase()?.indexOf(this.search.toLowerCase()) > -1;
            });

            if (filters.length === 0) {
                this.isOpen = false;
                this.result = null;
            } else {
                this.isOpen = true;
                this.options = filters;
            }
        },
        // khi người dùng nhập thì sẽ bắt đầu lọc
        onChange() {
            this.filterOptions();
        },

        showOptions() {
            this.error = false;
            this.options = this.items;
            // vì phải sử lý sự kiện focus nên phải xử lý riêng từng trường hợp của isOpen
            if (!this.isOpen) {
                this.isOpen = true;
                // focus vào input khi click vào icon
                this.$refs.BaseInput.focus();
            } else {
                this.isOpen = false;
            }
        },

        // phương thức khi người dùng click ra bên ngoài combobox
        handleClickOutside(event) {
            if (!this.$el.contains(event.target)) {
                this.isOpen = false;
            }
        },

        // sự kiện khi người dùng ấn mũi tên xuống
        onArrowDown() {
            if (!this.isOpen) {
                this.isOpen = true;
                return;
            }
            if (this.arrowCounter < this.options.length - 1) {
                this.arrowCounter = this.arrowCounter + 1;
                this.search = this.options[this.arrowCounter].text;
                this.result = this.options[this.arrowCounter];
            } else {
                this.arrowCounter = 0;
                this.search = this.options[0].text;
                this.result = this.options[0];
            }
        },
        // sự kiện khi người dùng ấn mũi tên lên
        onArrowUp() {
            if (!this.isOpen) {
                this.isOpen = true;
                return;
            }

            if (this.arrowCounter > 0) {
                this.arrowCounter = this.arrowCounter - 1;
                this.search = this.options[this.arrowCounter].text;
                this.result = this.options[this.arrowCounter];
            } else {
                this.arrowCounter = this.options.length - 1;
                this.search = this.options[this.arrowCounter].text;
                this.result = this.options[this.arrowCounter];
            }
        },
        // sự kiện khi người dùng ấn enter
        onEnter() {
            if (this.isOpen && this.options.length !== 0) {
                this.search = this.options[this.arrowCounter].text;
                this.result = this.options[this.arrowCounter];
                this.isOpen = false;
                // chắc chắn người dùng đã chọn
                this.error = false;
                this.$emit("result", this.result);
            }
        },

        onTab() {
            if (this.arrowCounter !== -1) {
                this.search = this.options[this.arrowCounter].text;
                this.result = this.options[this.arrowCounter];
                this.isOpen = false;
                this.$emit("result", this.result);

                // chắc chắn người dùng đã chọn
                this.error = false;
            } else {
                this.isOpen = false;
            }
        },

        onBlur() {
            const indexItem = this.items.findIndex(item => item.text === this.search);
            if (indexItem > -1) {
                this.result = this.items[indexItem];
                this.error = false;
            } else {
                this.error = true;
                if (this.required) {
                    this.errorMessage = ErrorRequire(this.name);
                }
            }
            if (!this.result?.value) {
                if (this.required) {
                    this.errorMessage = ErrorRequire(this.name);
                }
                this.error = true;
            }
        }

    }
};
</script>

<style scoped src="./style.css">
</style>
