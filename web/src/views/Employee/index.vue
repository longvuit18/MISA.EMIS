<template>
    <div id="employee">
        <div class="employee-header">
            <div class="title-text">
                Nhân viên
            </div>
            <div class="group-button">
                <div class="add-button">
                    <BaseButton
                        buttonName="Thêm mới nhân viên"
                        @click="handleOpenDialog"
                    />
                </div>
            </div>
        </div>
        <div class="employee-body">

            <div class="filter-bar">
                <div class="search-input">
                    <BaseInput
                        searchIcon
                        placeholder="Tìm theo mã, tên, số điện thoại"
                        fullWidth
                        v-model="filterText"
                    />

                </div>
                <div class="reload-button">
                    <div
                        class="reload-icon"
                        @click="reloadEmployees"
                    ></div>
                </div>
            </div>
            <div class="grid">
                <EmployeeTable
                    :columnNames="columnNames"
                    :dataProps="employees"
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
                            v-model.number="pageSize"
                            readonly
                            optionId="value"
                            keyLabel="text"
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
        <EmployeeDetails
            v-if="openDialog"
            @onClose="onCloseDialog"
            :employee="currentEmployee"
            :departments="departments"
            :state="stateDialog"
            @reloadEmployees="reloadEmployees"
        />
    </div>

</template>

<script>

import EmployeeApi from "../../api/service/employee";
// import DepartmentApi from "../../api/service/department";
import EmployeeDetails from "./Details";
import { mapActions, mapMutations } from "vuex";
import enums from "../../enums";
import resources from "../../resources";
import EmployeeTable from "./Table";
const columnNames = [
    { key: "employee_code", text: "Mã nhân viên", width: 145 },
    { key: "employee_name", text: "Họ và tên", sort: true, width: 250 },
    { key: "gender_name", text: "Giới tính", width: 120 },
    { key: "date_of_birth", text: "Ngày sinh", width: 150, align: "center", format: "date" },
    { key: "identity_number", text: "Số CMND", width: 200 },
    { key: "identity_date", text: "Ngày cấp", width: 150, align: "center", format: "date" },
    { key: "identity_place", text: "Nơi cấp", width: 150 },
    { key: "employee_position", text: "Chức danh", width: 250 },
    { key: "department_name", text: "Tên đơn vị", width: 250 },
    { key: "BankAccount_number", text: "Số tài khoản", width: 150 },
    { key: "bank_name", text: "Tên ngân hàng", width: 250 },
    { key: "bank_branch_name", text: "Tên chi nhánh ngân hàng", width: 250 },
    { key: "bank_province_name", text: "Tỉnh/TP ngân hàng", width: 200 },
    { key: "phone_number", text: "Điện thoại", width: 200 },
    { key: "email", text: "Email", width: 200 },
    { key: "address", text: "Địa chỉ", width: 200 }
];

// các giá trị mặc định của paging và filter
const defaultFilterText = "";
const defaultPageNumber = 1;
const defaultTotalPage = 1;
const defaultTotalRecord = 0;
const defaultPageSizes = [
    { value: 10, text: "10 bản ghi trên 1 trang" },
    { value: 20, text: "20 bản ghi trên 1 trang" },
    { value: 30, text: "30 bản ghi trên 1 trang" },
    { value: 50, text: "50 bản ghi trên 1 trang" },
    { value: 100, text: "100 bản ghi trên 1 trang" }
];
const defaultPageSize = 20;
export default {
    name: "Employee",
    components: { EmployeeDetails, EmployeeTable },
    data() {
        return {
            columnNames: columnNames,
            employees: [],
            openDialog: false,
            currentEmployee: {},
            stateDialog: enums.dialogState.post,
            departments: [],

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
            this.employees = null;
            this.loadEmployees();
        },

        /**
         * Khi pageSize thay thổi thì sẽ load lại employee
         * Created by: VLVU (18/8/2021)
         */
        pageSize() {
            this.employees = null;
            this.pageNumber = defaultPageNumber;

            this.$router.push({ path: "employee", query: { page: defaultPageNumber } }).catch(() => { });
            this.loadEmployees();
        },

        /**
         * Khi search sẽ thực hiện filter
         * Created by: VLVU (18/8/2021)
         */
        filterText(value) {
            clearTimeout(this.idTimeout);

            this.idTimeout = setTimeout(() => {
                this.pageNumber = defaultPageNumber;

                this.$router.push({ path: "employee", query: { page: defaultPageNumber } }).catch(() => { });
                this.loadEmployees();
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
                    EmployeeApi.getEmployeeFilterPaging("", this.pageNumber, this.pageSize)
                    // DepartmentApi.getAll()
                ]);

                this.employees = promise[0]?.data?.Data ?? [];
                // this.departments = promise[1]?.data?.map(item => ({ value: item.DepartmentId, text: item.DepartmentName })) ?? [];

                this.totalPage = promise[0]?.data?.total_page === 0 ? 1 : promise[0]?.data?.total_page || 1; // số page luôn là 1
                this.totalRecord = promise[0]?.data?.total_record;
            } catch (error) {
                this.employees = [];
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
        async loadEmployees() {
            try {
                const promise = await EmployeeApi.getEmployeeFilterPaging(this.filterText.trim(), this.pageNumber, this.pageSize);

                this.employees = promise?.data.Data;
                this.totalPage = promise?.data?.total_page === 0 ? 1 : promise?.data?.total_page || 1;
                this.totalRecord = promise?.data.total_record;
            } catch (error) {
                this.employees = [];
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
        reloadEmployees() {
            this.employees = null;
            this.pageNumber = defaultPageNumber;
            this.$router.push({ path: "employee", query: { page: defaultPageNumber } }).catch(() => { });
            this.totalPage = defaultTotalPage;
            this.totalRecord = defaultTotalRecord;
            this.filterText = defaultFilterText;

            this.loadEmployees();
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
            this.currentEmployee = {};
            if (params?.hasReloadEmployees) {
                this.employees = null;
                this.reloadEmployees();
            }
        },
        /**
         * Hàm khi người dùng dblclick vào 1 hàng hoặc ấn vào chữ sửa trên hàng đó
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        handleClickEdit(employee) {
            this.currentEmployee = employee;
            this.stateDialog = enums.dialogState.put;
            this.openDialog = true;
        },

        /**
         * Hàm khi người dùng dblclick vào 1 hàng hoặc ấn vào chữ sửa trên hàng đó
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        async handleClickDelete(employee) {
            this.currentEmployee = employee;
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
            this.currentEmployee = employee;
            this.stateDialog = enums.dialogState.post;
            this.openDialog = true;
        },

        /**
         * Hàm xóa 1 nhân viên
         * Created by: Vũ Long Vũ (19/7/2021)
         */

        async onDelete() {
            try {
                await EmployeeApi.deleteOne(this.currentEmployee.EmployeeId);
                this.setToast(resources.toast.deleteEmployeeSuccess(this.currentEmployee.EmployeeCode));
                this.reloadEmployees();
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
            this.currentEmployee = {};
        },

        /**
         * Các hàm thực hiện pagination
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        // #region paging

        onPagination(page) {
            this.$router.push({ path: "employee", query: { page: page } }).catch(() => { });
        },

        nextPage() {
            if (this.pageNumber === this.totalPage) {
                return;
            }
            this.$router.push({ path: "employee", query: { page: this.pageNumber + 1 } }).catch(() => { });
        },
        prevPage() {
            if (this.pageNumber - 1 === 0) {
                return;
            }
            this.$router.push({ path: "employee", query: { page: Math.max(0, this.pageNumber - 1) } }).catch(() => { });
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
