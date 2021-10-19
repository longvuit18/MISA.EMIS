<template>
    <div id="provider">
        <div class="provider-header">
            <div class="title-area">
                <div class="title-text">
                    Danh sách nhà cung cấp
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
                            buttonName="Thêm"
                            @click="handleOpenDialog"
                        />
                    </div>
                </div>
            </div>
            <div
                class="back"
                @click="() => $router.push('/cash/process')"
            >
                Quay lai
            </div>
        </div>

        <div class="provider-body">
            <div
                class="overview"
                v-if="showOverview"
            >
                <BaseRow>
                    <BaseCol
                        :cols="4"
                        style="padding-right: 3px"
                    >
                        <div class="overview-box">
                            <div class="debit-color">
                                <div class="mouse-pointer">
                                    <div class="data">
                                        <div class="total-money">0,0</div>
                                    </div>
                                    <div class="label">Nợ quá hạn</div>
                                    <div class="icon icon-size-18 mi-funnel icon-area"></div>
                                </div>
                                <div class="overview-line"></div>
                            </div>
                        </div>
                    </BaseCol>
                    <BaseCol
                        :cols="4"
                        style="padding: 0 3px;"
                    >
                        <div class="overview-box ">
                            <div class="total-debit-color">
                                <div class="mouse-pointer">
                                    <div class="data">
                                        <div class="total-money">10.073.000,0</div>
                                    </div>
                                    <div class="label">Tổng phải trả</div>
                                    <div class="icon icon-size-18 mi-funnel icon-area"></div>
                                </div>
                                <div class="overview-line"></div>
                            </div>
                        </div>
                    </BaseCol>
                    <BaseCol
                        :cols="4"
                        style="padding:0 0 0 3px;"
                    >
                        <div
                            class=""
                            style="padding-top:6px;"
                        >
                            <div class="payment-color">
                                <div class="mouse-pointer">
                                    <div class="data">
                                        <div class="total-money">0,0</div>
                                    </div>
                                    <div class="label">Đã thanh toán (30 ngày gần nhất)</div>
                                </div>
                                <div class="overview-line"></div>
                            </div>
                        </div>
                    </BaseCol>
                </BaseRow>
            </div>
            <div class="filter-bar">
                <div
                    class="collapse-overview flex justify-center items-center"
                    @click="showOverview = !showOverview"
                >
                    <div
                        class="icon icon-size-16 mi-chevron-up--primary"
                        :class="{'rotate-down': !showOverview}"
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
                            :disabled="selected < 1"
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
                        >
                            {{value[1]}}
                            <div
                                class="icon icon-size-16 mi-close--small"
                                style="cursor: pointer;"
                                @click="() => deleteAItemFilter(value[0])"
                            ></div>
                        </div>
                        <div
                            v-if="filterView.length !== 0"
                            class="filter-user-view-item delete-condition"
                            @click="reloadProviders"
                        >Xóa điều kiện lọc</div>
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
                    <div
                        class="reload-icon"
                        @click="reloadProviders"
                    ></div>
                    <div class="icon icon-size-24 mi-excel__nav ml-12 mr-12"></div>
                    <div class="icon icon-size-24 mi-setting__list"></div>
                </div>
            </div>
            <div class="grid">
                <ProviderTable
                    :columnNames="columnNames"
                    :dataProps="providers"
                    :filterProp="filter"
                    @handleClickEdit="handleClickEdit"
                    @handleClickView="handleClickView"
                    @handleClickDelete="handleClickDelete"
                    @handleClickRelication="handleClickRelication"
                    @clickCheckbox="handleClickCheckbox"
                    @filterValue="getFilterValue"
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
                            readonly
                            optionId="value"
                            keyLabel="label"
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
        <ProviderDetails
            v-if="openDialog"
            @onClose="onCloseDialog"
            :provider="currentProvider"
            :state="stateDialog"
            @reloadProviders="reloadProviders"
            :countries="countries"
        />
    </div>

</template>

