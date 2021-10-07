<template>
    <div id="payment">
        <div class="payment-header">
            <div class="title-area">
                <div class="title-text">
                    Thu chi tiền mặt
                </div>
                <div class="group-button">
                    <div class="tour-icon"></div>
                    <div class="tutorial">
                        Hướng dẫn
                    </div>
                    <div class="utility">
                        <BaseDropdownButton
                            buttonName="Tiện ích"
                            secondaryButton
                        />
                    </div>
                    <div class="add-button">
                        <BaseDropdownButton
                            buttonName="Thêm chi tiền"
                            @click="toPopupPaymentDetails"
                        />
                    </div>
                </div>
            </div>
        </div>

        <div class="payment-body">
            <div class="overview">
                <BaseRow>
                    <BaseCol
                        :cols="4"
                        :padding="4"
                        style="padding-left:0;"
                    >
                        <div class="debit">
                            <div class="data">
                                <div class="total-money">100.360.100,0</div>
                            </div>
                            <div class="label">Tổng thu đầu năm hiện tại</div>
                        </div>
                    </BaseCol>
                    <BaseCol
                        :cols="4"
                        :padding="4"
                    >
                        <div class="total-debit">
                            <div class="data">
                                <div class="total-money">10.000.115,0</div>
                            </div>
                            <div class="label">Tổng chi đầu năm hiện tại</div>
                        </div>
                    </BaseCol>
                    <BaseCol
                        :cols="4"
                        :padding="4"
                        style="padding-right:0;"
                    >
                        <div class="payment">
                            <div class="data">
                                <div
                                    class="total-money"
                                    style="color: red;"
                                >(894.440.015,0)</div>
                            </div>
                            <div class="label">Tồn quỹ hiện tại</div>
                        </div>
                    </BaseCol>
                </BaseRow>
            </div>
            <div class="tabs">
                <ul>
                    <li class="tab-active">Tất cả</li>
                    <li>Thu tiền</li>
                    <li>Chi tiền</li>
                </ul>
            </div>
            <div class="filter-bar">
                <div class="filter-area">
                    <div class="check-all-arrow">
                        <div class="check-all-arrow-icon"></div>
                    </div>
                    <div class="check-all">
                        <BaseDropdownButton
                            buttonName="Thực hiện hàng loạt"
                            disabled
                            secondaryButton
                        />
                    </div>
                    <div class="filter">
                        <BaseDropdownButton
                            buttonName="Lọc"
                            secondaryButton
                        />
                    </div>
                </div>
                <div class="filter-bar-right-area">
                    <div class="search-input">
                        <BaseInput
                            searchIcon
                            placeholder="Tìm theo mã, tên, số điện thoại"
                            fullWidth
                            v-model="filterText"
                        />

                    </div>
                    <div
                        class="reload-icon"
                        @click="reloadPayment"
                    ></div>
                </div>
            </div>
            <div class="grid">
                <PaymentTable
                    :columnNames="columnNames"
                    :dataProps="payments"
                    @handleClickEdit="handleClickEdit"
                    @handleClickDelete="handleClickDelete"
                    @handleClickRelication="handleClickRelication"
                />
            </div>
        </div>
        <div class="pagination-bar">
            <div class="pagination-box">
                <div class="paging-left">
                    Tổng số: <b>{{totalRecord}}</b> bản ghi
                </div>
                <div class="paging-right">
                    <div class="page-size">
                        <BaseCombobox
                            positionOption="top"
                            :items="pageSizes"
                            v-model="pageSize"
                            optionId="value"
                            keyLabel="label"
                            readonly
                        />
                    </div>
                    <div
                        class="btn-prev-page"
                        :class="{'disabled-text': pageNumber === 1}"
                        @click="prevPage"
                    >Trước</div>
                    <!-- Khi page có ít hơn 5 trang thì hiển thị toàn bộ số trang -->
                    <div
                        class="pagination"
                        v-if="totalPage < 6"
                    >
                        <div
                            v-for="page in totalPages"
                            :key="page"
                            class="pagination-item"
                            :class="{'active': page == pageNumber}"
                            @click="() => onPagination(page)"
                        >
                            {{page}}
                        </div>
                    </div>
                    <!-- Nếu không thì sẽ hiện thêm dấu 3 chấm ở giữa -->
                    <div v-else>
                        <!-- Nếu trang hiện tại là 3 trang đầu tiên vd: 1 2 3 ... 50 -->
                        <div
                            class="pagination"
                            v-if="pageNumber < 3"
                        >
                            <div
                                v-for="page in [1,2,3]"
                                :key="page"
                                class="pagination-item"
                                :class="{'active': page == pageNumber}"
                                @click="() => onPagination(page)"
                            >
                                {{page}}
                            </div>

                            <div class="pagination-item">
                                ...
                            </div>
                            <div
                                class="pagination-item"
                                :class="{'active': totalPage == pageNumber}"
                                @click="() => onPagination(totalPage)"
                            >
                                {{totalPage}}
                            </div>
                        </div>
                        <!-- Nếu trang hiện tại lớn hơn 3 thì sẽ xuất hiện thêm 1 dấu 3 chấm nữa vd: 1 ... 4 5 6 ... 50 -->
                        <div
                            v-else-if="pageNumber < totalPage - 1"
                            class="pagination"
                        >
                            <div
                                class="pagination-item"
                                :class="{'active': pageNumber === 1}"
                                @click="() => onPagination(1)"
                            >
                                1
                            </div>
                            <div
                                class="pagination-item"
                                v-if="pageNumber !== 3"
                            >
                                ...
                            </div>
                            <div
                                v-for="page in threePagesCloseTogether"
                                :key="page"
                                class="pagination-item"
                                :class="{'active': page == pageNumber}"
                                @click="() => onPagination(page)"
                            >
                                {{page}}
                            </div>
                            <div
                                class="pagination-item"
                                v-if="pageNumber !== totalPage - 2"
                            >
                                ...
                            </div>
                            <div
                                class="pagination-item"
                                :class="{'active': pageNumber === totalPage}"
                                @click="() => onPagination(totalPage)"
                            >
                                {{totalPage}}
                            </div>
                        </div>
                        <!-- Khi trang hiện tại là nhưng trang cuối cùng thì bỏ 1 dấu 3 chấm đi: 1 ... 48 49 50 -->
                        <div
                            class="pagination"
                            v-else
                        >
                            <div
                                class="pagination-item"
                                :class="{'active': pageNumber === 1}"
                                @click="() => onPagination(1)"
                            >
                                1
                            </div>

                            <div class="pagination-item">
                                ...
                            </div>
                            <div
                                v-for="page in [totalPage -2, totalPage -1, totalPage]"
                                :key="page"
                                class="pagination-item"
                                :class="{'active': page == pageNumber}"
                                @click="() => onPagination(page)"
                            >
                                {{page}}
                            </div>
                        </div>
                    </div>

                    <div
                        class="btn-next-page"
                        :class="{'disabled-text': pageNumber === totalPage}"
                        @click="nextPage"
                    >Sau</div>
                </div>
            </div>
        </div>
    </div>

