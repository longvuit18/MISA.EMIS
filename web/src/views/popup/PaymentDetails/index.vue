<template>
    <BaseDialog isOpen>
        <div class="receipt-details">
            <div class="header">
                <div class="header-left">
                    <div class="icon icon-size-24 mi-recent-log mr-10"></div>
                    <div class="title">Phiếu chi {{data.refno_finance}}</div>
                    <div>
                        <BaseCombobox
                            class="payment-list"
                            :items="paymentTypes"
                            optionId="key"
                            keyLabel="label"
                            v-model="paymentType"
                        />
                    </div>
                </div>
                <div class="header-right">
                    <div class="tour-icon"></div>
                    <div class="tutorial">
                        Hướng dẫn
                    </div>
                    <div
                        class="icon icon-size-24 mi-setting__nav ml-10 mr-10"
                        v-tooltip="'Tính năng chưa phát triển'"
                    ></div>
                    <div
                        class="helper-button"
                        v-tooltip="'Tính năng đang phát triển'"
                    ></div>
                    <div
                        @click="$router.back()"
                        class="close-button"
                        v-tooltip="'Đóng'"
                    ></div>
                </div>
            </div>
            <div class="content">
                <div class="main-information">
                    <BaseRow>
                        <BaseCol :cols="9">
                            <BaseRow>
                                <BaseCol :cols="9">
                                    <BaseRow>
                                        <BaseCol :cols="5">
                                            <BaseCombobox
                                                label="Đối tượng"
                                                tabindex="1"
                                                fullWidth
                                                focusInput
                                                hasAddIcon
                                                :items="accountObjects"
                                                optionsTable
                                                optionId="account_object_id"
                                                keyLabel="account_object_name"
                                                :columnNames="columnNamesObject"
                                                v-model="data.account_object_id"
                                            />
                                        </BaseCol>
                                        <BaseCol
                                            :cols="7"
                                            style="padding: 0 16px  0 12px;"
                                        >
                                            <BaseInput
                                                label="Người nhận"
                                                tabindex="2"
                                                fullWidth
                                                v-model="data.account_object_name"
                                            />
                                        </BaseCol>
                                    </BaseRow>
                                    <BaseRow>
                                        <BaseCol style="padding-right:16px;">
                                            <BaseInput
                                                label="Địa chỉ"
                                                tabindex="3"
                                                fullWidth
                                                v-model="data.account_object_address"
                                            />
                                        </BaseCol>
                                    </BaseRow>
                                    <BaseRow>
                                        <BaseCol style="padding-right:16px;">
                                            <BaseInput
                                                label="Lý do nộp"
                                                tabindex="4"
                                                fullWidth
                                                v-model="data.journal_memo"
                                            />
                                        </BaseCol>
                                    </BaseRow>
                                    <BaseRow>
                                        <BaseCol :cols="5">
                                            <BaseCombobox
                                                label="Nhân viên"
                                                tabindex="5"
                                                fullWidth
                                                hasAddIcon
                                                optionsTable
                                                :columnNames="columnNamesEmployee"
                                                optionId="employee_id"
                                                keyLabel="employee_name"
                                                :items="employees"
                                                v-model="data.employee_id"
                                            />
                                        </BaseCol>
                                        <BaseCol
                                            :cols="7"
                                            style="padding: 0 16px  0 12px;"
                                        >
                                            <div style="display: flex;">
                                                <BaseInput
                                                    label="Kèm theo"
                                                    tabindex="6"
                                                    enterRightToLeft
                                                    placeholder="Số lượng"
                                                    v-model="data.document_included"
                                                />
                                                <span style="padding: 28px 6.5px 0;">
                                                    chứng từ gốc
                                                </span>
                                            </div>
                                        </BaseCol>
                                    </BaseRow>
                                    <BaseRow>
                                        <BaseCol>
                                            <div>Tham chiếu ...</div>
                                        </BaseCol>
                                    </BaseRow>
                                </BaseCol>
                                <BaseCol :cols="3">
                                    <div class="main-center">
                                        <BaseRow>
                                            <BaseCol>
                                                <BaseDatePicker
                                                    label="Ngày hạch toán"
                                                    fullWidth
                                                    tabindex="7"
                                                    format="DD/MM/YYYY"
                                                    value-type="YYYY-MM-DD"
                                                    v-model="data.posted_date"
                                                />
                                            </BaseCol>
                                        </BaseRow>
                                        <BaseRow>
                                            <BaseCol>
                                                <BaseDatePicker
                                                    label="Ngày phiếu thu"
                                                    fullWidth
                                                    tabindex="8"
                                                    format="DD/MM/YYYY"
                                                    value-type="YYYY-MM-DD"
                                                    v-model="data.refdate"
                                                />
                                            </BaseCol>
                                        </BaseRow>
                                        <BaseRow>
                                            <BaseCol>
                                                <BaseInput
                                                    label="Số phiêu chi"
                                                    fullWidth
                                                    tabindex="9"
                                                    v-model="data.refno_finance"
                                                />
                                            </BaseCol>
                                        </BaseRow>
                                    </div>
                                </BaseCol>
                            </BaseRow>
                        </BaseCol>
                        <BaseCol :cols="3">
                            <div class="total-money">
                                <div class="money-title">Tổng tiền</div>
                                <div class="total">0,0</div>
                            </div>
                        </BaseCol>
                    </BaseRow>
                </div>

                <div class="acc-header">
                    <div class="title">Hạch toán</div>
                    <div class="curreny-option">
                        <div class="currency-type">
                            <div class="label mr-10">Loại tiền</div>
                            <div>
                                <BaseCombobox
                                    style="width: 100px"
                                    optionsTable
                                    positionOption="bottom-left"
                                    :columnNames="columnNamesCurrency"
                                    :items="currencies"
                                    optionId="currency_id"
                                    keyLabel="currency_name"
                                    v-model="data.currency_id"
                                />
                            </div>
                        </div>
                        <div
                            class="exchange"
                            v-if="data.currency_id !== 'VND'"
                        >
                            <div class="label mr-10">Tỉ giá</div>
                            <div style="width: 100px">
                                <BaseInput
                                    fullWidth
                                    format="currency"
                                    placeholder="1,00"
                                    v-model="data.exchange_rate"
                                />
                            </div>
                        </div>

                    </div>
                </div>
                <div class="grid">

                    <TableCommon
                        :columnNames="columnNames"
                        v-model="dataDetails"
                        @watchDataCombobox="customDataDetails"
                        @deleteRow="deleteRow"
                    />
                </div>
                <div class="grid-control">
                    <div class="btn-grid-control flex">
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
                    <div class="upload">
                        <div class="title flex">
                            <div class="icon icon-size-18 mi-attach"></div>
                            <div style="margin:0 15px 0 8px;font-weight: bold;">Đính kèm</div>
                            <div style="color: #757575;">Dung lượng tối đa 5MB</div>
                        </div>
                        <div class="drop-upload flex justify-center items-center">
                            Kéo/thả tệp vào đây hoặc bấm vào đây
                        </div>
                    </div>
                </div>

            </div>
            <div class="footer">
                <div>
                    <BaseButton
                        buttonName="Hủy"
                        secondaryButton
                        :style="{background: 'inherit', color: '#fff'}"
                    />
                </div>
                <div class="footer-right">
                    <div class="btn-first">
                        <BaseButton
                            buttonName="Cất"
                            secondaryButton
                            :style="{background: 'inherit', color: '#fff'}"
                        />
                    </div>
                    <BaseButton buttonName="Cất và thêm" />
                </div>
            </div>
            <div
                class="overlay"
                v-if="loading"
            >
            </div>
            <div
                class="loading"
                v-if="loading"
            >
                <BaseSpin />
            </div>
        </div>
    </BaseDialog>
