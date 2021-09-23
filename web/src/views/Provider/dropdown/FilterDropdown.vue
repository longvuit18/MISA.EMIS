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
                <BaseCombobox label="Tỉnh/TP" />
            </BaseCol>
            <BaseCol
                :cols="4"
                class="mr-10"
            >
                <BaseCombobox label="Quận/Huyện" />
            </BaseCol>
            <BaseCol :cols="4">
                <BaseCombobox label="Xã phường" />
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
    width: 530px;
    border-radius: 2px;
    border: 1px solid #babec5;
    background-color: #fff;
    padding: 16px 20px 20px;
}
</style>