<script>
/* eslint-disable vue/no-unused-vars */
import ProviderApi from "../../api/service/provider";
import LocationApi from "../../api/service/location";
import ProviderDetails from "./Details";
import { mapActions, mapMutations } from "vuex";
import enums from "../../enums";
import resources from "../../resources";
import ProviderTable from "./Table";
import FilterDropdown from "./dropdown/FilterDropdown.vue";
import BatchExecution from "./dropdown/BatchExecution.vue";
const columnNames = [
    { key: "account_object_code", text: "Mã nhà cung cấp", width: 180 },
    { key: "account_object_name", text: "Tên nhà cung cấp", width: 430 },
    { key: "address", text: "Địa chỉ", width: 250 },
    { key: "description", text: "Diễn giải", width: 500 },
    { key: "todo", text: "Công nợ", width: 180, align: "right" },
    { key: "account_object_group", text: "Nhóm KH, nhà cung cấp", width: 180 },
    { key: "tax_code", text: "Mã số thuế", width: 180 },
    { key: "phone_number", text: "Số điện thoại", width: 180 },
    { key: "personal_contact_identity_number", text: "Số CMND", width: 180 }
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

const defaultFilter = {
    CustomFilter: "",
    PageIndex: 1,
    PageSize: 20
};
const defaultPageSize = 20;
export default {
    name: "Provider",
    components: { ProviderDetails, ProviderTable, FilterDropdown, BatchExecution },
    data() {
        return {
            columnNames: columnNames,
            providers: [],
            openDialog: false,
            currentProvider: {},
            stateDialog: enums.dialogState.post,
            departments: [],

            // pagination
            pageSizes: defaultPageSizes,
            pageSize: defaultPageSize,
            pageNumber: defaultPageNumber,
            totalPage: defaultTotalPage,
            totalRecord: defaultTotalRecord,
            filterText: defaultFilterText,

            filter: defaultFilter,

            idTimeout: null, // id của setTimeOut khi thực hiện filter,

            showOverview: true,

            countries: [],
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
            },
            deep: true,
            immediate: true
        },
        /**
         * Khi pageNumber thay thổi thì sẽ load lại employee
         * Created by: VLVU (18/8/2021)
         */
        pageNumber() {
            this.providers = null;
            this.loadProviders();
        },

        /**
         * Khi pageNumber thay thổi thì sẽ load lại employee
         * Created by: VLVU (18/8/2021)
         */
        pageSize() {
            this.providers = null;
            this.pageNumber = defaultPageNumber;

            this.$router.push({ path: "provider", query: { page: defaultPageNumber } }).catch(() => { });
            this.loadProviders();
        },

        /**
         * Khi search sẽ thực hiện filter
         * Created by: VLVU (18/8/2021)
         */
        filterText(value) {
            clearTimeout(this.idTimeout);

            this.idTimeout = setTimeout(() => {
                this.pageNumber = defaultPageNumber;

                this.$router.push({ path: "provider", query: { page: defaultPageNumber } }).catch(() => { });
                this.loadProviders();
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
            return Object.entries(vFilter).filter(item => item[1])
                .map(item => {
                    const i = columnNames.findIndex(c => c.key === item[0]);
                    if (i > -1) {
                        return [item[0], `${columnNames[i].text} chứa "${item[1]}"`];
                    }
                    return item;
                });
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
                listId.push(this.providers[item].account_object_id);
            });

            const ok = await this.confirmPopup(resources.popup.deleteMultiple("nhà cung cấp"));
            if (!ok) {
                return;
            }

            try {
                const promise = await ProviderApi.deleteMultiple(listId);

                if (!promise?.data?.State) {
                    await this.confirmPopup(resources.popup.arisingRelatedProvider("", "Xóa"));
                    return;
                }
                this.setToast(resources.toast.deleteSuccess(this.currentProvider.provider_code, "nhà cung cấp"));
                this.selected = [];
                this.reloadProviders();
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
         * Lấy thông tin filter từ bảng
         * Created by: VLVU (28/9/2021)
         */
        getFilterValue(filter) {
            this.filter = { ...this.filter, ...filter };
            this.loadProviders();
            this.isOpenFeatureFilter = false;
        },

        /**
         * Xóa 1 điều kiện lọc
         * @param {string} key key của trường cần xóa
         * Created by: VLVU(29/9/2021)
         */
        deleteAItemFilter(key) {
            const tFilter = { ...this.filter };
            delete tFilter[key];
            this.filter = tFilter;

            this.loadProviders();
        },

        /**
         * Hàm lấy dữ liệu
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        async getData() {
            try {
                const promise = await Promise.all([
                    ProviderApi.getProviderFilterPaging(this.filter),
                    LocationApi.getCountries()
                ]);
                this.providers = promise[0].data?.Data.result ?? [];
                delete promise[1].data.country_code;
                this.countries = promise[1]?.data || [];
                this.totalPage = promise[0]?.data?.Data?.total_page === 0 ? 1 : promise[0]?.data?.Data?.total_page || 1; // số page luôn là 1
                this.totalRecord = promise[0]?.data?.Data?.total_record;
            } catch (error) {
                console.error(error);
                this.providers = [];
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
         * Hàm tải lại dữ liệu nhà cung cấp
         * Created by: VLVU (10/8/2021)
         */
        async loadProviders() {
            try {
                this.filter = {
                    ...this.filter,
                    CustomFilter: this.filterText,
                    PageIndex: this.pageNumber,
                    PageSize: this.pageSize
                };
                const promise = await ProviderApi.getProviderFilterPaging(this.filter);

                this.providers = promise.data?.Data.result ?? [];
                this.totalPage = promise?.data?.Data?.total_page === 0 ? 1 : promise?.data?.Data?.total_page || 1; // số page luôn là 1
                this.totalRecord = promise?.data?.Data?.total_record;
            } catch (error) {
                console.error(error);
                this.providers = [];
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
         * Hàm reload hoàn toàn lại bảng nhà cung cấp
         * Created by: Vũ Long Vũ (20/9/2021)
         */
        reloadProviders() {
            this.providers = null;
            this.pageNumber = defaultPageNumber;
            this.$router.push({ path: "provider", query: { page: defaultPageNumber } }).catch(() => { });
            this.totalPage = defaultTotalPage;
            this.totalRecord = defaultTotalRecord;
            this.filterText = defaultFilterText;
            this.filter = { ...defaultFilter };

            this.loadProviders();
        },

        /**
         * Hàm mở dialog
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        handleOpenDialog() {
            this.stateDialog = enums.dialogState.post;
            this.openDialog = true;
        },

        /**
         * Đóng dialog form chi tiết nhân viên
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        onCloseDialog(params) {
            this.openDialog = false;
            this.currentProvider = {};
            if (params?.hasReloadProviders) {
                this.providers = null;
                this.reloadProviders();
            }
        },
        /**
         * Hàm khi người dùng dblclick vào 1 hàng hoặc ấn vào chữ sửa trên hàng đó
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        handleClickEdit(provider) {
            this.currentProvider = provider;
            this.stateDialog = enums.dialogState.put;
            this.openDialog = true;
        },

        /**
         * Hàm khi người dùng dblclick vào 1 hàng hoặc ấn vào chữ sửa trên hàng đó
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        async handleClickDelete(provider) {
            this.currentProvider = provider;
            const ok = await this.confirmPopup(resources.popup.deleteOne(provider.account_object_code, "nhà cung cấp"));
            if (!ok) {
                return;
            }
            this.onDelete();
        },

        /**
         * Hàm khi người dùng ấn vào nhân bản
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        handleClickRelication(provider) {
            this.currentProvider = provider;
            this.stateDialog = enums.dialogState.post;
            this.openDialog = true;
        },

        handleClickView(provider) {
            this.currentProvider = provider;
            this.stateDialog = enums.dialogState.view;
            this.openDialog = true;
        },

        /**
         * Hàm xóa 1 nhà cung cấp
         * Created by: Vũ Long Vũ (124/9/2021)
         */

        async onDelete() {
            try {
                const promise = await ProviderApi.deleteOne(this.currentProvider.account_object_id);
                if (!promise?.data?.State) {
                    await this.confirmPopup(resources.popup.arisingRelatedProvider(this.currentProvider.account_object_name, "Xóa"));
                    return;
                }

                this.setToast(resources.toast.deleteSuccess(this.currentProvider.account_object_code, "nhà cung cấp"));
                this.reloadProviders();
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
            this.currentProvider = {};
        },

        /**
         * Các hàm thực hiện pagination
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        // #region paging

        onPagination(page) {
            this.$router.push({ path: "provider", query: { page: page } }).catch(() => { });
        },

        nextPage() {
            if (this.pageNumber === this.totalPage) {
                return;
            }
            this.$router.push({ path: "provider", query: { page: this.pageNumber + 1 } }).catch(() => { });
        },
        prevPage() {
            if (this.pageNumber - 1 === 0) {
                return;
            }
            this.$router.push({ path: "provider", query: { page: Math.max(0, this.pageNumber - 1) } }).catch(() => { });
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

<style>
.filter-content {
    height: 300px;
    background-color: #fff;
    padding: 16px 20px 20px;
}
</style>
