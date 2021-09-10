<template>
    <BaseRow>
        <BaseCol :cols="6">
            <BaseRow>
                <BaseCol
                    :cols="6"
                    style="padding-right: 12px;"
                >
                    <BaseCombobox label="Vị trí địa lý" />
                </BaseCol>
                <BaseCol :cols="6">
                    <BaseInput label="Vị trí địa lý" />
                </BaseCol>
            </BaseRow>
            <BaseRow>
                <BaseCol
                    :cols="6"
                    style="padding-right: 12px;"
                >
                    <BaseCombobox />
                </BaseCol>
                <BaseCol :cols="6">
                    <BaseInput />
                </BaseCol>
            </BaseRow>
        </BaseCol>
        <BaseCol :cols="6">
            <div class="details-address">
                <div
                    class="flex items-center"
                    style="margin-bottom: 4px;"
                >
                    <div class="label">
                        Địa chỉ giao hàng
                    </div>
                    <div class="ml-10 mr-10">
                        <BaseCheckbox label="Địa chỉ giống địa chỉ nhập" />
                    </div>
                </div>
                <div class="grid">
                    <TableCommon
                        :columnNames="columnNames"
                        v-model="dataTable"
                        @deleteRow="deleteRow"
                        disabledFirstWhiteSpace
                        disabledLastWhiteSpace
                        disabledIndexColumn
                        disabledHeader
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
        </BaseCol>
    </BaseRow>
</template>

<script>
import { mapActions, mapMutations } from "vuex";
import resources from "../../../resources";
import TableCommon from "../../../components/common/Table";

const columnNames = [
    { key: "a", text: "", width: 340, type: "input" }

];
export default {
    name: "Address",
    components: { TableCommon },
    data() {
        return {
            columnNames: columnNames,
            dataTable: [{
                a: ""
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

<style scoped>
.details-address .label {
    font-size: 12px;
    font-weight: bold;
}
</style>
