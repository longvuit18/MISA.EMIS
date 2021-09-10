<template>
    <BaseRow>
        <BaseCol
            :cols="3"
            style="padding-right: 12px"
        >
            <BaseRow>
                <BaseCol>
                    <BaseCombobox
                        label="Điều khoản thanh toán"
                        tabindex="9"
                    />
                </BaseCol>
            </BaseRow>
            <BaseRow>
                <BaseCol>
                    <BaseCombobox
                        fullWidth
                        tabindex="12"
                        label="Tài khoản công nợ phải trả"
                        name="Tài khoản công nợ phải trả"
                    />
                </BaseCol>
            </BaseRow>
        </BaseCol>
        <BaseCol
            :cols="3"
            style="padding-right: 12px"
        >
            <BaseInput
                fullWidth
                tabindex="10"
                label="Số ngày đươc nợ"
                format="number"
                enterRightToLeft
                name="Số ngày đươc nợ"
            />
        </BaseCol>
        <BaseCol
            :cols="3"
            style="padding-right: 12px"
        >
            <BaseInput
                fullWidth
                tabindex="11"
                label="Số ngày đươc nọ tối đa"
                format="currency"
                name="Số ngày đươc nọ tối đa"
            />
        </BaseCol>
    </BaseRow>
</template>

<script>
export default {
    name: "Terms"
};
</script>
