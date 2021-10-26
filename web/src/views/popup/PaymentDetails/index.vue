<template>
    <BaseDialog isOpen>
        <div class="receipt-details">
            <div class="header">
                <div class="header-left">
                    <div class="icon icon-size-24 mi-recent-log mr-10"></div>
                    <div class="title">Phiếu chi {{ data.refno_finance }}</div>
                    <div>
                        <BaseCombobox
                            class="payment-list"
                            :items="paymentTypes"
                            optionId="key"
                            keyLabel="label"
                            v-model="paymentType"
                            :disabled="viewMode"
                            readonly
                        />
                    </div>
                </div>
                <div class="header-right">
                    <div class="tour-icon"></div>
                    <div class="tutorial">
                        Hướng dẫn
                    </div>
                    <div class="icon icon-size-24 mi-setting__nav ml-10 mr-10" v-tooltip="'Tính năng chưa phát triển'"></div>
                    <div class="helper-button" v-tooltip="'Tính năng đang phát triển'"></div>
                    <div @click="onClose" class="close-button" v-tooltip="'Đóng (Esc)'"></div>
                </div>
            </div>
            <div class="content">
                <div class="main-information">
                    <BaseRow>
                        <BaseCol :cols="9">
                            <BaseRow>
                                <BaseCol :cols="10">
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
                                                :disabled="viewMode"
                                                required
                                                ref="1"
                                                name="Đối tượng"
                                            />
                                        </BaseCol>
                                        <BaseCol :cols="7" style="padding: 0 16px  0 12px;">
                                            <BaseInput
                                                label="Người nhận"
                                                tabindex="2"
                                                fullWidth
                                                v-model="data.account_object_contact_name"
                                                :disabled="viewMode"
                                                name="Người nhận"
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
                                                :disabled="viewMode"
                                                name="Địa chỉ"
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
                                                :disabled="viewMode"
                                                name="Lý do nộp"
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
                                                :disabled="viewMode"
                                                name="Nhân viên"
                                            />
                                        </BaseCol>
                                        <BaseCol :cols="7" style="padding: 0 16px  0 12px;">
                                            <div style="display: flex;">
                                                <BaseInput
                                                    label="Kèm theo"
                                                    tabindex="6"
                                                    enterRightToLeft
                                                    placeholder="Số lượng"
                                                    v-model="data.document_included"
                                                    :disabled="viewMode"
                                                    name="Kèm thêm"
                                                    format="number"
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
                                <BaseCol :cols="2">
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
                                                    :disabled="viewMode"
                                                    name="Ngày hạch toán"
                                                />
                                            </BaseCol>
                                        </BaseRow>
                                        <BaseRow>
                                            <BaseCol>
                                                <BaseDatePicker
                                                    label="Ngày phiếu chi"
                                                    fullWidth
                                                    tabindex="8"
                                                    format="DD/MM/YYYY"
                                                    value-type="YYYY-MM-DD"
                                                    v-model="data.refdate"
                                                    :disabled="viewMode"
                                                    name="Ngày phiếu chi"
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
                                                    :disabled="viewMode"
                                                    required
                                                    ref="2"
                                                    name="Số phiếu chi"
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
                                <div class="total">{{ totalAmountFormat }}</div>
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
                                    tabindex="10"
                                    :disabled="viewMode"
                                />
                            </div>
                        </div>
                        <div class="exchange" v-if="data.currency_id !== 'VND'">
                            <div class="label mr-10">Tỉ giá</div>
                            <div style="width: 100px">
                                <BaseInput fullWidth format="currency" placeholder="1,00" v-model="data.exchange_rate" :disabled="viewMode" />
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
                        hasRowTotal
                        @getTotal="getTotal"
                        tabindex="11"
                        :disabled="viewMode"
                        ref="detail"
                    />
                </div>
                <div class="grid-control">
                    <div class="btn-grid-control flex">
                        <div class="mr-10">
                            <BaseButton secondaryButton style="height: 24px;" buttonName="Thêm dòng" @click="addRow" />
                        </div>
                        <div>
                            <BaseButton secondaryButton style="height: 24px;" buttonName="Xóa hết dòng" @click="deleteAllRows" />
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
                    <BaseButton buttonName="Hủy" secondaryButton :style="{ background: 'inherit', color: '#fff' }" @click="() => onClose(false)" />
                </div>
                <div class="footer-right">
                    <div class="btn-first">
                        <BaseButton
                            buttonName="Cất"
                            secondaryButton
                            @click="() => onSave()"
                            :style="{ background: 'inherit', color: '#fff' }"
                            v-tooltip="'Cất (Ctrl + S)'"
                        />
                    </div>
                    <BaseDropdownButton
                        :buttonName="nameFooterPrimaryButton"
                        borderRadius="3px"
                        :isOpen="openBoxStateFooter"
                        @openBox="openBoxStateFooter = !openBoxStateFooter"
                        positionOption="top"
                        @close="openBoxStateFooter = false"
                        @click="nameFooterPrimaryButton === 'Cất và đóng' ? saveAndClose() : saveAndAdd()"
                    >
                        <div class="payment-detail-footer">
                            <div class="option" @click="() => saveAndClose()">Cất và đóng</div>
                            <div class="option" @click="() => saveAndAdd()">Cất và thêm</div>
                        </div>
                    </BaseDropdownButton>
                </div>
            </div>
            <div class="overlay" v-if="loading"></div>
            <div class="loading" v-if="loading">
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
import PaymentApi from "../../../api/service/payment";
import enums from "../../../enums";

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

