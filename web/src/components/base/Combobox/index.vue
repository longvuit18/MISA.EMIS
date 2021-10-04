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
                <div
                    class="chip-group"
                    v-if="multiple"
                    :class="{'combobox-disabled': disabled}"
                    ref="chip"
                >
                    <BaseChip
                        v-for="(item, chipIndex) in chips"
                        :key="item + chipIndex"
                        :value="item"
                        @deleteChip="deleteChip"
                    />

                    <input
                        type="text"
                        :tabindex="tabindex"
                        @input="onChange"
                        v-model="search"
                        @keydown.down="onArrowDown"
                        @keydown.up="onArrowUp"
                        @keydown.tab="onTab"
                        @keydown.enter="onEnter"
                        @blur="onBlur"
                        ref="BaseInput"
                        v-bind="$attrs"
                        :title="errorMessage"
                        :placeholder="placeholder"
                        :disabled="disabled"
                    />
                </div>
                <input
                    v-if="!multiple"
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
                    :disabled="disabled"
                />
                <div
                    v-if="hasAddIcon"
                    class="icon-plus"
                    :class="{'combobox-disabled': disabled}"
                    style="border-right: 1px solid #babec5;"
                    v-tooltip="'Tính năng đang phát triển'"
                >
                    <div class="icon icon-size-16 mi-plus--success">
                    </div>
                </div>
                <div
                    :class="{'combobox-disabled': disabled}"
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
        <portal to="combobox">
            <OptionWrapper
                :style="{...styleOption}"
                v-if="isOpen && !optionsTable"
                class='combobox-options'
                ref="options"
                :scrollPosition="scrollPosition"
            >
                <div
                    v-for="(option) in options"
                    :key="option.value"
                    @click="setResult(option)"
                    class="combobox-result"
                    :class="{ 'is-active':search === option[keyLabel] }"
                >
                    {{ option[keyLabel] }}
                </div>
                <div
                    v-if="options.length === 0"
                    class="combobox-result"
                    @click="isOpen = false"
                >
                    Không có dữ liệu
                </div>
            </OptionWrapper>
            <OptionWrapper
                :style="{...styleOption}"
                v-if="isOpen && optionsTable"
                class='combobox-options'
                ref="options"
                :scrollPosition="scrollPosition"
            >
                <OptionsTable
                    :columnNames="columnNames"
                    :data="options"
                    :multiple="multiple"
                    :selected="rowSelected"
                    @handleClickRow="setResult"
                    :currentCheck="currentCheck"
                    :treeTable="treeTable"
                    :treeColumnId="treeColumnId"
                />
            </OptionWrapper>
        </portal>
    </div>
</template>

<script>
import OptionsTable from "./OptionsTable.vue";
import OptionWrapper from "./OptionWrapper.vue";

/**
 * Cmbobox
 * CreatedBy: VLVU (15/7/2021)
 */