</template>

<script>
import { mapActions, mapMutations } from "vuex";
import TableCommon from "../../../components/common/Table";
import resources from "../../../resources";
import utils from "../../../utils";
import ProviderApi from "../../../api/service/provider";
import EmployeeApi from "../../../api/service/employee";

const debitAccount = [
    { account_number: "10000", account_name: "Tài khoản nợ 1" },
    { account_number: "10001", account_name: "Tài khoản nợ 2" },
    { account_number: "10002", account_name: "Tài khoản nợ 3" }

];

const creditAccount = [
    { account_number: "11111", account_name: "Tài khoản có 1" },
    { account_number: "11112", account_name: "Tài khoản có 2" },
    { account_number: "11113", account_name: "Tài khoản có 3" }

];

const columnNameAccount = [
    { key: "account_number", text: "Số tài khoản" },
    { key: "account_name", text: "Tên tài khoản" }
];

const columnNameBankAccount = [
    { key: "bank_number", text: "Số tài khoản" },
    { key: "bank_name", text: "Tên tài khoản" },
    { key: "bank_branch_name", text: "Chi nhánh" }
];

const columnNamesObject = [
    { key: "account_object_code", text: "Mã đối tượng", width: 120 },
    { key: "account_object_name", text: "Tên đối tượng", width: 250 },
    { key: "tax_code", text: "Mã sô thuế", width: 100 },
    { key: "address", text: "Địa chỉ", width: 300 },
    { key: "phone_number", text: "Điện thoại", width: 100 }
];
const columnNames = [
    { key: "description", text: "Diễn giải", width: 320, type: "input" },
    {
        key: "debit_account",
        text: "Tài khoản nợ",
        width: 150,
        type: "combobox",
        items: debitAccount,
        optionId: "account_number",
        keyLabel: "account_number",
        columnNames: columnNameAccount,
        isOptionsTable: true
    },
    {
        key: "credit_account",
        text: "Tài khoản có",
        width: 175,
        type: "combobox",
        items: creditAccount,
        optionId: "account_number",
        keyLabel: "account_number",
        columnNames: columnNameAccount,
        isOptionsTable: true
    },
    { key: "amount", text: "Số tiền", width: 150, align: "right", type: "input", format: "currency" },
    {
        key: "account_object_code",
        text: "Đối tượng",
        width: 120,
        type: "combobox",
        items: [],
        optionId: "account_object_code",
        keyLabel: "account_object_code",
        columnNames: columnNamesObject,
        isOptionsTable: true,
        positionOption: "bottom-left"
    },
    { key: "account_object_name", text: "Tên đối tượng", width: 250 },
    {
        key: "bank_account",
        text: "TK ngân hàng",
        width: 200,
        type: "combobox",
        items: [],
        optionId: "bank_number",
        keyLabel: "bank_number",
        columnNames: columnNameBankAccount,
        isOptionsTable: true,
        positionOption: "bottom-left"
    }

];

