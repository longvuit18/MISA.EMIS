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
                        optionId="country_name"
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
                        placeholder="Tỉnh/Thành phố"
                        :items="provinces"
                        optionId="name"
                        keyLabel="name"
                        v-model="data.province_or_city"
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
                        placeholder="Quận/Huyện"
                        :items="districts"
                        optionId="fullName"
                        keyLabel="fullName"
                        v-model="data.district"
                    />
                </BaseCol>
                <BaseCol
                    :cols="6"
                    style="margin-right: 13px; padding-left: 6px"
                >
                    <BaseCombobox
                        tabindex="12"
                        :disabled="disabled"
                        placeholder="Xã/Phường"
                        :items="wards"
                        optionId="fullName"
                        keyLabel="fullName"
                        v-model="data.ward_or_commune"
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
                            v-model="data.is_same_address"
                            :disabled="disabled"
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
import LocationApi from "../../../api/service/location";

const columnNames = [
    { key: "location_name", text: "", width: 340, type: "input" }

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
            dataTable: this.toDataTable(),

            provinces: [],
            districts: [],
            wards: [],
            countryId: "",
            provinceId: "",
            districtId: ""
        };
    },

    watch: {

        "data.is_same_address"(value) {
            if (value) {
                if (this.dataTable.length < 2) {
                    const newRow = JSON.parse(`{"location_name": "${this.data?.address ?? ""}"}`);
                    this.dataTable = [newRow];
                }
            }
        },

        dataTable: {
            handler(value) {
                this.data.shipping_address = JSON.stringify(value);
            },
            deep: true
        },
        async "data.country"() {
            const countryId = this.countries.find(item => item.country_name === this.data.country)?.country_id ?? "";
            this.countryId = countryId;
            try {
                const provincesPromise = await LocationApi.getProvinces(countryId);
                this.provinces = provincesPromise.data;
            } catch (error) {
                console.error(error);
            }
        },
        async "data.province_or_city"() {
            this.wards = [];
            const provinceId = this.provinces.find(item => item.name === this.data.province_or_city)?.id ?? "";
            this.provinceId = provinceId;
            try {
                const districtsPromise = await LocationApi.getDistricts(this.countryId, provinceId);
                this.districts = districtsPromise.data.map(item => ({ ...item, fullName: item.prefix + " " + item.name }));
            } catch (error) {
                console.error(error);
            }
        },

        async "data.district"() {
            const districtId = this.districts.find(item => item.prefix + " " + item.name === this.data.district)?.id ?? "";
            this.districtId = districtId;

            try {
                const districtsPromise = await LocationApi.getWards(this.countryId, this.provinceId, districtId);
                this.wards = districtsPromise.data.map(item => ({ ...item, fullName: item.prefix + " " + item.name }));
            } catch (error) {
                console.error(error);
            }
        }
    },

    async mounted() {
        const countryId = this.countries.find(item => item.country_name === this.data.country)?.country_id ?? "";
        this.countryId = countryId;
        try {
            const provincesPromise = await LocationApi.getProvinces(countryId);
            this.provinces = provincesPromise.data;
            if (this.data?.district) {
                const provinceId = this.provinces.find(item => item.name === this.data.province_or_city)?.id ?? "";
                this.provinceId = provinceId;
                const districtsPromise = await LocationApi.getDistricts(this.countryId, provinceId);
                this.districts = districtsPromise.data.map(item => ({ ...item, fullName: item.prefix + " " + item.name }));
            }
            if (this.data?.ward_or_commune) {
                const districtId = this.districts.find(item => item.prefix + " " + item.name === this.data.district)?.id ?? "";
                this.districtId = districtId;
                const districtsPromise = await LocationApi.getWards(this.countryId, this.provinceId, districtId);
                this.wards = districtsPromise.data.map(item => ({ ...item, fullName: item.prefix + " " + item.name }));
            }
        } catch (error) {
            console.error(error);
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
         * Created by: VLVU(9/9/2018)
         */
        addRow() {
            if (this.disabled) {
                return;
            }
            const newRow = JSON.parse("{\"location_name\": \"\"}");
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
            const data = (!this.data?.shipping_address || this.data?.shipping_address === "") ? "[{\"location_name\": \"\"}]" : this.data?.shipping_address;
            console.log(data);
            return JSON.parse(data);
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