// thông báo lỗi bắt buộc nhập
const ErrorRequire = (name) => `${name} là trường bắt buộc phải nhập!`;
const ErrorInCorrect = (name) => `Dữ liệu <${name}> không có trong danh mục!`;
export default {
    components: { OptionsTable, OptionWrapper },
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

        optionsTable: {
            type: Boolean,
            default: () => false
        },

        multiple: {
            type: Boolean,
            default: () => false
        },

        name: {
            type: String

        },
        placeholder: {
            type: String,
            default: () => ""
        },
        value: {
            type: [String, Number, Array]
        },
        // columnNames là 1 Array chứa object {key: string, text: string, align: string, sort: boolean, format: string, width: number}
        columnNames: {
            type: Array,
            default: () => []
        },

        // Xác định khóa chính của từng option
        optionId: {
            type: String,
            default: () => "key"
        },

        // Xác định key của label hiện thị khi đã chọn được option
        keyLabel: {
            type: String,
            default: () => "key"
        },

        hasAddIcon: {
            type: Boolean,
            default: () => false
        },

        focusInput: {
            type: Boolean,
            default: () => false
        },

        disabled: {
            type: Boolean,
            default: () => false
        },

        treeTable: {
            type: Boolean,
            default: () => false
        },

        /**
         * khi là table dạng cây thì bắt buộc phỉa có trường này đẻ có thể thực hiện map
         */
        treeColumnId: {
            type: String,
            default: "id"
        }
    },
    data() {
        return {
            isOpen: false,
            fixedOptions: this.toOptions(),
            options: this.toOptions(),
            search: this.defaultItem()?.[this.keyLabel],
            arrowCounter: -1,
            error: false,
            errorMessage: "",

            positonCombobox: null,

            currentCheck: null,

            scrollPosition: 0
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
                ? { top: this.top, left: this.left, "min-width": this.width }
                : { bottom: this.bottom, left: this.left, "min-width": this.width };
        },

        /**
         * map dữ liệu cho chip
         * Created by: VLVU (16/9/2021)
         */
        chips() {
            const nameFirstColumn = this.columnNames[0].key;
            return this.options
                .filter(x => this.value.findIndex(i => i === x.optionId) > -1)
                .map(item => ({ key: item.optionId, text: item[nameFirstColumn] }));
        },

        /**
         * những option nào đã được chọn
         * Created by: VLVU(16/9/2021)
         */
        rowSelected() {
            if (this.multiple) {
                return this.options.filter(item => this.value.findIndex(x => x === item.optionId) > -1);
            }
            return [this.options.find(item => item.optionId === this.value)];
        }
    },

    watch: {
        /**
         * lắng nghe mỗi khi isOpen thay đổi để xét lại vị trí của hộp thoại options
         * Created by: VLVU (19/9/2021)
         */
        isOpen(value) {
            this.positonCombobox = this.$refs.combobox.getBoundingClientRect();
            // this.$refs.options.scrollTo(0, this.scrollPosition);
        },

        /**
         * lắng nghe mỗi khi value thay đổi để xét lại vị trí của hộp thoại options
         * Created by: VLVU (19/9/2021)
         */
        value: {
            handler(value) {
                if (value === null || value === undefined) {
                    this.search = "";
                } else {
                    this.search = this.defaultItem()?.[this.keyLabel];
                }
                this.positonCombobox = this.$refs.combobox.getBoundingClientRect();
            },
            deep: true
        },

        /**
         * Khi thay đổi focusInput từ false sang true hoặc ngược lại thì đều fucus input đó
         */
        focusInput() {
            this.$refs.BaseInput.focus();
        },

        items: {
            handler() {
                this.fixedOptions = this.toOptions();
                this.options = this.toOptions();
                this.search = this.defaultItem()?.[this.keyLabel];
            },
            deep: true
        }
    },
    mounted() {
        // focus input nếu focusInput == true (nghĩa là có thể set focus input từ props)
        if (this.focusInput === true) {
            this.$refs.BaseInput.focus();
        }
        window.addEventListener("scroll", this.handleScrollOutSide, true);
        // Lắng nghe sự kiện click ra bên ngoài combobox
        document.addEventListener("click", this.handleClickOutside);
    },
    // xóa sự kiện này khi thoát khỏi xóa component
    destroyed() {
        document.removeEventListener("click", this.handleClickOutside);
        window.removeEventListener("scroll", this.handleScrollOutSide, true);
    },

    methods: {
        /**
         * sự kiến click ra ngoài element
         * Created by: VLVU (19/9/2021)
         */
        handleClickOutside(event) {
            if (!this.$el.contains(event.target) &&
                !this.$refs.options?.$el?.contains(event.target) &&
                !this.$refs.chip?.contains(event.target)
            ) {
                this.isOpen = false;
            }
        },
        /**
         * Sự kiện scoll bên ngoài combobox
         * Created by: VLVU (19/9/2021)
         */
        handleScrollOutSide(event) {
            if (!this.$el.contains(event.target) && !this.$refs.options?.$el?.contains(event.target)) {
                this.isOpen = false;
            }
        },
        /**
         * set giá trị ban đầu combobox
         * Created by: VLVU (19/9/2021)
         */
        defaultItem() {
            return this.toOptions().find(item => item[this.optionId] === this.value);
        },

        /**
         * map lại giá trị cho option
         * Created by: VLVU (19/9/2021)
         */
        toOptions() {
            return this.items.map(item => {
                return {
                    ...item,
                    optionId: item[this.optionId],
                    text: Object.values(item).join(" ")
                };
            });
        },
        /**
         * sự kiện khi người dùng click 1 option
         * Created by: VLVU (19/9/2021)
         */
        setResult(option) {
            this.currentCheck = null;
            if (this.multiple) {
                this.$refs.BaseInput.focus();
                // set laị giá trị cho search
                this.search = "";
                // check xem người dùng click vào hàng mới hay cũ
                const rowIndex = this.value.findIndex(item => item === option.optionId);

                if (rowIndex > -1) {
                    const newValue = this.value;
                    newValue.splice(rowIndex, 1);
                    this.$emit("result", newValue);
                    return;
                }
                // this.result = [...this.result, option];
                this.$emit("result", [...this.value, option.optionId]);
                return;
            }

            // tính toán lại vị trí của scroll khi click vao item
            this.arrowCounter = this.fixedOptions.findIndex(item => item.optionId === option.optionId);
            this.scrollPosition = 32 * (this.arrowCounter);

            this.search = this.displayText(option.optionId);
            this.isOpen = false;
            // chắc chắn người dùng đã chọn
            this.error = false;
            this.errorMessage = "";
            this.$emit("result", option.optionId);
        },

        /**
         * thực hiện lọc cho combobox
         * Created by: VLVU (19/9/2021)
         */
        filterOptions() {
            const filters = this.fixedOptions.filter((item) => {
                return item.text?.toLowerCase()?.indexOf(this.search.toLowerCase()) > -1;
            });

            if (filters.length === 0) {
                this.isOpen = false;
            } else {
                this.isOpen = true;
                this.options = filters;
            }
        },
        /**
         * onChange input
         * Created by: VLVU (19/9/2021)
         */
        onChange() {
            this.filterOptions();
        },

        /**
         * show option
         * Created by: VLVU (19/9/2021)
         */
        showOptions() {
            if (this.disabled) {
                return;
            }
            this.error = false;
            this.options = this.toOptions();
            // vì phải sử lý sự kiện focus nên phải xử lý riêng từng trường hợp của isOpen
            if (!this.isOpen) {
                this.isOpen = true;
                // focus vào input khi click vào icon
                this.$refs.BaseInput.focus();
            } else {
                this.isOpen = false;
            }
        },

        /**
         * sự kiến ấn mũi tên xuống
         * Created by: VLVU (19/9/2021)
         */
        onArrowDown() {
            // tính toán vị trí của scroll trong option
            if (this.items.length < 6) {
                this.scrollPosition = 0;
            } else {
                this.scrollPosition = 32 * (this.arrowCounter + 1);
            }

            if (!this.isOpen) {
                this.isOpen = true;
                return;
            }
            if (this.arrowCounter < this.options.length - 1) {
                this.arrowCounter = this.arrowCounter + 1;
                this.search = !this.multiple ? this.displayText(this.options[this.arrowCounter].optionId) : this.search;
                this.currentCheck = this.options[this.arrowCounter];
            } else {
                this.arrowCounter = 0;
                this.search = !this.multiple ? this.displayText(this.options[0].optionId) : this.search;
                this.currentCheck = this.options[0];
                this.scrollPosition = 0;
            }
        },
        /**
         * sự kiện ấn mũi tên lên
         * Created by: VLVU (19/9/2021)
         */
        onArrowUp() {
            // tính toán vị trí của scroll trong option
            if (this.items.length < 6) {
                this.scrollPosition = 0;
            } else {
                this.scrollPosition = 32 * (this.arrowCounter - 1);
            }
            if (!this.isOpen) {
                this.isOpen = true;
                return;
            }

            if (this.arrowCounter > 0) {
                this.arrowCounter = this.arrowCounter - 1;
            } else {
                this.arrowCounter = this.options.length - 1;
                this.scrollPosition = (this.options.length - 1) * 32;
            }
            this.search = !this.multiple ? this.displayText(this.options[this.arrowCounter].optionId) : this.search;
            this.currentCheck = this.options[this.arrowCounter];
        },
        /**
         * sự kiện ấn enter
         * Created by: VLVU (19/9/2021)
         */
        onEnter() {
            if (this.isOpen && this.options.length !== 0) {
                this.search = !this.multiple ? this.displayText(this.options[this.arrowCounter].optionId) : "";

                // nếu là chọn nhiều thì không cần đóng options
                this.isOpen = this.multiple;
                // chắc chắn người dùng đã chọn
                this.error = false;

                this.$emit("result", !this.multiple ? this.options[this.arrowCounter].optionId : [...this.value, this.options[this.arrowCounter].optionId]);
            }
        },

        /**
         * sự kiện ấn tab
         * Created by: VLVU (19/9/2021)
         */
        onTab() {
            if (this.arrowCounter !== -1) {
                this.search = !this.multiple ? this.displayText(this.options[this.arrowCounter].optionId) : "";

                this.isOpen = false;
                this.$emit("result", !this.multiple ? this.options[this.arrowCounter].optionId : [...this.value, this.options[this.arrowCounter].optionId]);

                // chắc chắn người dùng đã chọn
                this.error = false;
            } else {
                this.isOpen = false;
            }
        },

        /**
         * sự kiện ấn blur ra ngoài
         * Created by: VLVU (19/9/2021)
         */
        onBlur() {
            if (!this.required && !this.search) {
                this.error = false;
                this.errorMessage = "";
                this.$emit("result", undefined);
                return;
            }
            if (this.multiple) {
                if (this.search === "") {
                    this.error = false;
                } else {
                    this.error = true;
                    this.errorMessage = ErrorInCorrect(this.name);
                }
            } else {
                const indexItem = this.options.findIndex(item => this.displayText(item.optionId) === this.search);
                if (indexItem > -1) {
                    this.error = false;
                } else {
                    this.error = true;
                    this.errorMessage = ErrorInCorrect(this.name);
                    if (this.required) {
                        this.errorMessage = ErrorRequire(this.name);
                    }
                }
            }
        },

        /**
         * xóa chip
         * Created by: VLVU (19/9/2021)
         */
        deleteChip(chip) {
            if (this.disabled) {
                return;
            }
            this.value = this.value.filter(item => item !== chip.key);
        },

        /**
         * text hiện thi trên combox khi được chọn
         * Created by: VLVU (19/9/2021)
         */
        displayText(value) {
            return this.options.find(item => item.optionId === value)[this.keyLabel];
        }

    }
};
</script>

<style scoped src="./style.css">
</style>