</template>

<script>

import PaymentApi from "../../api/service/payment";
import { mapActions, mapMutations } from "vuex";
import enums from "../../enums";
import resources from "../../resources";
import PaymentTable from "./Table";
const columnNames = [
    { key: "posted_date", text: "Ngày hạch toán", width: 145, align: "center", format: "date" },
    { key: "refdate", text: "Ngày chứng từ", width: 150, align: "center", format: "date" },
    { key: "refno_finance", text: "Số chứng từ", width: 125 },
    { key: "journal_memo", text: "Diễn giải", width: 320 },
    { key: "total_amount", text: "Số tiền", width: 150 },
    { key: "account_object_code", text: "Mã đối tượng", width: 150 },
    { key: "account_object_name", text: "Đối tượng", width: 150 },
    { key: "account_object_contact_name", text: "Chức danh", width: 250 },
    { key: "account_object_address", text: "Tên đơn vị", width: 250 }
];

// các giá trị mặc định của paging và filter
const defaultFilterText = "";
const defaultPageNumber = 1;
const defaultTotalPage = 1;
const defaultTotalRecord = 0;
const defaultPageSizes = [
    { value: 10, label: "10 bản ghi trên 1 trang" },
    { value: 20, label: "20 bản ghi trên 1 trang" },
    { value: 30, label: "30 bản ghi trên 1 trang" },
    { value: 50, label: "50 bản ghi trên 1 trang" },
    { value: 100, label: "100 bản ghi trên 1 trang" }
];
const defaultPageSize = 20;
export default {
    name: "ReceiptPaymentList",
    components: { PaymentTable },
    data() {
        return {
            columnNames: columnNames,
            payments: [],
            openDialog: false,
            currentEmployee: {},
            stateDialog: enums.dialogState.post,

            // pagination
            pageSizes: defaultPageSizes,
            pageSize: defaultPageSize,
            pageNumber: defaultPageNumber,
            totalPage: defaultTotalPage,
            totalRecord: defaultTotalRecord,
            filterText: defaultFilterText,

            idTimeout: null // id của setTimeOut khi thực hiện filter

        };
    },

    watch: {
        /**
         * Theo dõi sự thay đổi của route để update pageNumber
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        "$route.query": {
            handler(query) {
                if (query?.page) {
                    this.pageNumber = Number(query.page);
                }
            },
            deep: true,
            immediate: true
        },
        /**
         * Khi pageNumber thay thổi thì sẽ load lại employee
         * Created by: VLVU (18/8/2021)
         */
        pageNumber() {
            this.payments = null;
            this.loadPayment();
        },

        /**
         * Khi pageNumber thay thổi thì sẽ load lại employee
         * Created by: VLVU (18/8/2021)
         */
        pageSize() {
            this.payments = null;
            this.pageNumber = defaultPageNumber;

            this.$router.push({ path: "receipt-payment-list", query: { page: 1 } }).catch(() => { });
            this.loadPayment();
        },

        /**
         * Khi search sẽ thực hiện filter
         * Created by: VLVU (18/8/2021)
         */
        filterText(value) {
            clearTimeout(this.idTimeout);

            this.idTimeout = setTimeout(() => {
                this.pageNumber = defaultPageNumber;

                this.$router.push({ path: "receipt-payment-list", query: { page: 1 } }).catch(() => { });
                this.loadPayment();
            }, 700);
        }
    },

    computed: {
        /**
         * Tạo 1 array totalPages
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        totalPages() {
            return Array.from({ length: this.totalPage }, (_v, k) => k + 1);
        },

        /**
         * Tạo ra 1 array gồm 3 trang gần nhau và gần với trang hiện tại
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        threePagesCloseTogether() {
            return [this.pageNumber - 1, this.pageNumber, this.pageNumber + 1];
        }
    },
    /**
     * Lấy thông tin của toàn bộ nhân viên
     * Lấy các vị trí và các phong ban
     * CreatedBy: Vũ Long Vũ 16/7/2021
     */
    mounted() {
        this.getData();
        document.removeEventListener("click", this.handleClickOutside);
        document.addEventListener("click", this.handleClickOutside);
    },

    methods: {
        ...mapMutations("toastMessage", {
            setToast: "setToast"
        }),
        ...mapActions("popup", {
            confirmPopup: "confirmPopup"
        }),

        /**
         * Hàm lấy dữ liệu và thông tin đơn vị
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        async getData() {
            try {
                const promise = await Promise.all([
                    PaymentApi.getPaymentFilterPaging("")
                ]);

                this.payments = promise[0]?.data?.Data ?? [];

                this.totalPage = promise[0]?.data?.TotalPage === 0 ? 1 : promise[0]?.data?.TotalPage || 1; // số page luôn là 1
                this.totalRecord = promise[0]?.data?.TotalRecord;
            } catch (error) {
                this.payments = [];
                if (error?.response?.status === enums.statusCode.serverError) {
                    this.setToast({
                        content: error.response.data.MsgUser,
                        type: "error"
                    });
                    return;
                }
                this.setToast({
                    content: resources.serverErrorMessageDefault,
                    type: "error"
                });
            }
        },

        /**
         * Hàm tải lại dữ liệu nhân viên
         * Created by: VLVU (10/8/2021)
         */
        async loadPayment() {
            try {
                const promise = await PaymentApi.getPaymentFilterPaging(this.filterText.trim(), this.pageNumber, this.pageSize.value);

                this.payments = promise?.data.Data;
                this.totalPage = promise?.data?.TotalPage === 0 ? 1 : promise?.data?.TotalPage || 1;
                this.totalRecord = promise?.data.TotalRecord;
            } catch (error) {
                this.payments = [];
                if (error?.response?.status === enums.statusCode.serverError) {
                    this.setToast({
                        content: error.response.data.MsgUser,
                        type: "error"
                    });
                    return;
                }
                this.setToast({
                    content: resources.serverErrorMessageDefault,
                    type: "error"
                });
            }
        },

        /**
         * Hàm reload hoàn toàn lại bảng nhân viên
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        reloadPayment() {
            this.payments = null;
            this.pageNumber = defaultPageNumber;
            this.$router.push({ path: "employee", query: { page: defaultPageNumber } }).catch(() => { });
            this.totalPage = defaultTotalPage;
            this.totalRecord = defaultTotalRecord;
            this.filterText = defaultFilterText;

            this.loadPayment();
        },

        /**
         * Hàm mở popup payment details
         * Created by: Vũ Long Vũ (6/10/2021)
         */
        toPopupPaymentDetails() {
            this.$router.push({ name: "PaymentDetailsFromPaymentView" });
        },

        /**
         * Đóng dialog form chi tiết nhân viên
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        onCloseDialog(params) {
            this.openDialog = false;
            this.currentPayment = {};
            if (params?.hasReloadPayment) {
                this.payments = null;
                this.reloadPayment();
            }
        },
        /**
         * Hàm khi người dùng dblclick vào 1 hàng hoặc ấn vào chữ sửa trên hàng đó
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        handleClickEdit(employee) {
            this.currentPayment = employee;
            this.stateDialog = enums.dialogState.put;
            this.openDialog = true;
        },

        /**
         * Hàm khi người dùng dblclick vào 1 hàng hoặc ấn vào chữ sửa trên hàng đó
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        async handleClickDelete(employee) {
            this.currentPayment = employee;
            const ok = await this.confirmPopup(resources.popup.deleteEmployee(employee.EmployeeCode));
            if (!ok) {
                return;
            }
            this.onDelete();
        },

        /**
         * Hàm khi người dùng dblclick vào 1 hàng hoặc ấn vào chữ sửa trên hàng đó
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        handleClickRelication(employee) {
            this.currentPayment = employee;
            this.stateDialog = enums.dialogState.post;
            this.openDialog = true;
        },

        /**
         * Hàm xóa 1 nhân viên
         * Created by: Vũ Long Vũ (19/7/2021)
         */

        async onDelete() {
            try {
                await PaymentApi.deleteOne(this.currentPayment.ref_id);
                this.setToast(resources.toast.deleteEmployeeSuccess(this.currentPayment.EmployeeCode));
                this.reloadPayment();
            } catch (error) {
                if (error.response.status === enums.statusCode.serverError) {
                    this.setToast({
                        content: error.response.data.MsgUser,
                        type: "error"
                    });
                }
                this.setToast({
                    content: resources.serverErrorMessageDefault,
                    type: "error"
                });
            }
            this.currentPayment = {};
        },

        /**
         * Các hàm thực hiện pagination
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        // #region paging

        onPagination(page) {
            this.$router.push({ path: "receipt-payment-list", query: { page: page } }).catch(() => { });
        },

        nextPage() {
            if (this.pageNumber === this.totalPage) {
                return;
            }
            this.$router.push({ path: "receipt-payment-list", query: { page: this.pageNumber + 1 } }).catch(() => { });
        },
        prevPage() {
            if (this.pageNumber - 1 === 0) {
                return;
            }
            this.$router.push({ path: "receipt-payment-list", query: { page: Math.max(0, this.pageNumber - 1) } }).catch(() => { });
        }
        // #endregion
    },

    // xóa sự kiện này khi thoát khỏi xóa component
    destroyed() {
        document.removeEventListener("click", this.handleClickOutside);
    }
};
</script>

<style scoped src="./style.css">
</style>
