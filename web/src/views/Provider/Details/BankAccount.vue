<template>
    <div>
        <div class="grid">
            <TableCommon
                :columnNames="columnNames"
                v-model="dataTable"
                @deleteRow="deleteRow"
                disabledFirstWhiteSpace
                disabledLastWhiteSpace
                disabledIndexColumn
            />
        </div>
        <div class="btn-grid-control flex mt-10">
            <div class="mr-10">
                <BaseButton
                    secondaryButton
                    style="height: 24px;"
                    buttonName="Thêm dòng"
                    @click="addRow"
                />
            </div>
            <div>
                <BaseButton
                    secondaryButton
                    style="height: 24px;"
                    buttonName="Xóa hết dòng"
                    @click="deleteAllRows"
                />
            </div>
        </div>
    </div>
</template>

<script>
import { mapActions, mapMutations } from "vuex";
import resources from "../../../resources";
import TableCommon from "../../../components/common/Table";

const columnNames = [
    { key: "a", text: "Số tài khoản", width: 150, type: "input" },
    { key: "b", text: "Tên ngân hàng", width: 200, type: "input" },
    { key: "c", text: "Chi nhánh", width: 180, type: "input" },
    { key: "d", text: "Tỉnh/Tp của nhân hàng", width: 240, type: "input" }
];
export default {
    name: "BankAccount",
    components: { TableCommon },
    data() {
        return {
            columnNames: columnNames,
            dataTable: [{
                a: "", b: "", c: "", d: ""
            }]
        };
    },

    methods: {

        ...mapMutations("toastMessage", {
            setToast: "setToast"
        }),
        ...mapActions("popup", {
            confirmPopup: "confirmPopup"
        }),
        /**
         * Thêm 1 hàng mới vào bảng
         * Created by: VLVU(9/9/2018)
         */
        addRow() {
            const newRow = { a: "abc", b: "abc", c: "abc", d: "abc", e: "abc", f: "abc" };
            this.dataTable = [...this.dataTable, newRow];
        },
        /**
         * Xóa tất cả các hàng
         * Created by: VLVU (9/9/2021)
         */
        async deleteAllRows() {
            const ok = await this.confirmPopup(resources.popup.deleteAllRows);
            if (!ok) {
                return;
            }
            this.dataTable = [];
        },

        /**
         * @param {number} index vị trí của hàng
         * Xóa 1 hàng
         * Created by: VLVU(9/9/2021)
         */
        deleteRow(index) {
            this.dataTable.splice(index, 1);
        }
    }
};
</script>
