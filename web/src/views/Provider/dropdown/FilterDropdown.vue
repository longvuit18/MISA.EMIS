<template>
    <div class="filter">
        <BaseRow>
            <BaseCol
                :cols="6"
                class="mr-10"
            >
                <BaseCombobox
                    label="Loại"
                    :items="[{value: null, label: 'Tất cả'},{value: 0, label: 'Tổ chức'}, {value: 1, label: 'Cá nhân'}]"
                    optionId="value"
                    keyLabel="label"
                    v-model="filter.account_object_type"
                />
            </BaseCol>
            <BaseCol :cols="6">
                <BaseCombobox
                    label="Nhóm"
                    name="Nhóm nhà cung cấp"
                    placeholder=""
                    :items="groupProviderData"
                    v-model="filter.account_object_group"
                    :columnNames="columnNamesProriverGroup"
                    optionId="account_object_group_code"
                    keyLabel="account_object_group_name"
                    optionsTable
                />
            </BaseCol>
        </BaseRow>
        <BaseRow>
            <BaseCol
                :cols="6"
                class="mr-10"
            >
                <BaseCombobox label="Tình trạng công nợ" />
            </BaseCol>
            <BaseCol :cols="6">
                <BaseCombobox label="Trạng thái" />
            </BaseCol>
        </BaseRow>
        <BaseRow>
            <BaseCol
                :cols="4"
                class="mr-10"
            >
                <BaseCombobox
                    label="Tỉnh/TP"
                    placeholder="Tỉnh/Thành phố"
                    :items="provinces"
                    optionId="name"
                    keyLabel="name"
                    v-model="filter.province_or_city"
                />
            </BaseCol>
            <BaseCol
                :cols="4"
                class="mr-10"
            >
                <BaseCombobox
                    label="Quận/Huyện"
                    placeholder="Quận/Huyện"
                    :items="districts"
                    optionId="fullName"
                    keyLabel="fullName"
                    v-model="filter.district"
                />
            </BaseCol>
            <BaseCol :cols="4">
                <BaseCombobox
                    label="Xã phường"
                    placeholder="Xã/Phường"
                    :items="wards"
                    optionId="fullName"
                    keyLabel="fullName"
                    v-model="filter.ward_or_commune"
                />
            </BaseCol>
        </BaseRow>
        <BaseRow class="justify-space-between">
            <BaseButton
                secondaryButton
                buttonName="Đặt lại"
                @click="reset"
            />
            <BaseButton
                buttonName="Lọc"
                @click="onFilter"
            />
        </BaseRow>
    </div>
</template>

<script>
import LocationApi from "../../../api/service/location";
import resources from "../../../resources";

const columnNamesProriverGroup = [
    { key: "account_object_group_code", text: "Mã nhóm KH, NCC", width: 170 },
    { key: "account_object_group_name", text: "Tên nhóm KH, NCC", width: 190 }
];
export default {
    name: "FilterDropdown",

    props: {
        filterProp: {
            type: Object,
            default: () => { }
        }
    },

    data() {
        return {
            filter: { ...this.filterProp, account_object_type: this.filterProp?.account_object_type ?? null },
            provinces: [],
            districts: [],
            wards: [],
            countryId: resources.countryIdVN,
            provinceId: "",
            districtId: "",
            columnNamesProriverGroup,
            groupProviderData: [
                { account_object_group_code: "NCC-1", account_object_group_name: "Nhóm nhà cung cấp 1" },
                { account_object_group_code: "NCC-2", account_object_group_name: "Nhóm nhà cung cấp 2" },
                { account_object_group_code: "NCC-3", account_object_group_name: "Nhóm nhà cung cấp 3" },
                { account_object_group_code: "NCC-4", account_object_group_name: "Nhóm nhà cung cấp 4" },
                { account_object_group_code: "NCC-5", account_object_group_name: "Nhóm nhà cung cấp 5" },
                { account_object_group_code: "NCC-6", account_object_group_name: "Nhóm nhà cung cấp 6" },
                { account_object_group_code: "NCC-7", account_object_group_name: "Nhóm nhà cung cấp 7" }
            ]
        };
    },
    watch: {
        async "filter.province_or_city"(value) {
            if (!value) {
                return;
            }
            this.wards = [];
            const provinceId = this.provinces.find(item => item.name === value)?.id ?? "";
            this.provinceId = provinceId;
            try {
                const districtsPromise = await LocationApi.getDistricts(this.countryId, provinceId);
                this.districts = districtsPromise.data.map(item => ({ ...item, fullName: item.prefix + " " + item.name }));
            } catch (error) {
                console.error(error);
            }
        },

        async "filter.district"() {
            const districtId = this.districts.find(item => item.prefix + " " + item.name === this.filter.district)?.id ?? "";
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
        try {
            const provincesPromise = await LocationApi.getProvinces(this.countryId);
            this.provinces = provincesPromise.data;
            if (this.filter?.district) {
                const provinceId = this.provinces.find(item => item.name === this.filter.province_or_city)?.id ?? "";
                this.provinceId = provinceId;
                const districtsPromise = await LocationApi.getDistricts(this.countryId, provinceId);
                this.districts = districtsPromise.data.map(item => ({ ...item, fullName: item.prefix + " " + item.name }));
            }
            if (this.filter?.ward_or_commune) {
                const districtId = this.districts.find(item => item.prefix + " " + item.name === this.filter.district)?.id ?? "";
                this.districtId = districtId;
                const districtsPromise = await LocationApi.getWards(this.countryId, this.provinceId, districtId);
                this.wards = districtsPromise.data.map(item => ({ ...item, fullName: item.prefix + " " + item.name }));
            }
        } catch (error) {
            console.error(error);
        }
    },

    methods: {
        onFilter() {
            this.$emit("filterValue", this.filter);
        },

        reset() {
            this.filter = {
                province_or_city: "",
                district: "",
                ward_or_commune: "",
                account_object_group: ""
            };

            this.districts = [];
            this.wards = [];
        }
    }
};
</script>

<style scoped>
.filter {
    width: 530px;
    border-radius: 2px;
    border: 1px solid #babec5;
    background-color: #fff;
    padding: 16px 20px 20px;
}
</style>
