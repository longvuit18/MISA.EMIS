<template>
    <BaseRow>
        <BaseCol
            :cols="6"
            style="padding-right: 26px"
        >
            <BaseRow>
                <BaseCol
                    :cols="6"
                    style="padding: 0 6px 0px 0;"
                >
                    <BaseCombobox
                        label="Vị trí địa lý"
                        :focusInput="true"
                        tabindex="9"
                        :disabled="disabled"
                        placeholder="Nước"
                        :items="countries"
                        optionId="country_id"
                        keyLabel="country_name"
                        v-model="data.country"
                    />
                </BaseCol>
                <BaseCol
                    :cols="6"
                    style="margin: 20px 13px 0 0; padding-left: 6px"
                >
                    <BaseCombobox
                        tabindex="10"
                        :disabled="disabled"
                    />
                </BaseCol>
            </BaseRow>
            <BaseRow>
                <BaseCol
                    :cols="6"
                    style="padding: 0 6px 0px 0;"
                >
                    <BaseCombobox
                        tabindex="11"
                        :disabled="disabled"
                    />
                </BaseCol>
                <BaseCol
                    :cols="6"
                    style="margin-right: 13px; padding-left: 6px"
                >
                    <BaseCombobox
                        tabindex="12"
                        :disabled="disabled"
                    />
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
                        <BaseCheckbox
                            label="Địa chỉ giống địa chỉ nhập"
                            tabindex="13"
                        />
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
                        tabindex="14"
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
    props: {
        disabled: {
            type: Boolean,
            default: () => false
        },
        countries: {
            type: Array,
            default: () => []
        },
        data: {
            type: Object,
            default: () => { }
        }
    },
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
            if (this.disabled) {
                return;
            }
            const newRow = { a: "" };
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
