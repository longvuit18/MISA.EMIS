<template>
    <div style="height: 100%">
        <table>
            <thead>
                <tr>
                    <th class="first-white-space z-index-header"></th>
                    <th
                        class="first-column-fixed align-center"
                        style="z-index: 14;"
                    >
                        #
                    </th>
                    <th
                        v-for="(columnName) in columnNames"
                        :key="columnName.key"
                    >
                        <div
                            :style="{width: width(columnName.key), 'min-width': width(columnName.key), 'background': '#ECEEF1'}"
                            :class="{'align-center': columnName.align === 'center', 'align-right': columnName.align === 'right'}"
                        >
                            {{columnName.text}}
                        </div>
                    </th>
                    <th
                        class="last-column-fixed align-center"
                        style="z-index:16;"
                    >
                    </th>
                    <th class="last-white-space-1"></th>
                </tr>
            </thead>
            <tbody>
                <tr
                    v-for="(item, rowIndex) in data"
                    @click="(e) => handleClick(e, rowIndex)"
                    :key="rowIndex"
                    @dblclick="(e) => handleDblClickRow(e, item)"
                    @contextmenu="(e) => handleRightClick(e, item)"
                >
                    <th class="first-white-space"></th>
                    <td class="first-column-fixed align-center td-viewer">
                        <div>
                            {{rowIndex + 1}}
                        </div>
                    </td>
                    <td
                        v-for="(value, key) in mapDataFlowHeader(item)"
                        :key="key"
                        :class="{...setAlign(key)}"
                        class="td-viewer"
                    >
                        <div
                            :style="{width: width(key)}"
                            class="width-cell"
                        >
                            <BaseInput
                                v-if="typeCol(key) === 'input' && rowSelected(rowIndex)"
                                fullWidth
                                v-model="item[key]"
                                isTextarea
                                rows="1"
                            />
                            <BaseCombobox
                                v-else-if="typeCol(key) === 'combobox' && rowSelected(rowIndex)"
                                fullWidth
                                :items="comboboxOptions(key)"
                                v-model="item[key]"
                            />
                            <span
                                v-else
                                :title="width(key) ? value : ''"
                            >{{value}}</span>
                        </div>
                    </td>
                    <td class="last-column-fixed align-center td-viewer">
                        <div class="feature-column">
                            <span
                                @click="() =>handleClickDelete(rowIndex)"
                                style="cursor: pointer;"
                            >
                                <div class="delete-icon"></div>
                            </span>
                        </div>

                    </td>
                    <td class="last-white-space-1"></td>

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

export default {
    name: "TableCommon",
    props: {
        // columnNames là 1 Array chứa object {key: string, text: string, align: string, sort: boolean, format: string, width: number}
        columnNames: {
            type: Array
        },
        data: {
            type: Array,
            default: () => null
        }
    },

    data() {
        return {
            // lọc ra những cột nào cần set align
            alignColumns: this.columnNames.filter(item => item.align),
            // lọc ra những cột nào cần set width
            widthColumns: this.columnNames.filter(item => item.width),
            // lọc ra những cột nào cần set fixed,
            rowsSelected: 0
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
            },
            deep: true
        }
    },

    methods: {
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
        handleClick(e, index) {
            e.preventDefault();

            // nếu cho phép chọn nhiều thì cập nhập array không thì chỉ truyền vào 1
            this.rowsSelected = index;
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
         * @param {string} key key của column
         * @returns Kiểu của cell thuộc cột đó là kiểu gì. (vd: input, combobox)
         * Created by: VLVU (9/9/2021)
         */
        typeCol(key) {
            const column = this.columnNames.find((item) => item.key === key);

            if (!column || !column?.type) {
                return "string";
            }

            return column.type;
        },

        /**
         * @param {string} key key của column
         * @returns option của combobox (vd: input, combobox)
         * Created by: VLVU (9/9/2021)
         */
        comboboxOptions(key) {
            const column = this.columnNames.find((item) => item.key === key);

            if (!column || !column?.items) {
                return [];
            }
            return column.items;
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
            this.$emit("deleteRow", index);
        }

    }
};
</script>

<style scoped src="./style.css">
</style>
