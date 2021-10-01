<template>
    <div id="account-system">
        <div class="account-system-header">
            <div class="title-area flex justify-space-between">
                <div class="title-text">
                    Hệ thống tài khoản
                </div>
                <div class="group-button flex items-center">
                    <div class="switch-account mr-10">
                        <BaseButton
                            secondaryButton
                            buttonName="Chuyển tài khoản hạch toán"
                            style="border-radius: 30px"
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
            <div class="back">
                Quay lai
            </div>
        </div>
        <div class="account-system-body">

            <div class="filter-bar">
                <div class="search-input">
                    <BaseInput
                        searchIcon
                        placeholder="Tìm theo số, tên tài khoản"
                        fullWidth
                        v-model="filterText"
                    />

                </div>
                <div class="reload-button">
                    <div
                        class="reload-icon"
                        @click="reloadAccounts"
                    ></div>
                </div>
            </div>
            <div class="grid">
                <AccountTable
                    :columnNames="columnNames"
                    :dataProps="accounts"
                    @handleClickEdit="handleClickEdit"
                    @handleClickDelete="handleClickDelete"
                    @handleClickRelication="handleClickRelication"
                />
            </div>
        </div>
        <div class="pagination-bar">
            <div class="pagination-box">
                <div class="paging-left">
                    Tổng số: <b>{{100}}</b> bản ghi
                </div>
            </div>
        </div>
        <AccountDetails
            v-if="openDialog"
            @onClose="onCloseDialog"
            :employee="currentAccount"
            :state="stateDialog"
            @reloadAccounts="reloadAccounts"
        />
    </div>

</template>

<script>

import EmployeeApi from "../../api/service/employee";
import AccountDetails from "./Details";
import { mapActions, mapMutations } from "vuex";
import enums from "../../enums";
import resources from "../../resources";
import AccountTable from "./Table";
const columnNames = [
    { key: "account_number", text: "Số tài khoản", width: 145 },
    { key: "account_name", text: "Tên tài khoản", width: 250 },
    { key: "property_name", text: "Tính chất", width: 120 },
    { key: "account_eng_name", text: "Tên tiếng anh", width: 250 },
    { key: "description", text: "Diễn giải", width: 250 },
    { key: "state", text: "Trạng thái", width: 150 }
];

// các giá trị mặc định của paging và filter
const defaultFilterText = "";
export default {
    name: "AccountSystem",
    components: { AccountDetails, AccountTable },
    data() {
        return {
            columnNames: columnNames,
            accounts: [
                { account_id: "1", account_name: "test1", account_number: 111, parent_id: "", level: 0 },
                { account_id: "2", account_name: "test2", account_number: 1112, parent_id: "1", level: 1 },
                { account_id: "3", account_name: "test3", account_number: 1113, parent_id: "1", level: 1 },
                { account_id: "4", account_name: "test4", account_number: 100, parent_id: "", level: 0 },
                { account_id: "5", account_name: "test5", account_number: 11132, parent_id: "3", level: 2 }

            ],
            openDialog: false,
            currentAccount: {},
            stateDialog: enums.dialogState.post,

            filterText: defaultFilterText,

            idTimeout: null // id của setTimeOut khi thực hiện filter

        };
    },

    watch: {

        /**
         * Khi search sẽ thực hiện filter
         * Created by: VLVU (18/8/2021)
         */
        filterText(value) {
            clearTimeout(this.idTimeout);

            this.idTimeout = setTimeout(() => {
                this.loadAccounts();
            }, 700);
        }
    },

    computed: {
    },
    /**
     * Lấy thông tin của toàn bộ nhân viên
     * Lấy các vị trí và các phong ban
     * CreatedBy: Vũ Long Vũ 16/7/2021
     */
    mounted() {
        // this.getData();
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
                // const promise = await Promise.all([
                //     EmployeeApi.getEmployeeFilterPaging("", this.pageNumber, this.pageSize),
                //     DepartmentApi.getAll()
                // ]);

                // this.employees = promise[0]?.data?.Data ?? [];
                // this.departments = promise[1]?.data?.map(item => ({ value: item.DepartmentId, text: item.DepartmentName })) ?? [];

                // this.totalPage = promise[0]?.data?.TotalPage === 0 ? 1 : promise[0]?.data?.TotalPage || 1; // số page luôn là 1
                // this.totalRecord = promise[0]?.data?.TotalRecord;
            } catch (error) {
                this.accounts = [];
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
        async loadAccounts() {
            try {

            } catch (error) {
                this.accounts = [];
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
        reloadAccounts() {
            this.accounts = null;
            this.filterText = defaultFilterText;

            this.loadAccounts();
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
            this.currentAccount = {};
            if (params?.hasReloadAccounts) {
                this.accounts = null;
                this.reloadAccounts();
            }
        },
        /**
         * Hàm khi người dùng dblclick vào 1 hàng hoặc ấn vào chữ sửa trên hàng đó
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        handleClickEdit(employee) {
            this.currentAccount = employee;
            this.stateDialog = enums.dialogState.put;
            this.openDialog = true;
        },

        /**
         * Hàm khi người dùng dblclick vào 1 hàng hoặc ấn vào chữ sửa trên hàng đó
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        async handleClickDelete(employee) {
            this.currentAccount = employee;
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
            this.currentAccount = employee;
            this.stateDialog = enums.dialogState.post;
            this.openDialog = true;
        },

        /**
         * Hàm xóa 1 nhân viên
         * Created by: Vũ Long Vũ (19/7/2021)
         */

        async onDelete() {
            try {
                await EmployeeApi.deleteOne(this.currentAccount.EmployeeId);
                this.setToast(resources.toast.deleteEmployeeSuccess(this.currentAccount.EmployeeCode));
                this.reloadAccounts();
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
            this.currentAccount = {};
        }
    },

    // xóa sự kiện này khi thoát khỏi xóa component
    destroyed() {
        document.removeEventListener("click", this.handleClickOutside);
    }
};
</script>

<style scoped src="./style.css">
</style>
