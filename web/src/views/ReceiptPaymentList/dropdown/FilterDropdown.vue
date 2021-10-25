<template>
    <div class="filter">
        <BaseRow>
            <BaseCol
                :cols="12"
                class="mr-10"
            >
                <BaseCombobox label="Lý do chi" />
            </BaseCol>
        </BaseRow>
        <BaseRow>
            <BaseCol
                :cols="12"
                class="mr-10"
            >
                <BaseCombobox label="Trạng thái ghi nợ" />
            </BaseCol>
        </BaseRow>
        <BaseRow>
            <BaseCol
                :cols="4"
                class="mr-10"
            >
                <BaseCombobox
                    label="Thời gian"
                    :items="time"
                    optionId="value"
                    keyLabel="label"
                    v-model="filter.time"
                />
            </BaseCol>
            <BaseCol
                :cols="4"
                class="mr-10"
            >
                <BaseDatePicker
                    label="Từ ngày"
                    v-model="filter.StartDate"
                    format="DD/MM/YYYY"
                    value-type="YYYY-MM-DD"
                />
            </BaseCol>
            <BaseCol :cols="4">
                <BaseDatePicker
                    label="Đến ngày"
                    v-model="filter.EndDate"
                    format="DD/MM/YYYY"
                    value-type="YYYY-MM-DD"
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
import utils from "../../../utils";

const time = [
    { value: 0, label: "Từ đầu năm đến nay" },
    { value: 1, label: "Hôm nay" },
    { value: 2, label: "Tháng này" },
    { value: 3, label: "Tùy chọn" }
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
            filter: {
                ...this.filterProp,
                StartDate: this.filterProp?.StartDate ?? utils.formatDateValueInput(`${new Date().getFullYear()}-01-01`),
                EndDate: this.filterProp?.EndDate ?? utils.formatDateValueInput(new Date()),
                time: this.filterProp?.time ?? 0
            },
            time
        };
    },

    watch: {
        "filter.time": {
            handler(value) {
                switch (value) {
                    case 0:
                        this.filter.StartDate = utils.formatDateValueInput(`${new Date().getFullYear()}-01-01`);
                        this.filter.EndDate = utils.formatDateValueInput(new Date());
                        return;
                    case 1:
                        this.filter.StartDate = utils.formatDateValueInput(new Date());
                        this.filter.EndDate = utils.formatDateValueInput(new Date());
                        return;
                    case 2:
                        this.filter.StartDate = utils.formatDateValueInput(`${new Date().getFullYear()}-${new Date().getMonth() + 1}-01`);
                        this.filter.EndDate = utils.formatDateValueInput(new Date());
                }
            },
            deep: true
        }
    },

    methods: {
        onFilter() {
            this.$emit("filterValue", this.filter);
        },

        reset() {
            this.filter = {
                time: 0
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
