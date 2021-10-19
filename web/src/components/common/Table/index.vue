<template>
    <div style="height: 100%">
        <table>
            <thead v-if="!disabledHeader">
                <tr>
                    <th
                        class="first-white-space z-index-header"
                        :class="{'header-sticky': headerSticky}"
                        v-if="!disabledFirstWhiteSpace"
                    ></th>
                    <th
                        class="first-column-fixed align-center"
                        :class="{'first-column-fixed': !disabledFirstWhiteSpace,'header-sticky': headerSticky}"
                        style="z-index: 14;"
                        v-if="!disabledIndexColumn"
                    >
                        #
                    </th>
                    <th
                        v-for="(columnName) in columnNames"
                        :key="columnName.key"
                        :class="{'header-sticky': headerSticky}"
                    >
                        <div
                            :style="{width: width(columnName.key), 'min-width': width(columnName.key), 'background': '#ECEEF1'}"
                            :class="{'align-center': columnName.align === 'center', 'align-right': columnName.align === 'right'}"
                        >
                            {{columnName.text}}
                        </div>
                    </th>
                    <th
                        class="align-center"
                        :class="{'last-column-fixed': !disabledLastWhiteSpace, 'header-sticky': headerSticky}"
                        style="z-index:16;"
                    >
                    </th>
                    <th
                        class="last-white-space-1"
                        v-if="!disabledLastWhiteSpace"
                        :class="{'header-sticky': headerSticky}"
                    ></th>
                </tr>
            </thead>
            <tbody>
                <tr
                    v-for="(item, rowIndex) in data"
                    :key="rowIndex"
                    @dblclick="(e) => handleDblClickRow(e, item)"
                    @contextmenu="(e) => handleRightClick(e, item)"
                >
                    <td
                        class="first-white-space"
                        v-if="!disabledFirstWhiteSpace"
                    ></td>
                    <td
                        class="align-center td-viewer"
                        :class="{'first-column-fixed': !disabledFirstWhiteSpace}"
                        v-if="!disabledIndexColumn"
                    >
                        <div>
                            {{rowIndex + 1}}
                        </div>
                    </td>
                    <td
                        v-for="(value, key, cellIndex) in mapDataFlowHeader(item)"
                        :key="key"
                        :class="{...setAlign(key)}"
                        class="td-viewer"
                        @click="(e) => handleClick(e, rowIndex, cellIndex)"
                    >
                        <div
                            :style="{width: width(key)}"
                            class="width-cell"
                        >
                            <BaseInput
                                v-if="!disabled &&mapAttribute(key, 'type', 'string') === 'input' && rowSelected(rowIndex)"
                                fullWidth
                                v-model="item[key]"
                                :enterRightToLeft="mapAttribute(key, 'format', 'string') === 'number' || mapAttribute(key, 'format', 'string') === 'curreny'"
                                :format="mapAttribute(key, 'format', 'string')"
                                rows="1"
                                :focusInput="cellIndex === cellFocus"
                                :tabindex="tabindexNumber + columnNames.length * (rowIndex) + cellIndex"
                                :disabled="disabled"
                            />
                            <BaseCombobox
                                v-else-if="!disabled && mapAttribute(key, 'type', 'string') === 'combobox' && rowSelected(rowIndex)"
                                fullWidth
                                :items="mapAttribute(key, 'items', [])"
                                v-model="item[key]"
                                :focusInput="cellIndex === cellFocus"
                                :tabindex="tabindexNumber + columnNames.length * (rowIndex) + cellIndex"
                                :disabled="disabled"
                                :optionsTable="mapAttribute(key, 'isOptionsTable', false)"
                                :columnNames="mapAttribute(key, 'columnNames', [])"
                                :optionId="mapAttribute(key, 'optionId', 'id')"
                                :keyLabel="mapAttribute(key, 'keyLabel', 'text')"
                                :positionOption="mapAttribute(key, 'positionOption', 'bottom')"
                                @getOption="(option) =>watchDataCombobox(option, rowIndex, cellIndex)"
                            />
                            <span
                                v-else
                                :title="width(key) ? value : ''"
                            >{{mapAttribute(key, 'format', 'string') === 'number' || mapAttribute(key, 'format', 'string') === 'currency' ? formatValue(value) : value}}</span>
                        </div>
                    </td>
                    <td
                        class="align-center td-viewer"
                        :class="{'last-column-fixed': !disabledLastWhiteSpace}"
                    >
                        <div class="feature-column">
                            <span
                                @click="() =>handleClickDelete(rowIndex)"
                                style="cursor: pointer;"
                            >
                                <div class="delete-icon"></div>
                            </span>
                        </div>

                    </td>
                    <td
                        class="last-white-space-1"
                        v-if="!disabledLastWhiteSpace"
                    ></td>

                </tr>
                <tr
                    class="total-row"
                    v-if="hasRowTotal"
                >
                    <td
                        class="first-white-space"
                        v-if="!disabledFirstWhiteSpace"
                    ></td>
                    <td
                        class="align-center td-viewer"
                        :class="{'first-column-fixed': !disabledFirstWhiteSpace}"
                        v-if="!disabledIndexColumn"
                    >
                    </td>
                    <td
                        v-for="(value) in columnNames"
                        :key="value.key"
                        :class="{...setAlign(value.key)}"
                        class="td-viewer"
                    >
                        <div
                            v-if="value.total === true"
                            :style="{width: width(value.key)}"
                            class="width-cell"
                        >
                            {{caculateTotal(value.key)}}
                        </div>
                    </td>
                    <td
                        class="align-center td-viewer"
                        :class="{'last-column-fixed': !disabledLastWhiteSpace}"
                    >
                    </td>
                    <td
                        class="last-white-space-1"
                        v-if="!disabledLastWhiteSpace"
                    ></td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
