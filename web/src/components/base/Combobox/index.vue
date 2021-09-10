<template>
    <div
        class="combobox"
        ref="combobox"
    >
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
                    :placeholder="placeholder"
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
            </div>
        </div>
        <portal to="root">
            <ul
                :style="{...styleOption}"
                v-if="isOpen"
                class='combobox-options'
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
        </portal>
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
    model: {
        prop: "value",
        event: "result"
    },
    props: {
        // là 1 array object {value: string, text: string}
        items: {
            type: Array,
            required: false,
            default: () => []
        },
        label: String,
        tabindex: String,

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

        },
        placeholder: {
            type: String,
            default: () => "Nhập/Chọn"
        },
        value: {
            type: [String, Number]
        }
    },
    data() {
        return {
            isOpen: false,
            options: this.items,
            search: this.defaultItem()?.text,
            result: this.defaultItem(),
            arrowCounter: -1,
            error: false,
            errorMessage: "",

            positonCombobox: null
        };
    },

    computed: {
        /**
         * Lấy vị trí top hiện tại của combobox
         * Created by: VLVU(10/9/2021)
         */
        top() {
            return this.positonCombobox?.top + this.positonCombobox?.height + 4 + "px";
        },
        /**
         * Lấy vị trí bottom hiện tại của combobox
         * Created by: VLVU(10/9/2021)
         */
        bottom() {
            return `calc(100% - ${this.positonCombobox?.top}px + 4px)`;
        },
        /**
         * Lấy vị trí left hiện tại của combobox
         * Created by: VLVU(10/9/2021)
         */
        left() {
            return this.positonCombobox?.left + "px";
        },
        /**
         * Lấy độ dài hiện tại của combobox
         * Created by: VLVU(10/9/2021)
         */
        width() {
            return this.positonCombobox?.width + "px";
        },
        /**
         * set style cho option
         * Created by: VLVU(10/9/2021)
         */
        styleOption() {
            return this.positionOption === "bottom"
                ? { top: this.top, left: this.left, width: this.width }
                : { bottom: this.bottom, left: this.left, width: this.width };
        }
    },

    watch: {
        isOpen() {
            this.positonCombobox = this.$refs.combobox.getBoundingClientRect();
        }
    },
    // Lắng nghe sự kiện click ra bên ngoài combobox
    mounted() {
        window.addEventListener("scroll", this.handleScrollOutSide, true);
        document.addEventListener("click", this.handleClickOutside);
    },
    // xóa sự kiện này khi thoát khỏi xóa component
    destroyed() {
        document.removeEventListener("click", this.handleClickOutside);
        window.removeEventListener("scroll", this.handleScrollOutSide, true);
    },
    methods: {
        // phương thức khi người dùng click ra bên ngoài combobox
        handleClickOutside(event) {
            if (!this.$el.contains(event.target)) {
                this.isOpen = false;
            }
        },
        // phương thức khi người dùng scroll ở bên ngoài combobox
        handleScrollOutSide(event) {
            if (!this.$el.contains(event.target)) {
                this.isOpen = false;
            }
        },
        // lấy giá trị mặc định
        defaultItem() {
            return this.items.find(i => i.value === this.value);
        },
        // khi người dùng ấn vào 1 kết quả
        setResult(option) {
            this.search = option.text;
            this.isOpen = false;
            this.result = option;
            this.$emit("result", this.result.value);
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
                this.$emit("result", this.result.value);
            }
        },

        onTab() {
            if (this.arrowCounter !== -1) {
                this.search = this.options[this.arrowCounter].text;
                this.result = this.options[this.arrowCounter];
                this.isOpen = false;
                this.$emit("result", this.result.value);

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
