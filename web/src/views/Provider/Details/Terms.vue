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
                        hasAddIcon
                        tabindex="9"
                        optionsTable
                        :items="terms"
                        :columnNames="termColumnNames"
                        optionId="id"
                        keyLabel="code"
                        v-model="data.payment_term_id"
                        :focusInput="true"
                        :disabled="disabled"
                    />
                </BaseCol>
            </BaseRow>
            <BaseRow>
                <BaseCol>
                    <BaseCombobox
                        v-if="!isCustomer"
                        fullWidth
                        tabindex="12"
                        label="Tài khoản công nợ phải trả"
                        name="Tài khoản công nợ phải trả"
                        :items="accountPayment"
                        optionId="code"
                        keyLabel="code"
                        optionsTable
                        :columnNames="accountColumnNames"
                        v-model="data.pay_account"
                        :disabled="disabled"
                    />
                    <BaseCombobox
                        v-show="isCustomer"
                        fullWidth
                        tabindex="12"
                        label="Tài khoản công nợ phải thu"
                        name="Tài khoản công nợ phải thu"
                        :items="accountReceive"
                        optionId="code"
                        keyLabel="code"
                        optionsTable
                        :columnNames="accountColumnNames"
                        v-model="data.receive_account"
                        :disabled="disabled"
                    />
                </BaseCol>
            </BaseRow>
        </BaseCol>
        <BaseCol
            :cols="3"
            style="padding-right: 12px"
        >
            <BaseRow>
                <BaseInput
                    fullWidth
                    tabindex="10"
                    label="Số ngày đươc nợ"
                    format="number"
                    enterRightToLeft
                    name="Số ngày đươc nợ"
                    v-model.number="data.due_time_debt"
                    :disabled="disabled"
                />
            </BaseRow>
            <BaseRow>
                <BaseCombobox
                    v-if="isCustomer"
                    fullWidth
                    tabindex="12"
                    label="Tài khoản công nợ phải trả"
                    name="Tài khoản công nợ phải trả"
                    :items="accountPayment"
                    optionId="code"
                    keyLabel="code"
                    optionsTable
                    :columnNames="accountColumnNames"
                    v-model="data.pay_account"
                    :disabled="disabled"
                />
            </BaseRow>
        </BaseCol>
        <BaseCol
            :cols="3"
            style="padding-right: 12px"
        >
            <BaseInput
                fullWidth
                tabindex="11"
                label="Số ngày đươc nọ tối đa"
                format="number"
                enterRightToLeft
                name="Số ngày đươc nọ tối đa"
                v-model.number="data.maximize_debt_amount"
                :disabled="disabled"
            />
        </BaseCol>
    </BaseRow>
</template>

<script>

const termColumnNames = [
    { key: "code", text: "Mã điều khoản thanh toán" },
    { key: "name", text: "Tên điều khoản thanh toán" }
];
const terms = [
    { id: "123e4567-e89b-12d3-a456-426614174001", code: "ĐK1", name: "Điều khoản 1" },
    { id: "123e4567-e89b-12d3-a456-426614174002", code: "ĐK2", name: "Điều khoản 2" },
    { id: "123e4567-e89b-12d3-a456-426614174003", code: "ĐK3", name: "Điều khoản 3" }
];

const accountColumnNames = [
    { key: "code", text: "Số tài khoản" },
    { key: "name", text: "Tên tài khoản" }
];
const accountPayment = [
    { code: "331", name: "Phải trả cho người bán" },
    { code: "3348", name: "Phải trả người lao động khác" }
];

const accountReceive = [
    { code: "1122", name: "Ngoại tệ" },
    { code: "131", name: "Phải thu của khách hàng" }
];
export default {
    name: "Terms",

    props: {
        isCustomer: {
            type: Boolean,
            default: () => false
        },

        data: {
            type: Object,
            default: () => { }
        },
        disabled: {
            type: Boolean,
            default: () => false
        }
    },

    data() {
        return {
            terms,
            termColumnNames,
            accountColumnNames,
            accountPayment,
            accountReceive
        };
    }
};
</script>