/**
 * Table Common
 * CreatedBy: Vũ Long Vũ 8/9/2021
 */
import AutoNumeric from "autonumeric";

export default {
    name: "TableCommon",
    props: {
        // columnNames là 1 Array chứa object {key: string, text: string, align: string, sort: boolean, format: string, width: number, type: string}
        columnNames: {
            type: Array
        },
        data: {
            type: Array,
            default: () => null
        },

        disabledFirstWhiteSpace: {
            type: Boolean,
            default: () => false
        },
        disabledLastWhiteSpace: {
            type: Boolean,
            default: () => false
        },

        disabledIndexColumn: {
            type: Boolean,
            default: () => false
        },
        disabledHeader: {
            type: Boolean,
            default: () => false
        },

        headerSticky: {
            type: Boolean,
            default: () => false
        },

        focus: {
            type: Boolean,
            default: () => false
        },
        tabindex: {
            type: [String, Number],
            default: () => "0"
        },

        disabled: {
            type: Boolean,
            default: false
        },

        hasRowTotal: {
            type: Boolean,
            default: () => false
        }
    },

    data() {
        return {
            // lọc ra những cột nào cần set align
            alignColumns: this.columnNames.filter(item => item.align),
            // lọc ra những cột nào cần set width
            widthColumns: this.columnNames.filter(item => item.width),

            rowsSelected: 0,
            // ô cần được focus input
            cellFocus: -1,

            totals: {}
        };
    },

    model: {
        prop: "data",
        event: "getData"
    },

    watch: {
        data: {
            handler(value, oldValue) {
                this.$emit("getData", value);

                if (value.length !== oldValue.length) {
                    this.rowsSelected = value.length - 1;
                    this.cellFocus = 0;
                }
            },
            deep: true
        },

        focus() {
            this.rowsSelected = 0;
            this.cellFocus = 0;
        }
    },

    mounted() {
        if (this.focus) {
            this.rowsSelected = 0;
            this.cellFocus = 0;
        }
    },

    computed: {
        tabindexNumber() {
            return Number(this.tabindex.toString());
        }
    },

    methods: {
        /**
         * Tính toán tổng giá trị trên cột
         * Created by: VLVU (13/10/2021)
         */
        caculateTotal(key) {
            if (this.data.length === 1) {
                this.totals[key] = this.data[0][key];
                this.$emit("getTotal", this.totals);
                return this.formatValue(this.data[0][key]);
            }
            let totalValueColumn = 0;
            this.data.forEach(item => {
                totalValueColumn += item[key];
            });
            this.totals[key] = totalValueColumn;
            this.$emit("getTotal", this.totals);
            return this.formatValue(totalValueColumn);
        },
        /**
         * watch combobox mỗi khi thay đổi
         * Created by: VLVU(13/10/2021)
         */
        watchDataCombobox(comboboxItem, rowIndex, cellIndex) {
            this.$emit("watchDataCombobox", comboboxItem, rowIndex, cellIndex);
        },
        /**
        * Sự kiện khi double click vào 1 row
        * CreatedBy: Vũ Long Vũ 14/7/2021
        */
        handleDblClickRow(e, item) {
            e.preventDefault();
            this.$emit("dblClickRow", item);
        },

        /**
        * Sự kiện khi right click vào 1 row
        * CreatedBy: Vũ Long Vũ 19/7/2021
        */
        handleRightClick(e, item) {
            e.preventDefault();
            this.$emit("rightClick", item, e.pageX, e.pageY);
        },

        /**
        * Sự kiện khi  click vào 1 row
        * CreatedBy: Vũ Long Vũ 19/7/2021
        */
        handleClick(e, index, cellIndex) {
            e.preventDefault();

            // nếu cho phép chọn nhiều thì cập nhập array không thì chỉ truyền vào 1
            this.rowsSelected = index;
            this.cellFocus = cellIndex;
        },

        /**
        *check xem hàng đó có được chọn không
        * Created by: VLVU (11/8/2021)
        */
        rowSelected(index) {
            return this.rowsSelected === index;
        },

        /**
        * Thực hiện map dữ liệu theo các trường của header
        * CreatedBy: Vũ Long Vũ 14/7/2021
        */
        mapDataFlowHeader(item) {
            const newItem = {};
            this.columnNames.forEach(c => {
                Object.assign(newItem, { [c.key]: item[c.key] });
            });
            return newItem;
        },

        /**
        * truyền vào class align khi nó được truyền trong prop columnName
        * CreatedBy: Vũ Long Vũ 17/7/2021
        */
        setAlign(key) {
            const positionAlign = this.alignColumns.findIndex(item => item.key === key);
            if (positionAlign > -1) {
                return { [`align-${this.alignColumns[positionAlign].align}`]: true };
            }

            return "";
        },

        /**
         * hàm này sẽ set width cho column
         * Created by: VLVU (11/8/2021)
         */
        width(key) {
            const totalPadding = 20; // padding của cell
            const positionWidth = this.widthColumns.findIndex(item => item.key === key);
            if (positionWidth > -1) {
                return `${this.widthColumns[positionWidth].width - totalPadding}px`;
            }

            return null;
        },

        /**
        * sort column
        * CreatedBy: Vũ Long Vũ 17/7/2021
        */
        onSort(key) {
            const dataSorted = this.data.sort((a, b) => {
                const lastKeyA = a[key].split(" ").slice(-1).join(" ").toLowerCase();
                const lastKeyB = b[key].split(" ").slice(-1).join(" ").toLowerCase();
                // sắp xếp theo thứ tự bảng chữ cái với unicode
                return lastKeyA.localeCompare(lastKeyB);
            });
            this.data = dataSorted;
        },

        /**
         * @param {number} index vị trí của hàng cần xóa
         * sự kiện ấn để xóa row
         * Created by: VLVU (17/8/2021)
         */
        handleClickDelete(index) {
            if (this.disabled) {
                return;
            }
            this.$emit("deleteRow", index);
        },

        /**
         * kiểm tra focus
         * @param {number} index vị trí của ô cần fucus
         * Created by: VLVU (23/9/2021)
         */
        isFocus(index) {
            return this.cellFocus === index;
        },

        /**
         * Map các attribute khai báo trong column Name cho các cột
         * @param {string} key key của cột đó
         * @param {string} attributeName tên attribute
         * @param {any} defaultValue giá trị mặc định của attibute khi không tìm thấy trong column name
         * Created by: VLVU (11/10/2021)
         */
        mapAttribute(key, attributeName, defaultValue) {
            const column = this.columnNames.find((item) => item.key === key);

            if (!column || !column?.[attributeName]) {
                return defaultValue;
            }
            return column?.[attributeName];
        },

        /**
         * format lại giá trị muốn hiển thị nếu nó là dạng format hoặc currency
         * Created by: VLVU (11/10/2021)
         */
        formatValue(value) {
            const settings = {
                digitGroupSeparator: ".",
                decimalCharacter: ",",
                minimumValue: "0",
                decimalCharacterAlternative: ".",
                allowDecimalPadding: "floats"
            };
            return AutoNumeric.format(value, settings);
        }

    }
};
</script>

<style scoped src="./style.css">
</style>
