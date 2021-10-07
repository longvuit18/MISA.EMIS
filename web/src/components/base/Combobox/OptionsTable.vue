<template>
    <div
        style="height: 100%"
        ref="optionTable"
    >
        <table>
            <thead>
                <tr>
                    <th
                        v-for="(columnName) in columnNames"
                        :key="columnName.key"
                    >
                        <div
                            :style="{width: width(columnName.key), 'min-width': width(columnName.key)}"
                            :class="{'align-center': columnName.align === 'center', 'align-right': columnName.align === 'right'}"
                        >
                            {{columnName.text}}
                        </div>
                    </th>
                    <th
                        class="align-center"
                        style="z-index: 14; width: 40px;"
                        v-if="multiple"
                    >
                    </th>
                </tr>
            </thead>
            <tbody v-if="data && data.length !== 0">
                <tr
                    v-for="(item, rowIndex) in mapData"
                    :key="rowIndex"
                    @click="() =>handleClickRow(item[0], rowIndex)"
                    :class="{'is-surfing': multiple && isSurfing(item[0]), 'is-active': !multiple && isSurfing(item[0])}"
                >
                    <td
                        v-for="(value, key, cellIndex) in mapDataFlowHeader(item[0])"
                        :key="key"
                        :class="{...setAlign(key), ['level-' + item[0].level]: cellIndex === 0, 'uppercase': item[1] === 'is-parent' && treeTable}"
                        class="td-viewer"
                    >
                        <div
                            :style="{width: width(key)}"
                            class="width-cell"
                        ><span :title="width(key) ? value : ''">{{value}}</span></div>
                    </td>
                    <td
                        class="align-center td-viewer"
                        style="width: 40px;"
                        v-if="multiple"
                    >
                        <div>
                            <div
                                class="icon icon-size-16 check"
                                v-if="hasCheck(item[0].optionId)"
                            />
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <div
            style="flex-direction: column;"
            v-show="data && data.length === 0"
        >
            Không có dữ liệu hiển thị
        </div>
        <div v-show="!data">
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
    name: "OptionsTable",
    props: {
        // columnNames là 1 Array chứa object {key: string, text: string, align: string, sort: boolean, format: string, width: number}
        columnNames: {
            type: Array
        },
        // đây là data để bind vào tbody, nó bắt buộc phải có, kiểu dữ liệu có thể null, hoặc array, OBJECT PHẢI CÓ 1 TRƯỜNG LÀ rowId
        data: {
            required: true,
            default: () => null
        },
        // cho phép chọn nhiều không
        multiple: {
            type: Boolean,
            default: () => false
        },

        selected: {
            type: Array,
            default: () => []
        },

        currentCheck: {
            type: Object,
            default: () => null
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
            // lọc ra những cột nào cần set align
            alignColumns: this.columnNames.filter(item => item.align),
            // lọc ra những cột nào cần set width
            widthColumns: this.columnNames.filter(item => item.width)
        };
    },

    computed: {
        /**
         * map dữ liệu đầu vào thành dữ liệu có thể biểu diễn dưới dạng cây (nếu cần)
         * prop PHẢI CÓ treeColumn để có thể thực hiện map
         * Created by: VLVU (2/10/2021)
         */
        mapData() {
            if (!this.treeTable) {
                return this.data.map(item => {
                    return [item, "is-parent"];
                });
            }
            let result = [];

            let levelCount = 0;
            while (true) {
                const dataLevelN = this.data.filter(item => item.level === levelCount);
                if (dataLevelN.length === 0) {
                    break;
                }
                // level 0 là lấy luôn
                if (levelCount === 0) {
                    result = [...dataLevelN];
                    levelCount++;
                    continue;
                }

                dataLevelN.forEach(item => {
                    const indexParent = result.findIndex(r => r[this.treeColumnId] === item.parent_id);
                    if (indexParent === -1) {
                        result.push(item);
                        return;
                    }

                    result = [...result.slice(0, indexParent + 1), item, ...result.slice(indexParent + 1)];
                });
                levelCount++;
            }

            result = result.map((item, index, array) => {
                if (index === array.length - 1) {
                    return [item, "is-children"];
                }

                if (array[index + 1].parent_id === item[this.treeColumnId]) {
                    return [item, "is-parent"];
                };

                return [item, "is-children"];
            });

            return result;
        }
    },

    methods: {
        /**
         * sự kiện click vào row
         * @param {string} rowId id của row đó
         * @param {number} index index của row đó
         * Created by: VLVU(15/9/2021)
         */
        handleClickRow(item, index) {
            this.$emit("handleClickRow", item, index);
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
        hasCheck(optionId) {
            return this.selected?.findIndex(item => item?.optionId === optionId) > -1;
        },

        /**
         * check xem hàng có đang được lướt qua hay không
         * Created by: VLVU (16/9/2021)
         */
        isSurfing(item) {
            return (this.currentCheck && this.currentCheck?.optionId === item.optionId) ||
                (!this.currentCheck && this.selected[0]?.optionId === item.optionId);
        },

        /**
         * @param {number} rowIndex vị trí của hàng trong bảng
         * check xem hàng đó có mở hay không
         * Created by: VLVU (17/8/2021)
         */
        showFeature(rowIndex) {
            return rowIndex === this.showedFeature;
        }

    }
};
</script>

