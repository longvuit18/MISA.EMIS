<template>
    <div class="filter">
        <BaseCombobox label="Lý lo thu, chi" />
        <BaseCombobox label="Trạng thái ghi sổ" />
        <BaseRow>
            <BaseCol
                :cols="4"
                class="mr-10"
            >
                <BaseCombobox label="Thời gian" />
            </BaseCol>
            <BaseCol
                :cols="4"
                class="mr-10"
            >
                <BaseDatePicker label="Từ ngày" />
            </BaseCol>
            <BaseCol :cols="4">
                <BaseCombobox label="Đến ngày" />
            </BaseCol>
        </BaseRow>
        <BaseRow class="justify-space-between">
            <BaseButton
                secondaryButton
                buttonName="Đặt lại"
            />
            <BaseButton buttonName="Lọc" />
        </BaseRow>
    </div>
</template>

<style scoped>
.filter {
    width: 450px;
    border-radius: 2px;
    border: 1px solid #babec5;
    background-color: #fff;
    padding: 16px 20px 20px;
}
</style>
