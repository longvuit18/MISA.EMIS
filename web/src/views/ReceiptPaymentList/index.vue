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
                        <BaseDropdownButton buttonName="Tiện ích" secondaryButton />
                    </div>
                    <div class="add-button">
                        <BaseDropdownButton buttonName="Thêm chi tiền" @click="toPopupPaymentDetails" />
                    </div>
                </div>
            </div>
        </div>

        <div class="payment-body">
            <div class="overview" v-if="showOverview">
                <BaseRow>
                    <BaseCol :cols="4" :padding="4" style="padding-left:0;">
                        <div class="debit">
                            <div class="data">
                                <div class="total-money">100.360.100,0</div>
                            </div>
                            <div class="label">Tổng thu đầu năm hiện tại</div>
                        </div>
                    </BaseCol>
                    <BaseCol :cols="4" :padding="4">
                        <div class="total-debit">
                            <div class="data">
                                <div class="total-money">10.000.115,0</div>
                            </div>
                            <div class="label">Tổng chi đầu năm hiện tại</div>
                        </div>
                    </BaseCol>
                    <BaseCol :cols="4" :padding="4" style="padding-right:0;">
                        <div class="payment">
                            <div class="data">
                                <div class="total-money" style="color: red;">(894.440.015,0)</div>
                            </div>
                            <div class="label">Tồn quỹ hiện tại</div>
                        </div>
                    </BaseCol>
                </BaseRow>
            </div>
            <div class="tabs">
                <ul>
                    <li v-tooltip="'Tính năng chưa phát triển!'">Tất cả</li>
                    <li v-tooltip="'Tính năng chưa phát triển!'">Thu tiền</li>
                    <li class="tab-active">Chi tiền</li>
                </ul>
            </div>
            <div class="filter-bar">
                <div class="collapse-overview flex justify-center items-center" @click="showOverview = !showOverview">
                    <div
                        class="icon icon-size-16 mi-chevron-up--primary"
                        :class="{ 'rotate-down': !showOverview }"
                    ></div>
                </div>
                <div class="filter-area">
                    <div class="check-all-arrow">
                        <div class="check-all-arrow-icon"></div>
                    </div>
                    <div class="check-all">
                        <BaseDropdownButton
                            buttonName="Thực hiện hàng loạt"
                            secondaryButton
                            :disabled="selected < 3"
                            :isOpen="isOpenCheckAll"
                            @click="isOpenCheckAll = !isOpenCheckAll"
                            @close="isOpenCheckAll = false"
                        >
                            <BatchExecution @handleClickDeleteMulti="handleClickDeleteMulti" />
                        </BaseDropdownButton>
                    </div>
                    <div class="filter">
                        <BaseDropdownButton
                            buttonName="Lọc"
                            secondaryButton
                            @click="isOpenFeatureFilter = !isOpenFeatureFilter"
                            :isOpen="isOpenFeatureFilter"
                            @close="isOpenFeatureFilter = false"
                        >
                            <FilterDropdown
                                :filterProp="filter"
                                @close="isOpenFeatureFilter = false"
                                @filterValue="getFilterValue"
                            />
                        </BaseDropdownButton>
                    </div>
                    <div class="filter-bar-center">
                        <div
                            class="filter-user-view-item"
                            v-for="(value, index) in filterView"
                            :key="index"
                            :class="{ 'filter-user-text-color': value[0] === 'time' }"
                        >
                            {{ value[1] }}
                            <div
                                v-if="value[0] !== 'time'"
                                class="icon icon-size-16 mi-close--small"
                                style="cursor: pointer;"
                                @click="() => deleteAItemFilter(value[0])"
                            ></div>
                        </div>
                        <div
                            v-if="filterView.length !== 0 && filterView[0][0] !== 'time'"
                            class="filter-user-view-item delete-condition"
                            @click="reloadPayment"
                        >
                            Xóa điều kiện lọc
                        </div>
                    </div>
                </div>
                <div class="filter-bar-right-area">
                    <div class="search-input">
                        <BaseInput
                            searchIcon
                            placeholder="Tìm theo mã, tên, số điện thoại"
                            fullWidth
                            v-model="filterText"
                            focusInput
                        />
                    </div>
                    <div class="reload-icon" @click="reloadPayment"></div>
                    <div class="icon icon-size-24 mi-excel__nav ml-12 mr-12"></div>
                    <div class="icon icon-size-24 mi-setting__list"></div>
                </div>
            </div>
            <div class="grid">
                <PaymentTable
                    :columnNames="columnNames"
                    :dataProps="payments"
                    @handleClickView="handleClickView"
                    @handleClickDelete="handleClickDelete"
                    @handleClickRelication="handleClickRelication"
                    @clickCheckbox="handleClickCheckbox"
                    :totals="totals"
                />
            </div>
        </div>
        <div class="pagination-bar">
            <div class="pagination-box">
                <div class="paging-left">
                    Tổng số: <b>{{ totalRecord }}</b> bản ghi
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
                    <div class="btn-prev-page" :class="{ 'disabled-text': pageNumber === 1 }" @click="prevPage">
                        Trước
                    </div>
                    <!-- Khi page có ít hơn 5 trang thì hiển thị toàn bộ số trang -->
                    <div class="pagination" v-if="totalPage < 6">
                        <div
                            v-for="page in totalPages"
                            :key="page"
                            class="pagination-item"
                            :class="{ active: page == pageNumber }"
                            @click="() => onPagination(page)"
                        >
                            {{ page }}
                        </div>
                    </div>
                    <!-- Nếu không thì sẽ hiện thêm dấu 3 chấm ở giữa -->
                    <div v-else>
                        <!-- Nếu trang hiện tại là 3 trang đầu tiên vd: 1 2 3 ... 50 -->
                        <div class="pagination" v-if="pageNumber < 3">
                            <div
                                v-for="page in [1, 2, 3]"
                                :key="page"
                                class="pagination-item"
                                :class="{ active: page == pageNumber }"
                                @click="() => onPagination(page)"
                            >
                                {{ page }}
                            </div>

                            <div class="pagination-item">
                                ...
                            </div>
                            <div
                                class="pagination-item"
                                :class="{ active: totalPage == pageNumber }"
                                @click="() => onPagination(totalPage)"
                            >
                                {{ totalPage }}
                            </div>
                        </div>
                        <!-- Nếu trang hiện tại lớn hơn 3 thì sẽ xuất hiện thêm 1 dấu 3 chấm nữa vd: 1 ... 4 5 6 ... 50 -->
                        <div v-else-if="pageNumber < totalPage - 1" class="pagination">
                            <div
                                class="pagination-item"
                                :class="{ active: pageNumber === 1 }"
                                @click="() => onPagination(1)"
                            >
                                1
                            </div>
                            <div class="pagination-item" v-if="pageNumber !== 3">
                                ...
                            </div>
                            <div
                                v-for="page in threePagesCloseTogether"
                                :key="page"
                                class="pagination-item"
                                :class="{ active: page == pageNumber }"
                                @click="() => onPagination(page)"
                            >
                                {{ page }}
                            </div>
                            <div class="pagination-item" v-if="pageNumber !== totalPage - 2">
                                ...
                            </div>
                            <div
                                class="pagination-item"
                                :class="{ active: pageNumber === totalPage }"
                                @click="() => onPagination(totalPage)"
                            >
                                {{ totalPage }}
                            </div>
                        </div>
                        <!-- Khi trang hiện tại là nhưng trang cuối cùng thì bỏ 1 dấu 3 chấm đi: 1 ... 48 49 50 -->
                        <div class="pagination" v-else>
                            <div
                                class="pagination-item"
                                :class="{ active: pageNumber === 1 }"
                                @click="() => onPagination(1)"
                            >
                                1
                            </div>

                            <div class="pagination-item">
                                ...
                            </div>
                            <div
                                v-for="page in [totalPage - 2, totalPage - 1, totalPage]"
                                :key="page"
                                class="pagination-item"
                                :class="{ active: page == pageNumber }"
                                @click="() => onPagination(page)"
                            >
                                {{ page }}
                            </div>
                        </div>
                    </div>

                    <div class="btn-next-page" :class="{ 'disabled-text': pageNumber === totalPage }" @click="nextPage">
                        Sau
                    </div>
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
import BatchExecution from "./dropdown/BatchExecution.vue";
import FilterDropdown from "./dropdown/FilterDropdown.vue";
import utils from "../../utils";
const columnNames = [
    { key: "posted_date", text: "Ngày hạch toán", width: 145, align: "center", format: "date" },
    { key: "refdate", text: "Ngày chứng từ", width: 150, align: "center", format: "date" },
    { key: "refno_finance", text: "Số chứng từ", width: 125 },
    { key: "journal_memo", text: "Diễn giải", width: 320 },
    { key: "total_amount", text: "Số tiền", width: 150, format: "currency", align: "right" },
    { key: "account_object_code", text: "Mã đối tượng", width: 150 },
    { key: "account_object_name", text: "Đối tượng", width: 150 },
    { key: "account_object_address", text: "Địa chỉ", width: 250 }
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

const defaultFilter = {
    CustomFilter: "",
    PageIndex: 1,
    PageSize: 20,
    time: 0,
    StartDate: `${new Date().getFullYear()}-01-01`,
    EndDate: utils.formatDateValueInput(new Date())
};
export default {
    name: "ReceiptPaymentList",
    components: { PaymentTable, BatchExecution, FilterDropdown },
    data() {
        return {
            columnNames: columnNames,
            payments: [],
            openDialog: false,
            currentPayment: {},
            stateDialog: enums.dialogState.post,
            totals: {},

            // pagination
            pageSizes: defaultPageSizes,
            pageSize: defaultPageSize,
            pageNumber: defaultPageNumber,
            totalPage: defaultTotalPage,
            totalRecord: defaultTotalRecord,
            filterText: defaultFilterText,

            idTimeout: null, // id của setTimeOut khi thực hiện filter

            showOverview: true,
            filter: defaultFilter,
            isOpenFeatureFilter: false,

            // index các phần tử đã chon trong bảng
            selected: [],
            isOpenCheckAll: false
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

                if (Number(query?.reload) === 1) {
                    this.reloadPayment();
                }
            },
            deep: true,
            immediate: true
        },
        /**
         * Khi pageNumber thay thổi thì sẽ load lại phiếu chi
         * Created by: VLVU (13/10/2021)
         */
        pageNumber() {
            this.payments = null;
            this.loadPayment();
        },

        /**
         * Khi pageNumber thay thổi thì sẽ load lại phiếu chi
         * Created by: VLVU (13/10/2021)
         */
        pageSize() {
            this.payments = null;
            this.pageNumber = defaultPageNumber;

            this.$router.push({ path: "receipt-payment-list", query: { page: 1 } }).catch(() => {});
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

                this.$router.push({ path: "receipt-payment-list", query: { page: 1 } }).catch(() => {});
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
        },

        /**
         * filter hiển thị cho người dùng
         */
        filterView() {
            const vFilter = { ...this.filter };
            delete vFilter.CustomFilter;
            delete vFilter.PageIndex;
            delete vFilter.PageSize;
            if (vFilter.time < 3) {
                switch (vFilter.time) {
                    case 0:
                        return [["time", "Từ đầu năm đến nay"]];
                    case 1:
                        return [["time", "Hôm nay"]];
                    case 2:
                        return [["time", "Tháng này"]];
                }
            }
            return [
                ["rangTime", `${utils.formatDateLocal(vFilter.StartDate)}-${utils.formatDateLocal(vFilter.EndDate)}`]
            ];
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

        deleteAItemFilter(key) {
            this.filter = { ...defaultFilter };
            this.loadPayment();
        },

        /**
         * Lấy thông tin filter từ bảng
         * Created by: VLVU (28/9/2021)
         */
        getFilterValue(filter) {
            this.filter = { ...this.filter, ...filter };
            this.loadPayment();
            this.isOpenFeatureFilter = false;
        },

        /**
         * Láy hết các index các hàng đã chọn
         * Created by: VLVU (29/9/2021)
         */
        handleClickCheckbox(selected) {
            this.selected = selected;
        },

        /**
         * sự kiến xóa nhiều
         * Created by: VLVU (10/10/2021)
         */
        async handleClickDeleteMulti() {
            this.isOpenCheckAll = false;
            const listId = [];
            this.selected.forEach(item => {
                listId.push(this.payments[item].refid);
            });

            const ok = await this.confirmPopup(resources.popup.deleteMultiple("phiếu chi"));
            if (!ok) {
                return;
            }

            try {
                const promise = await PaymentApi.deleteMultiple(listId);

                if (!promise?.data?.State) {
                    await this.confirmPopup(resources.popup.arisingRelatedProvider("", "Xóa"));
                    return;
                }
                this.setToast(resources.toast.deleteMultipleSuccess("phiếu chi"));
                this.selected = [];
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
        },

        /**
         * Hàm lấy dữ liệu và thông tin đơn vị
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        async getData() {
            try {
                const promise = await Promise.all([PaymentApi.getPaymentFilterPaging(defaultFilter)]);

                this.payments = promise[0]?.data?.Data?.result ?? [];

                this.totalPage = promise[0]?.data?.Data?.total_page === 0 ? 1 : promise[0]?.data?.Data?.total_page || 1; // số page luôn là 1
                this.totalRecord = promise[0]?.data?.Data?.total_record;
                this.totals = {
                    total_amount: promise[0]?.data?.Data?.total_amount_payment
                };
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
                this.filter = {
                    ...this.filter,
                    CustomFilter: this.filterText,
                    PageIndex: this.pageNumber,
                    PageSize: this.pageSize
                };
                const promise = await PaymentApi.getPaymentFilterPaging(this.filter);

                this.payments = promise?.data?.Data?.result ?? [];

                this.totalPage = promise?.data?.Data?.total_page === 0 ? 1 : promise?.data?.Data?.total_page || 1; // số page luôn là 1
                this.totalRecord = promise?.data?.Data?.total_record;

                this.totals = {
                    total_amount: promise?.data?.Data?.total_amount_payment
                };
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
            this.$router.push({ path: "receipt-payment-list", query: { page: defaultPageNumber } }).catch(() => {});
            this.totalPage = defaultTotalPage;
            this.totalRecord = defaultTotalRecord;
            this.filterText = defaultFilterText;

            this.filter = { ...defaultFilter };
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
        async handleClickDelete(payment) {
            this.currentPayment = payment;
            const ok = await this.confirmPopup(resources.popup.deleteOne(payment.refno_finance, "phiếu chi"));
            if (!ok) {
                return;
            }
            this.onDelete();
        },

        /**
         * Hàm khi người dùng dblclick vào 1 hàng hoặc ấn vào chữ sửa trên hàng đó
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        handleClickRelication(payment) {
            this.$router.push({
                name: "PaymentDetailsFromPaymentView",
                params: {
                    replicationId: payment.refid
                }
            });
        },

        /**
         * Hàm khi người dùng nhấn view trên row
         * Created by: Vũ Long Vũ (13/10/2021)
         */
        handleClickView(payment) {
            this.$router.push(`/popup/payment-details/${payment.refid}`);
        },

        /**
         * Hàm xóa 1 phiếu chi
         * Created by: Vũ Long Vũ (19/10/2021)
         */

        async onDelete() {
            try {
                await PaymentApi.deleteMultiple([this.currentPayment.refid]);
                this.setToast(resources.toast.deleteSuccess(this.currentPayment.refno_finance));
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
            this.$router.push({ path: "receipt-payment-list", query: { page: page } }).catch(() => {});
        },

        nextPage() {
            if (this.pageNumber === this.totalPage) {
                return;
            }
            this.$router.push({ path: "receipt-payment-list", query: { page: this.pageNumber + 1 } }).catch(() => {});
        },
        prevPage() {
            if (this.pageNumber - 1 === 0) {
                return;
            }
            this.$router
                .push({ path: "receipt-payment-list", query: { page: Math.max(0, this.pageNumber - 1) } })
                .catch(() => {});
        }
        // #endregion
    },

    // xóa sự kiện này khi thoát khỏi xóa component
    destroyed() {
        document.removeEventListener("click", this.handleClickOutside);
    }
};
</script>

<style scoped src="./style.css"></style>
