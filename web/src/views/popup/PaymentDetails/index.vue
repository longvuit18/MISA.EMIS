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
                                            />
                                        </BaseCol>
                                        <BaseCol
                                            :cols="7"
                                            style="padding: 0 16px  0 12px;"
                                        >
                                            <BaseInput
                                                label="Đối tượng"
                                                tabindex="2"
                                                fullWidth
                                            />
                                        </BaseCol>
                                    </BaseRow>
                                    <BaseRow>
                                        <BaseCol style="padding-right:16px;">
                                            <BaseInput
                                                label="Địa chỉ"
                                                tabindex="3"
                                                fullWidth
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
                                                    v-model="data.posted_date"
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
                        v-model="dataTable"
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
        </div>
    </BaseDialog>
</template>

<script>
import { mapActions, mapMutations } from "vuex";
import TableCommon from "../../../components/common/Table";
import resources from "../../../resources";
import utils from "../../../utils";

const columnNames = [
    { key: "a", text: "Diễn giải", width: 320, type: "input" },
    { key: "b", text: "Tài khoản nợ", width: 150, type: "combobox", items: [{ value: "test", text: "test" }] },
    { key: "c", text: "Tài khoản có", width: 175, type: "combobox", items: [{ value: "test", text: "test" }] },
    { key: "d", text: "Số tiền", width: 190, align: "right", type: "input", format: "currency" },
    { key: "e", text: "Đối tượng", width: 185, type: "input" },
    { key: "f", text: "Tên đối tượng", width: 305 }
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
            columnNamesCurrency,
            currencies,
            dataTable: [
                { a: "abc", b: "test", c: "test", d: "abc", e: "abc", f: "abc" },
                { a: "abc", b: "abc", c: "abc", d: "abc", e: "abc", f: "abc" }
            ],
            paymentTypes,
            paymentType: "Chi tiền cho",
            data: {
                journal_memo: "Chi tiền cho",
                posted_date: utils.formatDateValueInput(new Date()),
                refdate: utils.formatDateValueInput(new Date()),
                refno_finance: "",
                currency_id: "VND",
                exchange_rate: "1"
            }
        };
    },

    methods: {

        ...mapMutations("toastMessage", {
            setToast: "setToast"
        }),
        ...mapActions("popup", {
            confirmPopup: "confirmPopup"
        }),
        /**
         * Thêm 1 hàng mới vào bảng
         * Created by: VLVU(9/9/2018)
         */
        addRow() {
            const newRow = { a: "abc", b: "abc", c: "abc", d: "abc", e: "abc", f: "abc" };
            this.dataTable = [...this.dataTable, newRow];
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
            this.dataTable = [];
        },

        /**
         * @param {number} index vị trí của hàng
         * Xóa 1 hàng
         * Created by: VLVU(9/9/2021)
         */
        deleteRow(index) {
            this.dataTable.splice(index, 1);
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
</style>
