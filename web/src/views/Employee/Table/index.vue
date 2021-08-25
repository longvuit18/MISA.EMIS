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
                        <BaseCheckbox />
                    </th>
                    <th
                        v-for="(columnName,index) in columnNames"
                        :key="columnName.key"
                        :class="{'second-column-fixed z-index-header': index === 0}"
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
                        style="z-index:16; width: 120px; min-width: 120px;"
                    >
                        Chức năng
                    </th>
                    <th class="last-white-space-1"></th>
                    <th class="last-white-space-2"></th>
                </tr>
            </thead>
            <tbody v-if="data && data.length !== 0">
                <tr
                    v-for="(item, rowIndex) in data"
                    :key="rowIndex"
                    @dblclick="(e) => handleDblClickRow(e, item)"
                    @contextmenu="(e) => handleRightClick(e, item)"
                    :class="{'selected': rowSelected(rowIndex), 'z-index-row': showFeature(rowIndex)}"
                >
                    <th class="first-white-space"></th>
                    <td class="first-column-fixed align-center td-viewer">
                        <div>
                            <BaseCheckbox />
                        </div>
                    </td>
                    <td
                        v-for="(value, key, cellIndex) in mapDataFlowHeader(item)"
                        :key="key"
                        :class="{...setAlign(key), 'second-column-fixed': cellIndex === 0}"
                        class="td-viewer"
                    >
                        <div
                            :style="{width: width(key)}"
                            class="width-cell"
                        ><span :title="width(key) ? value : ''">{{value}}</span></div>
                    </td>
                    <td class="last-column-fixed align-center td-viewer">
                        <div class="feature-column">
                            <span
                                @click="() =>handleClickEdit(item)"
                                style="cursor: pointer;"
                            >Sửa</span>
                            <div>
                                <button
                                    @click="handleClickFeature(rowIndex)"
                                    :class="{'hide-border': !showFeature(rowIndex)}"
                                    class="td-viewer"
                                >
                                    <div class="icon-arrow-down"></div>
                                </button>
                            </div>
                            <ul
                                v-if="showFeature(rowIndex)"
                                :class="rowIndex === data.length - 1 || rowIndex === data.length - 2 ? 'popup-top' : 'popup-bottom'"
                            >
                                <li
                                    @click="() =>handleClickReplication(item)"
                                    style="cursor: pointer;"
                                >Nhân bản</li>
                                <li
                                    @click="() =>handleClickDelete(item)"
                                    style="cursor: pointer;"
                                >Xóa</li>
                                <li
                                    v-tooltip.bottom="'Tính năng chưa phát triển'"
                                    style="cursor: pointer;"
                                >Ngừng sử dụng</li>
                            </ul>
                        </div>

                    </td>
                    <td class="last-white-space-1"></td>
                    <td class="last-white-space-2"></td>

                </tr>
            </tbody>
        </table>
        <div
            class="loading"
            style="flex-direction: column;"
            v-show="data && data.length === 0"
        >
            <img
                src="../../../assets/icon/nodata.svg"
                alt=""
            >
            Không có dữ liệu
        </div>
        <div
            class="loading"
            v-show="!data"
        >
            <BaseSpin size="40px" />
        </div>
    </div>
</template>

<script>
import utils from "../../../utils";
/**
 * Table Employee
 * CreatedBy: Vũ Long Vũ 14/7/2021
 */

export default {
    name: "EmployeeTable",
    props: {
        // columnNames là 1 Array chứa object {key: string, text: string, align: string, sort: boolean, format: string, width: number}
        columnNames: {
            type: Array
        },
        // đây là data để bind vào tbody, nó bắt buộc phải có, kiểu dữ liệu có thể null, hoặc array
        dataProps: {
            required: true,
            default: () => null
        },

        // cho phép người dùng có thể chọn nhiều hàng
        allowsMultipleSelection: {
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
            // lọc ra những cột nào cần set fixed,
            data: null,
            rowsSelected: [],

            showedFeature: -1
        };
    },

    watch: {
        // data được update khi dataProps được update
        dataProps() {
            this.data = this.dataProps;
            this.rowsSelected = [];
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
        handleClick(e, index, item) {
            e.preventDefault();
            // gắn lại những hàng đã chon trước đó
            // check xem người dùng click vào hàng mới hay cũ
            const rowIndex = this.rowsSelected.findIndex(row => row === index);

            if (rowIndex > -1) {
                this.rowsSelected.splice(rowIndex, 1);
                this.$emit("click", null);
            } else {
                // nếu cho phép chọn nhiều thì cập nhập array không thì chỉ truyền vào 1
                this.rowsSelected = this.allowsMultipleSelection ? [...this.rowsSelected, index] : [index];
                this.$emit("click", item);
            }
        },

        /**
        * Thực hiện map dữ liệu theo các trường của header
        * CreatedBy: Vũ Long Vũ 14/7/2021
        */
        mapDataFlowHeader(item) {
            const newItem = {};
            this.columnNames.forEach(c => {
                switch (c.format) {
                    case "date":
                        Object.assign(newItem, { [c.key]: utils.formatDateLocal(item[c.key]) });
                        return;
                    case "currency":
                        Object.assign(newItem, { [c.key]: utils.formatCurency(item[c.key]) });
                        return;
                    default:
                        break;
                }

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
         * check xem hàng đó có được chọn không
         * Created by: VLVU (11/8/2021)
         */
        rowSelected(index) {
            return this.rowsSelected.findIndex(i => i === index) > -1;
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
         * @param {number} rowIndex vị trí của row trong bảng
         * sự kiện ấn show all tính năng
         * Created by: VLVU (17/8/2021)
         */
        handleClickFeature(rowIndex) {
            if (this.showedFeature === rowIndex) {
                this.showedFeature = -1;
            } else {
                this.showedFeature = rowIndex;
            }
        },

        /**
         * @param {number} rowIndex vị trí của hàng trong bảng
         * check xem hàng đó có mở hay không
         * Created by: VLVU (17/8/2021)
         */
        showFeature(rowIndex) {
            return rowIndex === this.showedFeature;
        },

        /**
         * sự kiện ấn để sửa row
         * Created by: VLVU (17/8/2021)
         */
        handleClickEdit(item) {
            this.showedFeature = -1;
            this.$emit("handleClickEdit", item);
        },

        /**
         * sự kiện ấn để xóa row
         * Created by: VLVU (17/8/2021)
         */
        handleClickDelete(item) {
            this.showedFeature = -1;
            this.$emit("handleClickDelete", item);
        },

        /**
         * sự kiện ấn để nhân bản row
         * Created by: VLVU (17/8/2021)
         */
        handleClickReplication(item) {
            this.showedFeature = -1;
            this.$emit("handleClickRelication", item);
        }

    }
};
</script>

<style scoped src="./style.css">
</style>