<style scoped >
table {
    -moz-user-select: none;
    /* Firefox */
    user-select: none;
    /* Internet Explorer */
    -khtml-user-select: none;
    /* KHTML browsers (e.g. Konqueror) */
    -webkit-user-select: none;
    /* Chrome, Safari, and Opera */
    -webkit-touch-callout: none;
    /* Disable Android and iOS callouts*/
    box-sizing: border-box;
    border-spacing: 0;
}

table thead th {
    background-color: #f4f5f8 !important;
    white-space: nowrap;
    position: sticky;
    position: -webkit-sticky;
    top: -2px;
    z-index: 13;
    padding: 0px 10px;
    height: 32px;
}

table th,
td {
    text-align: left;
    white-space: nowrap;
}

table td {
    text-align: left;
    white-space: nowrap;
    padding: 5px 10px;
    height: 32px;
}

table tbody tr {
    background-color: #fff;
}

table tbody tr:hover {
    background-color: #ebedf0 !important;
    color: #35bf22;
    font-weight: 700;
}

table tbody tr:hover .td-viewer {
    background-color: #ebedf0 !important;
    color: #35bf22;
    font-weight: 700;
}

table tbody tr {
    position: relative;
}

.align-center {
    text-align: center !important;
}

.align-right {
    text-align: right !important;
}

.align-left {
    text-align: left !important;
}

.loading {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 40%;
    position: fixed;
    width: calc(100% - 180px);
}

.width-cell {
    overflow: hidden;
    text-overflow: ellipsis;
}

.selected {
    background-color: #019160 !important;
    color: #fff;
}

.first-white-space {
    position: sticky;
    position: -webkit-sticky;
    left: 0;
    top: -5px;
    z-index: 12;
    border: 0;
    background-color: #fff !important;
}

.first-column-fixed {
    position: sticky;
    position: -webkit-sticky;
    top: -2px;
    left: 0;
    z-index: 9;
    background-color: #fff;
}

.last-column-fixed {
    position: sticky;
    position: -webkit-sticky;
    right: 20px;
    z-index: 9;
    border-left: 1px dotted #c7c7c7;
    border-right: 0;
    background-color: #fff;
}

.z-index-header {
    z-index: 16;
}

.z-index-row {
    z-index: 10;
}

.feature-column {
    position: relative;
    display: flex;
    justify-content: center;
    align-items: center;
}

.feature-column .popup-top {
    background-color: #fff;
    border-radius: 2px;
    border: 1px solid #c7c7c7;
    position: absolute;
    z-index: 13;
    bottom: 30px;
    right: 20px;
    list-style: none;
    text-align: left;
}

.feature-column .popup-bottom {
    background-color: #fff;
    border-radius: 2px;
    border: 1px solid #c7c7c7;
    position: absolute;
    z-index: 13;
    top: 30px;
    right: 20px;
    list-style: none;
    text-align: left;
}

.feature-column ul li {
    padding: 5px 10px;
}

.feature-column ul li:hover {
    background-color: #e8e9ec;
    color: #08bf1e;
    transition: all 0.1s ease;
}

.feature-column > div {
    width: 46px;
    height: 36px;
    display: flex;
    justify-content: center;
    align-items: center;
    color: #0075c0;
    font-weight: 600;
}

.feature-column > span {
    color: #0075c0;
    font-weight: 600;
}

.feature-column .icon-arrow-down {
    width: 16px;
    height: 16px;
    min-height: 16px;
    min-width: 16px;
    background: url("../../../assets//icon//icon.svg") no-repeat;
    background-position: -896px -359px;
    cursor: pointer;
}

.feature-column .hide-border {
    border: none;
}

button {
    border-spacing: 0;
    margin: 0;
    padding: 0;
    width: 26px;
    display: flex;
    justify-content: center;
    background-color: #fff;
    border: 1px solid #0075c0;
}

.check {
    background-position: -896px -312px;
}

.is-surfing {
    background-color: #ebedf0 !important;
    color: #35bf22;
    font-weight: 700;
}

.is-active {
    background-color: #2ca01c;
    color: white;
}

.level-1 {
    padding-left: 15px !important;
}

.level-2 {
    padding-left: 30px !important;
}

.level-3 {
    padding-left: 45px !important;
}

.level-4 {
    padding-left: 60px !important;
}

.level-5 {
    padding-left: 75px !important;
}

.uppercase {
    font-weight: bold;
}
</style>
