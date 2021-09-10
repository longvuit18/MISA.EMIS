<template>
    <BaseRow>
        <BaseCol :cols="6">
            <BaseRow>
                <BaseCol
                    :cols="4"
                    :padding="0"
                    style="padding-right: 4px;"
                >
                    <BaseCombobox
                        label="Người liên hệ"
                        tabindex="9"
                        placeholder="Xưng hô"
                    />
                </BaseCol>
                <BaseCol
                    :cols="8"
                    :padding="0"
                >
                    <BaseInput
                        fullWidth
                        tabindex="10"
                        name="Họ và tên"
                        style="margin-top:20px;padding-right: 13px;"
                        placeholder="Họ và tên"
                    />
                </BaseCol>
            </BaseRow>
            <BaseRow>
                <BaseCol style="padding: 0 13px 12px 0;">
                    <BaseInput
                        fullWidth
                        tabindex="11"
                        name="Email"
                        placeholder="Email"
                    />
                </BaseCol>
            </BaseRow>

            <BaseInput
                fullWidth
                tabindex="12"
                style="width: 50%;"
                name="Số điện thoại"
                placeholder="Số điện thoại"
            />
        </BaseCol>
        <BaseCol
            :cols="6"
            style="padding-left: 13px;"
        >
            <BaseInput
                fullWidth
                tabindex="13"
                label="Đại diện theo PL"
                name="Đại diện theo PL"
                placeholder="Đại diện theo PL"
            />
        </BaseCol>
    </BaseRow>
</template>

<script>
export default {
    name: "Contact"
};
</script>
