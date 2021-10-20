<template>
    <div class="filter">
        <BaseRow>
            <BaseCol
                :cols="6"
                class="mr-10"
            >
                <BaseCombobox label="Loại" />
            </BaseCol>
            <BaseCol :cols="6">
                <BaseCombobox label="Nhóm" />
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

export default {
    name: "FilterDropdown",

    props: {
        filterProp: {
            type: Object,
            default: () => { }
        }
        // isOpen: {
        //     type: Boolean,
        //     default: () => false
        // }
    },
    data() {
        return {
            filter: { ...this.filterProp },
            provinces: [],
            districts: [],
            wards: [],
            countryId: "22d23039-6d1b-4a11-8fea-d9e066a39b92",
            provinceId: "",
            districtId: ""
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
                ward_or_commune: ""
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