const bankAccount = [
    { bank_number: "134673623", bank_name: "Ngân hàng quân đội MB", bank_branch_name: "Bà Triệu" },
    { bank_number: "111123512", bank_name: "Ngân hàng TMCB Á Châu", bank_branch_name: "Hà Thành" },
    { bank_number: "5262452", bank_name: "Vietcombank Phố Vọng", bank_branch_name: "Phố Vọng" }
];

const columnNameAccount = [
    { key: "account_number", text: "Số tài khoản" },
    { key: "account_name", text: "Tên tài khoản" }
];

const columnNameBankAccount = [
    { key: "bank_number", text: "Số tài khoản", width: 150 },
    { key: "bank_name", text: "Tên tài khoản", width: 250 },
    { key: "bank_branch_name", text: "Chi nhánh", width: 120 }
];

const columnNamesObject = [
    { key: "account_object_code", text: "Mã đối tượng", width: 120 },
    { key: "account_object_name", text: "Tên đối tượng", width: 250 },
    { key: "tax_code", text: "Mã sô thuế", width: 100 },
    { key: "address", text: "Địa chỉ", width: 300 },
    { key: "phone_number", text: "Điện thoại", width: 100 }
];
const columnNames = [
    {
        key: "credit_account",
        text: "Tài khoản có",
        width: 175,
        type: "combobox",
        items: creditAccount,
        optionId: "account_number",
        keyLabel: "account_number",
        columnNames: columnNameAccount,
        isOptionsTable: true,
        required: true
    },
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
        isOptionsTable: true,
        required: true
    },
    { key: "amount", text: "Số tiền", width: 150, align: "right", type: "input", format: "currency", total: true },
    {
        key: "account_object_code",
        text: "Đối tượng",
        width: 160,
        type: "combobox",
        items: [],
        optionId: "account_object_code",
        keyLabel: "account_object_code",
        columnNames: columnNamesObject,
        isOptionsTable: true,
        positionOption: "bottom-left"
    },
    { key: "account_object_name", text: "Tên đối tượng", width: 230 },
    {
        key: "bank_account",
        text: "TK ngân hàng",
        width: 180,
        type: "combobox",
        items: bankAccount,
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

const currencies = [{ currency_id: "VND", currency_name: "VND" }];

const paymentTypes = [{ key: "Chi tiền cho", label: "5.Chi khác" }];

const dataDetailsDefault = [
    {
        description: "Chi tiền cho",
        debit_account: "",
        credit_account: "11111",
        amount: 0,
        account_object_code: "",
        account_object_name: "",
        bank_account: ""
    }
];

const dataDefault = {
    journal_memo: "Chi tiền cho",
    posted_date: utils.formatDateValueInput(new Date()),
    refdate: utils.formatDateValueInput(new Date()),
    refno_finance: "",
    currency_id: "VND",
    exchange_rate: 1,
    account_object_id: undefined,
    account_object_name: "",
    account_object_address: "",
    account_object_contact_name: "",
    employee_id: undefined,
    total_amount: 0,
    document_included: null
};
export default {
    name: "PaymentDetails",
    components: { TableCommon },

    data() {
        return {
            columnNames: columnNames,

            dataDetails: dataDetailsDefault,
            paymentTypes,
            paymentType: "Chi tiền cho",
            data: { ...dataDefault },
            columnNamesCurrency,
            currencies,

            // loading
            loading: false,
            edited: false,
            recordInserted: 0,

            accountObjects: [],
            employees: [],
            columnNamesObject,
            columnNamesEmployee,

            currentState: enums.dialogState.post,

            openBoxStateFooter: false,
            nameFooterPrimaryButton: "Cất và đóng"
        };
    },

    watch: {
        data: {
            handler() {
                this.edited = true;
            },
            deep: true
        },
        dataDetails: {
            handler() {
                this.edited = true;
            },
            deep: true
        },
        "data.account_object_id": {
            handler(value) {
                // không làm gì cả khi ở view mode
                if (this.viewMode) {
                    return;
                }
                const accountObject = this.accountObjects.find(item => item.account_object_id === value);
                if (accountObject) {
                    this.data.account_object_name = accountObject.account_object_name;
                    this.data.account_object_code = accountObject.account_object_code;
                    this.data.account_object_address = accountObject.address;
                    this.data.account_object_contact_name = accountObject.account_object_name;
                    this.data.journal_memo = this.paymentType + " " + accountObject.account_object_name;

                    this.dataDetails = this.dataDetails.map(item => {
                        return {
                            ...item,
                            account_object_code: accountObject.account_object_code,
                            account_object_name: accountObject.account_object_name,
                            account_object_id: accountObject.account_object_id
                        };
                    });
                }
            },
            deep: true
        },

        "data.employee_id": {
            handler(value) {
                this.data.employee_name = this.employees.find(item => item.employee_id === value)?.employee_name;
            },
            deep: true
        },

        "data.journal_memo": {
            handler(value, oldvalue) {
                this.dataDetails = this.dataDetails.map(item => {
                    if (item.description === oldvalue) {
                        return {
                            ...item,
                            description: value
                        };
                    }

                    return item;
                });
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

    computed: {
        totalAmountFormat() {
            return utils.formatNumber(this.data.total_amount);
        },

        viewMode() {
            return !!this.$route.params?.id;
        }
    },

    async mounted() {
        // lấy tên của primary button footer

        const buttonName = localStorage.getItem("paymentDetailFooterPrimaryButtonName");

        this.nameFooterPrimaryButton = buttonName ?? "Cất và đóng";

        // lắng nghe sự kiện ấn phím
        document.addEventListener("keydown", e => this.listenerKeyPress(e));
        this.loading = true;
        if (this.$route.params?.replicationId) {
            let providerPromise = {};
            let employeePromise = {};
            let dataPromise = {};
            try {
                providerPromise = ProviderApi.getAll();
            } catch (error) {
                console.log(error);
            }

            try {
                employeePromise = EmployeeApi.getAll();
            } catch (error) {
                console.log(error);
            }

            try {
                dataPromise = PaymentApi.getPaymentById(this.$route.params?.replicationId);
            } catch (error) {
                console.log(error);
            }

            const accountObject = (await providerPromise) ?? [];
            const employees = (await employeePromise) ?? [];
            const data = await dataPromise;

            this.accountObjects = accountObject?.data?.Data ?? [];
            this.employees = employees?.data?.Data ?? [];
            this.data = data?.data?.Data?.payment ?? dataDefault;

            await this.setPaymentCode();
            this.loading = false;
            this.edited = false;
            return;
        }

        if (this.viewMode) {
            try {
                const promise = await PaymentApi.getPaymentById(this.$route.params.id);
                this.data = promise.data?.Data?.payment ?? dataDefault;
                this.dataDetails = promise.data?.Data?.payment_details ?? [];
                this.accountObjects = [
                    {
                        account_object_id: this.data.account_object_id,
                        account_object_name: this.data.account_object_name,
                        address: this.data.account_object_address
                    }
                ];
                this.employees = [{ employee_id: this.data.employee_id, employee_name: this.data.employee_name }];
                this.loading = false;
                this.edited = false;
                return;
            } catch (error) {
                this.loading = false;

                console.log(error);
            }
        }

        let promise = {};
        let employeePromise = {};
        try {
            promise = ProviderApi.getAll();
        } catch (error) {
            console.log(error);
        }

        try {
            employeePromise = EmployeeApi.getAll();
        } catch (error) {
            console.log(error);
        }

        const accountObject = (await promise) ?? [];
        const employees = (await employeePromise) ?? [];

        this.accountObjects = accountObject?.data?.Data ?? [];
        this.employees = employees?.data?.Data ?? [];
        await this.setPaymentCode();
        this.loading = false;
        this.edited = false;
    },

    destroyed() {
        document.removeEventListener("keydown", e => this.listenerKeyPress(e));
    },

    methods: {
        ...mapMutations("toastMessage", {
            setToast: "setToast"
        }),
        ...mapActions("popup", {
            confirmPopup: "confirmPopup"
        }),
        /**
         * listener key press
         */
        listenerKeyPress(event) {
            if (event.keyCode === 27) {
                this.onClose(true);
                return;
            }
            if (event.ctrlKey || event.metaKey) {
                switch (String.fromCharCode(event.which).toLowerCase()) {
                    case "s":
                        event.preventDefault();
                        this.onSave();
                        return;
                }
            }

            if (event.ctrlKey && event.shiftKey && event.keyCode === 83) {
                event.preventDefault();
                this.onSave(true);
            }
        },

        getTotal(total) {
            this.data.total_amount = total.amount;
        },

        async setPaymentCode() {
            try {
                const newCodePromise = await PaymentApi.getPaymentCode();

                this.data.refno_finance = newCodePromise?.data ?? "";
            } catch (error) {
                console.error(error);
            }
        },

        /**
         * sửa tên của object theo đối tượng đã chọn trong data detail
         * Created by: VLVU(12/10/2021	)
         */
        customDataDetails(comboboxItem, rowIndex) {
            if (comboboxItem?.account_object_name) {
                this.dataDetails[rowIndex].account_object_name = comboboxItem.account_object_name;
            }

            this.dataDetails[rowIndex].account_object_id = comboboxItem.account_object_id;
        },
        /**
         * Thêm 1 hàng mới vào bảng
         * Created by: VLVU(9/9/2018)
         */
        addRow() {
            let newRow = {};
            if (this.viewMode) {
                return;
            }
            if (this.dataDetails.length === 0) {
                newRow = {
                    description: this.data.journal_memo,
                    debit_account: "",
                    credit_account: "11111",
                    amount: 0,
                    account_object_code: this.data.account_object_code,
                    account_object_name: this.data.account_object_name,
                    bank_account: ""
                };
            } else {
                newRow = { ...this.dataDetails[this.dataDetails.length - 1] };
            }

            this.dataDetails = [...this.dataDetails, newRow];
        },
        /**
         * Xóa tất cả các hàng
         * Created by: VLVU (9/9/2021)
         */
        async deleteAllRows() {
            if (this.viewMode) {
                return;
            }
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
            if (this.viewMode) {
                return;
            }
            this.dataDetails.splice(index, 1);
        },

        async onClose(confirm = true) {
            if (this.edited && confirm && !this.viewMode) {
                const ok = await this.confirmPopup(resources.popup.confirmCloseDetailForm);
                // không làm gì cả
                if (ok === null) {
                    return;
                }
                // cất và đóng form
                if (ok === true) {
                    this.onSave();
                    return;
                }
            }
            if (this.$route.name === "PaymentDetailsFromProcessView") {
                this.$router.push("/cash/process");
                return;
            }

            if (this.recordInserted > 0) {
                this.$router.push("/cash/receipt-payment-list?reload=1");
                return;
            }
            this.$router.push("/cash/receipt-payment-list");
        },

        /**
         * Hàm kiểm tra validate dữ liệu trước khi gửi lên
         * Created by: VLVU (10/8/2021)
         */
        async validate() {
            let isError;
            // vị trí của lỗi
            const indexErrors = [];
            const errorsMessage = [];
            // tìm tất cả vị trí lỗi, sắp xếp theo vị trí thứ thự của tabindex
            Object.entries(this.$refs)
                .sort((a, b) => a[0] < b[0])
                .forEach((item, index) => {
                    if (item[0] === "detail") {
                        return;
                    }
                    item[1].$refs.BaseInput.focus();
                    item[1].$refs.BaseInput.blur();
                    if (item[1].error === true) {
                        isError = true;
                        // thêm vị trí lỗi vào array
                        indexErrors.push(index);
                        // thêm câu thông báo lỗi vào array
                        errorsMessage.push(item[1].errorMessage);
                    }
                });
            if (!isError) {
                return true;
            } else {
                // focus vào lỗi đầu tiên
                // hiển thị ra toast lỗi
                await this.confirmPopup({
                    content: errorsMessage[0],
                    type: "error",
                    state: "alert"
                });
                Object.values(this.$refs)[indexErrors[0]].$refs.BaseInput.focus();
                return false;
            }
        },

        /**
         * validate các trường ở trong bảng detail
         * Created by: VLVU (21/10/2021)
         */
        async validateDetails() {
            let isError;
            // vị trí của lỗi
            const indexErrors = [];
            const errorsMessage = [];
            // tìm tất cả vị trí lỗi, sắp xếp theo vị trí thứ thự của tabindex
            Object.values(this.$refs.detail.$refs).forEach((item, index) => {
                if (item.length === 0) {
                    return;
                }
                item[0].$refs.BaseInput.focus();
                item[0].$refs.BaseInput.blur();
                if (item[0].error === true) {
                    isError = true;
                    // thêm vị trí lỗi vào array
                    indexErrors.push(index);
                    // thêm câu thông báo lỗi vào array
                    errorsMessage.push(item[0].errorMessage);
                }
            });
            if (!isError) {
                return true;
            } else {
                // focus vào lỗi đầu tiên
                // hiển thị ra toast lỗi
                await this.confirmPopup({
                    content: errorsMessage[0],
                    type: "error",
                    state: "alert"
                });
                Object.values(this.$refs.detail.$refs)[indexErrors[0]][0].$refs.BaseInput.focus();
                return false;
            }
        },

        /**
         * Hàm cất dữ liệu, hàm này có thể thêm mới hoặc sửa dữ liệu
         * @param {bool} keepCreating Check xem có phải button cất và thêm không
         * Created by: VLVU (14/10/2021)
         */
        async onSave(keepCreating = false) {
            // trạng thái xem không xảy ra gì cả
            if (this.viewMode) {
                return;
            }
            // validate các trường trên master
            const validate = await this.validate();
            if (!validate) {
                return;
            }

            // kiểm tra xem chi tiết chứng từ có để trống hay không
            if (this.dataDetails.length === 0) {
                await this.confirmPopup({
                    content: "Bạn phải nhập chứng từ chi tiết!",
                    type: "error",
                    state: "alert"
                });
                return;
            }

            // validate các trường ở detail
            const isValidateDetails = await this.validateDetails();
            if (!isValidateDetails) {
                return;
            }

            // Kiêm tra ngày hạch toán có lớn hơn ngày chứng từ hay không
            if (new Date(this.data.refdate).getTime() > new Date(this.data.posted_date).getTime()) {
                await this.confirmPopup({
                    content: `Ngày hạch toán &lt;${utils.formatDateLocal(this.data.refdate)}&gt; phải lớn hơn hoặc bằng 
						ngày chứng từ &lt;${utils.formatDateLocal(this.data.posted_date)}&gt;.`,
                    type: "error",
                    state: "alert"
                });
                return;
            }

            let promise = null;
            try {
                this.loading = true;
                promise = await PaymentApi.insertPayment(this.data, this.dataDetails);

                // lỗi validate
                if (!promise.data.State) {
                    await this.confirmPopup({
                        content: promise.data.MsgUser,
                        type: "warning",
                        state: "alert"
                    });
                    this.loading = false;
                    return;
                }
                this.setToast(resources.toast.addSuccess(this.data.refno_finance, "phiếu chi"));
                this.data = { ...dataDefault };
                this.dataDetails = dataDetailsDefault;
                this.recordInserted = this.recordInserted + 1;
                if (keepCreating) {
                    const newCodePromise = await PaymentApi.getPaymentCode();
                    this.data.refno_finance = newCodePromise.data;
                    this.$refs["1"].$refs.BaseInput.focus();
                    this.loading = false;
                    this.edited = false;
                    return;
                }
                this.onClose(false);
            } catch (error) {
                this.loading = false;
                if (error?.response?.status === enums.statusCode.serverError) {
                    this.setToast({
                        type: "error",
                        content: error.response?.data?.MsgUser || resources.serverErrorMessageDefault
                    });
                    return;
                }

                this.setToast({
                    type: "error",
                    content: resources.serverErrorMessageDefault
                });
            }
        },

        saveAndClose() {
            // trạng thái xem không xảy ra gì cả
            if (this.viewMode) {
                return;
            }
            this.openBoxStateFooter = false;
            localStorage.setItem("paymentDetailFooterPrimaryButtonName", "Cất và đóng");
            this.nameFooterPrimaryButton = "Cất và đóng";
            this.onSave();
        },

        saveAndAdd() {
            // trạng thái xem không xảy ra gì cả
            if (this.viewMode) {
                return;
            }
            this.openBoxStateFooter = false;
            localStorage.setItem("paymentDetailFooterPrimaryButtonName", "Cất và thêm");
            this.nameFooterPrimaryButton = "Cất và thêm";
            this.onSave(true);
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
    padding: 16px 16px 20px;
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
    height: calc(100% - 118px);
}

.content .main-information {
    padding: 8px 24px 20px;
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
    padding: 16px 24px 16px 24px;
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
    padding: 10px 0px 16px;
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
<style>
.payment-detail-footer {
    width: 100%;
    background: #fff;
    padding: 2px 1px;
    border-radius: 2px;
    border: 1px solid #babec5;
    position: relative;
}

.payment-detail-footer .option {
    background: inherit;
    color: inherit;
    cursor: pointer;
    transition: all 0.2s ease;
    padding: 5px;
    padding-left: 10px;
    padding-right: 10px;
    width: 100%;
    position: relative;
    display: block;
}

.payment-detail-footer .option:hover {
    color: #08bf1e;
    background-color: #e8e9ec;
}
</style>
