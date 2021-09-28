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
                        <BaseCheckbox
                            @click="handleClickCheckboxAll"
                            :value="selectedAll"
                        />
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
                        <div
                            class="filter"
                            style="display: none;"
                        >
                            <div
                                class="icon icon-size-16 mi-header-option"
                                @click="() => showedFilter = index"
                            >
                            </div>
                            <BaseBoxPopup v-if="index === showedFilter">
                                <div
                                    class="base-table-filter"
                                    v-click-outside="(e) => showedFilter = -1"
                                >
                                    <div
                                        class="lock"
                                        v-tooltip="'Tính năng đang phát triển'"
                                    >
                                        Cố định cột này
                                    </div>
                                    <div class="filter-text">
                                        <div class="title">Lọc {{columnName.text}}</div>
                                        <div class="mt-10 mb-10">
                                            <BaseInput
                                                fullWidth
                                                placeholder="Nhập giá trị lọc"
                                                focusInput
                                                v-model="filter[columnName.key]"
                                            />
                                        </div>
                                    </div>
                                    <BaseRow class="justify-space-between">
                                        <BaseButton
                                            secondaryButton
                                            buttonName="Bỏ lọc"
                                            @click="() => discardFilter(columnName.key)"
                                        />
                                        <BaseButton
                                            buttonName="Lọc"
                                            @click="() =>handleFilter()"
                                        />
                                    </BaseRow>
                                </div>
                            </BaseBoxPopup>
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
                    :class="{'z-index-row': rowIndex === showedFeature}"
                >
                    <th class="first-white-space"></th>
                    <td class="first-column-fixed align-center td-viewer">
                        <div>
                            <BaseCheckbox
                                @click="(e) =>handleClickCheckbox(e, rowIndex)"
                                :value="hasCheck(rowIndex)"
                            />
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
                                @click="() =>handleClickView(item)"
                                style="cursor: pointer;"
                            >Xem</span>
                            <div>
                                <button
                                    @click="handleClickFeature(rowIndex)"
                                    :class="{'hide-border': rowIndex  !== showedFeature}"
                                    class="td-viewer"
                                >
                                    <div class="icon-arrow-down"></div>
                                </button>
                            </div>
                            <ul
                                v-if="rowIndex === showedFeature"
                                :class="rowIndex === data.length - 1 || rowIndex === data.length - 2 ? 'popup-top' : 'popup-bottom'"
                            >
                                <li
                                    @click="() =>handleClickEdit(item)"
                                    style="cursor: pointer;"
                                >Sửa</li>
                                <li
                                    @click="() =>handleClickReplication(item)"
                                    style="cursor: pointer;"
                                >Nhân bản</li>
                                <li
                                    @click="() =>handleClickDelete(item)"
                                    style="cursor: pointer;"
                                >Xóa</li>
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
    name: "ProviderTable",
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

        filterProp: {
            type: Object,
            default: () => { }
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
            selectedAll: false,

            showedFeature: -1,
            showedFilter: -1,

            filter: {}
        };
    },

    watch: {
        // data được update khi dataProps được update
        dataProps() {
            this.data = this.dataProps;
            this.rowsSelected = [];
        },

        filterProp: {
            handler(value) {
                this.filter = { ...value };
            },
            deep: true
        }
    },

    methods: {
        /**
         * Hàm bỏ lọc
         * Created by: VLVU (28/9/2021)
         */
        discardFilter(columnName) {
            if (this.filter[columnName]) {
                this.showedFilter = -1;
                this.filter[columnName] = "";
                this.$emit("filterValue", this.filter);
                return;
            }
            this.showedFilter = -1;
        },

        /**
         * Thưc hiện lọc
         * Created by: VLVU (28/9/2021)
         */
        handleFilter() {
            this.showedFilter = -1;
            this.$emit("filterValue", this.filter);
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
         * sự kiện click vào checkbox
         * Created by: VLVU(12/9/2021)
         */
        handleClickCheckbox(e, index) {
            // gắn lại những hàng đã chon trước đó
            // check xem người dùng click vào hàng mới hay cũ
            const rowIndex = this.rowsSelected.findIndex(row => row === index);

            if (rowIndex > -1) {
                this.rowsSelected.splice(rowIndex, 1);
            } else {
                // nếu cho phép chọn nhiều thì cập nhập array không thì chỉ truyền vào 1
                this.rowsSelected = [...this.rowsSelected, index];
            }
            if (this.rowsSelected.length < this.data.length) {
                this.selectedAll = false;
            } else {
                this.selectedAll = true;
            }
            this.$emit("clickCheckbox", this.rowsSelected);
        },

        /**
         * sự kiện click vào checkbox
         * Created by: VLVU(12/9/2021)
         */
        handleClickCheckboxAll() {
            if (this.selectedAll) {
                this.selectedAll = false;
                this.rowsSelected = [];
            } else {
                this.selectedAll = true;
                this.rowsSelected = Array.from({ length: this.data.length }, (_v, k) => k);
            }
            this.$emit("clickCheckbox", this.rowsSelected);
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
        hasCheck(index) {
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
         * @param {number} rowIndex vị trí của column trong bảng
         * sự kiện ấn show filter
         * Created by: VLVU (27/89/2021)
         */
        handleClickShowFilter(columnIndex) {
            if (this.showedFilter === columnIndex) {
                this.showedFilter = -1;
            } else {
                this.showedFilter = columnIndex;
            }
        },

        /**
         * sự kiện ấn để xem row
         * Created by: VLVU (24/9/2021)
         */
        handleClickView(item) {
            this.showedFeature = -1;
            this.$emit("handleClickView", item);
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
<style>
.base-table-filter {
    font-weight: 400;
    font-size: 13px;
    border-radius: 2px;
    box-shadow: 3px 3px 6px #ddd;
    padding: 22px 17px;
    border: 1px solid #babec5;
}
.base-table-filter .lock {
    position: relative;
    padding-left: 30px;
    line-height: 24px;
    cursor: pointer;
    border-bottom: 1px solid #ebedf0;
    padding-bottom: 14px;
    margin-bottom: 14px;
    text-transform: none;
}

.base-table-filter .lock:before {
    content: "";
    position: absolute;
    display: block;
    height: 24px;
    width: 24px;
    top: 0;
    left: 2px;
    background: transparent url("../../../assets/icon/icon.svg") no-repeat -1726px -560px;
}

.base-table-filter.filter-text {
    text-transform: none;
}
</style>
