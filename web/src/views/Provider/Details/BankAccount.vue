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
                focus
                tabindex="10"
                :disabled="disabled"
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
    { key: "bank_account_number", text: "Số tài khoản", width: 150, type: "input" },
    { key: "bank_name", text: "Tên ngân hàng", width: 200, type: "input" },
    { key: "bank_branch_name", text: "Chi nhánh", width: 180, type: "input" },
    { key: "province_or_city", text: "Tỉnh/Tp của nhân hàng", width: 240, type: "input" }
];
export default {
    name: "BankAccount",
    components: { TableCommon },

    model: {
        prop: "value",
        event: "onChangeValue"
    },
    props: {
        value: {
            type: String,
            default: () => "[{\"bank_account_number\": \"\", \"bank_name\": \"\", \"bank_branch_name\": \"\", \"province_or_city\": \"\"}]"
        },
        disabled: {
            type: Boolean,
            default: () => false
        }
    },
    data() {
        return {
            columnNames,
            dataTable: this.toDataTable(),
            focusFirstInput: false
        };
    },

    watch: {
        dataTable: {
            handler(value) {
                this.$emit("onChangeValue", JSON.stringify(value));
            },
            deep: true
        }
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
         * Created by: VLVU(9/9/2021)
         */
        addRow() {
            if (this.disabled) {
                return;
            }
            const newRow = JSON.parse("{\"bank_account_number\": \"\", \"bank_name\": \"\", \"bank_branch_name\": \"\", \"province_or_city\": \"\"}");
            this.dataTable = [...this.dataTable, newRow];
        },
        /**
         * Xóa tất cả các hàng
         * Created by: VLVU (9/9/2021)
         */
        async deleteAllRows() {
            if (this.disabled) {
                return;
            }
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
        },

        toDataTable() {
            const data = (!this.value || this.value === "") ? "[{\"bank_account_number\": \"\", \"bank_name\": \"\", \"bank_branch_name\": \"\", \"province_or_city\": \"\"}]" : this.value;
            return JSON.parse(data);
        }
    }
};
</script>
