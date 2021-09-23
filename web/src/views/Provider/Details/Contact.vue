<template>
    <div>
        <BaseRow v-if="providerType === 0">
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
                            :items="prefixItems"
                            v-model="data.prefix"
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
                            v-model="data.organization_contact_name"
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
                            v-model="data.organization_contact_email"
                        />
                    </BaseCol>
                </BaseRow>

                <BaseInput
                    fullWidth
                    tabindex="12"
                    style="width: 50%;"
                    name="Số điện thoại"
                    placeholder="Số điện thoại"
                    v-model="data.organization_contact_phone"
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
                    v-model="data.legal_representative"
                />
            </BaseCol>
        </BaseRow>
        <BaseRow v-show="providerType === 1">
            <BaseCol :cols="6">
                <BaseRow>
                    <BaseCol
                        :cols="12"
                        :padding="0"
                    >
                        <BaseInput
                            fullWidth
                            tabindex="10"
                            label="Thông tin liên hệ"
                            name="Thông tin liên hệ"
                            style="padding-right: 13px;"
                            placeholder="Email"
                            v-model="data.personal_contact_email"
                        />
                    </BaseCol>
                </BaseRow>
                <BaseRow>
                    <BaseCol
                        :cols="6"
                        style="padding: 0 13px 12px 0;"
                    >
                        <BaseInput
                            fullWidth
                            tabindex="11"
                            name="Điện thoại"
                            placeholder="Điện thoại di động"
                            v-model="data.personal_contact_phone"
                        />
                    </BaseCol>
                </BaseRow>
                <BaseRow>
                    <BaseCol
                        :cols="6"
                        style="padding: 0 13px 12px 0;"
                    >
                        <BaseInput
                            fullWidth
                            tabindex="11"
                            name="Điện thoại"
                            placeholder="Điện thoại cố định"
                            v-model="data.personal_contact_telephone"
                        />
                    </BaseCol>
                </BaseRow>

                <BaseInput
                    label="Đại diện theo PL"
                    fullWidth
                    tabindex="12"
                    name="Đại diện theo PL"
                    placeholder="Đại diện theo PL"
                    v-model="data.legal_representative"
                />
            </BaseCol>
            <BaseCol
                :cols="6"
                style="padding-left: 13px;"
            >
                <BaseRow>
                    <BaseCol
                        :cols="12"
                        :padding="0"
                    >
                        <BaseInput
                            fullWidth
                            tabindex="10"
                            label="Thông tin CMND/Thẻ căn cước"
                            name="Thông tin liên hệ"
                            style="padding-right: 13px;"
                            placeholder="Số CMND/Thẻ căn cước"
                            v-model="data.personal_contact_identity_number"
                        />
                    </BaseCol>
                </BaseRow>
                <BaseRow>
                    <BaseCol
                        :cols="6"
                        style="padding: 0 13px 0 0;"
                    >
                        <BaseDatePicker
                            fullWidth
                            tabindex="11"
                            name="Ngày cấp"
                            placeholder="Ngày cấp"
                            v-model="data.personal_contact_identity_date"
                        />
                    </BaseCol>
                </BaseRow>
                <BaseRow>
                    <BaseCol
                        :cols="12"
                        style="padding: 0 13px 0 0;"
                    >
                        <BaseInput
                            fullWidth
                            tabindex="11"
                            name="Nơi cấp"
                            placeholder="Nơi cấp"
                            v-model="data.personal_contact_identity_place"
                        />
                    </BaseCol>
                </BaseRow>
            </BaseCol>
        </BaseRow>
    </div>
</template>

<script>

const prefixItems = [
    { key: "Anh", text: "Anh" },
    { key: "Chị", text: "Chị" },
    { key: "Ông", text: "Ông" },
    { key: "Bà", text: "Bà" },
    { key: "Miss", text: "Miss" },
    { key: "Mr", text: "Mr" },
    { key: "Mrs", text: "Mrs" }
];
export default {
    name: "Contact",
    props: {
        providerType: {
            type: Number,
            default: () => 0
        },

        data: {
            type: Object,
            default: () => { }
        }
    },

    data() {
        return {
            prefixItems
        };
    }
};
</script>