const columnNamesEmployee = [
    { key: "employee_code", text: "Mã nhân viên", width: 120 },
    { key: "employee_name", text: "Tên nhân viên", width: 220 },
    { key: "employee_position", text: "Đơn vị", width: 120 },
    { key: "phone_number", text: "ĐT di động", width: 120 }
];
const columnNamesCurrency = [
    { key: "currency_id", text: "Mã loại tiền" },
    { key: "currency_name", text: "Tên loại tiền" }
];

const currencies = [
    { currency_id: "VND", currency_name: "VND" },
    { currency_id: "USD", currency_name: "USD" }
];

const paymentTypes = [
    { key: "Chi tiền cho", label: "5.Chi khác" }
];
export default {
    name: "PaymentDetails",
    components: { TableCommon },

    data() {
        return {
            columnNames: columnNames,

            dataDetails: [
                {
                    description: "",
                    debit_account: "",
                    credit_account: "",
                    amount: 0,
                    account_object_code: "",
                    account_object_name: "",
                    bank_account: ""
                }
            ],
            paymentTypes,
            paymentType: "Chi tiền cho",
            data: {
                journal_memo: "Chi tiền cho",
                posted_date: utils.formatDateValueInput(new Date()),
                refdate: utils.formatDateValueInput(new Date()),
                refno_finance: "",
                currency_id: "VND",
                exchange_rate: 1,
                account_object_id: "",
                account_object_name: "",
                account_object_address: "",
                employee_id: ""
            },
            columnNamesCurrency,
            currencies,

            // loading
            loading: false,

            accountObjects: [],
            employees: [],
            columnNamesObject,
            columnNamesEmployee
        };
    },

    watch: {
        "data.account_object_id": {
            handler(value) {
                const accountObject = this.accountObjects.find(item => item.account_object_id === value);
                if (accountObject) {
                    this.data.account_object_name = accountObject.account_object_name;
                    this.data.account_object_address = accountObject.address;
                    this.data.journal_memo = this.paymentType + " " + accountObject.account_object_name;
                }
            },
            deep: true
        },

        "data.posted_date": {
            handler(value, oldValue) {
                if (oldValue === this.data.refdate) {
                    this.data.refdate = value;
                }
            },
            deep: true
        },

        "data.currency_id": {
            handler(value) {
                if (value === "USD") {
                    this.data.exchange_rate = 23000;
                    return;
                }

                this.data.exchange_rate = 1;
            },
            deep: true
        },

        accountObjects: {
            handler(value) {
                this.columnNames = this.columnNames.map(item => {
                    if (item.key === "account_object_code") {
                        return {
                            ...item,
                            items: value
                        };
                    }

                    return item;
                });
            },
            deep: true
        }
    },

    async mounted() {
        try {
            this.loading = true;
            const promise = await ProviderApi.getAll();
            this.accountObjects = promise?.data?.Data ?? [];
            const employeePromise = await EmployeeApi.getAll();
            this.employees = employeePromise?.data?.Data ?? [];

            this.loading = false;
        } catch (error) {
            this.loading = false;

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
         * sửa tên của object theo đối tượng đã chọn trong data detail
         * Created by: VLVU(12/10/2021)
         */
        customDataDetails(comboboxItem, rowIndex) {
            this.dataDetails[rowIndex].account_object_name = comboboxItem.account_object_name;
        },
        /**
         * Thêm 1 hàng mới vào bảng
         * Created by: VLVU(9/9/2018)
         */
        addRow() {
            const newRow = {
                description: "",
                debit_account: "",
                credit_account: "",
                amount: 0,
                account_object_code: "",
                account_object_name: "",
                bank_account: ""
            };
            this.dataDetails = [...this.dataDetails, newRow];
        },
        /**
         * Xóa tất cả các hàng
         * Created by: VLVU (9/9/2021)
         */
        async deleteAllRows() {
            const ok = await this.confirmPopup(resources.popup.deleteAllRows);
            if (!ok) {
                return;
            }
            this.dataDetails = [];
        },

        /**
         * @param {number} index vị trí của hàng
         * Xóa 1 hàng
         * Created by: VLVU(9/9/2021)
         */
        deleteRow(index) {
            this.dataDetails.splice(index, 1);
        }
    }
};
</script>

<style scoped>
.receipt-details {
    width: 100%;
    height: 100%;
    background-color: #f4f5f8;
}

.header {
    display: flex;
    justify-content: space-between;
    padding: 9px 16px;
    margin-bottom: 7px;
    position: relative;
    top: 0;
}

.header-left {
    display: flex;
    align-items: center;
}

.header-left .title {
    font-size: 24px;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    font-weight: bold;
    margin-right: 50px;
}

.header-left .payment-list {
    width: 350px;
}

.close-button {
    background: url("../../../assets/icon/icon.svg") no-repeat;
    cursor: pointer;
    width: 24px;
    height: 24px;
    background-position: -144px -144px;
}

.helper-button {
    background: url("../../../assets/icon/icon.svg") no-repeat;
    cursor: pointer;
    width: 24px;
    height: 24px;
    background-position: -89px -144px;
    margin-right: 10px;
}

.header-right {
    display: flex;
}

.footer {
    width: 100%;
    height: 50px;
    background-color: #38393d;
    position: absolute;
    bottom: 0;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 8px 16px;
}

.footer .footer-right {
    display: flex;
}

.btn-first {
    margin-right: 10px;
}

.content {
    overflow: auto;
    height: calc(100% - 108px);
}

.content .main-information {
    padding: 16px 30px 24px 30px;
    position: sticky;
    position: -webkit-sticky;
    left: 0;
}

.content .main-center {
    display: flex;
    flex-direction: column;
    padding-left: 16px;
    border-left: 1px solid #d4d7dc;
}

.total-money {
    display: flex;
    flex-direction: column;
    align-items: flex-end;
}

.total-money .total {
    font-size: 36px;
    font-weight: 700;
}

.acc-header {
    padding: 30px;
    display: flex;
    justify-content: space-between;
    align-items: center;

    background-color: #fff;
    position: sticky;
    position: -webkit-sticky;
    left: 0;
}

.acc-header .curreny-option {
    display: flex;
}

.acc-header .curreny-option .currency-type,
.exchange {
    display: flex;
    align-items: center;
}

.grid-control {
    padding: 0 30px;
    position: sticky;
    position: -webkit-sticky;
    left: 0;
}

.btn-grid-control {
    padding: 10px 0px 32px;
}

.upload .title {
    margin-bottom: 6px;
}

.drop-upload {
    width: 340px;
    height: 50px;
    border-radius: 2px;
    border: 1px solid #babec5;
    color: #9e9e9e;
    font-style: italic;
}

.tour-icon {
    background: url("../../../assets/icon/icon.svg") no-repeat -984px -144px;
    width: 24px;
    height: 24px;
    cursor: pointer;
}

.tutorial {
    white-space: nowrap;
    padding-left: 5px;
    font-size: 13px;
    color: #0075c0;
}

.acc-header .title {
    white-space: nowrap;
    padding-left: 5px;
    font-size: 13px;
    color: #0075c0;
    font-weight: 700;
}

.tutorial:hover,
.acc-header .title:hover {
    text-decoration: underline;
    cursor: pointer;
}

.overlay {
    position: absolute;
    top: 0;
    width: 100%;
    height: 100%;
    background: #fff;
    opacity: 0.4;
}

.loading {
    position: absolute;
    margin-left: auto;
    margin-right: auto;
    top: 40%;
    left: 0;
    right: 0;
    text-align: center;
}
</style>
