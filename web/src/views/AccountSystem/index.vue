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
            <div class="back" @click="() => $router.push('/cash/process')">
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
                <div class="flex">
                    <div
                        class="control-table mr-12"
                        @click="extendTable = !extendTable"
                    >{{!extendTable ? 'Thu gọn' : 'Mở rộng'}}</div>
                    <div
                        class="reload-icon"
                        @click="reloadAccounts"
                    ></div>
                    <div class="icon icon-size-24 mi-excel__nav ml-12 mr-12"></div>
                </div>
            </div>
            <div class="grid">
                <AccountTable
                    :columnNames="columnNames"
                    :dataProps="accounts"
                    :extendTable="extendTable"
                    disabledFilter
                    @handleClickEdit="handleClickEdit"
                    @handleClickDelete="handleClickDelete"
                    @handleClickRelication="handleClickRelication"
                    @handleClickView="handleClickView"
                />
            </div>
        </div>
        <div class="pagination-bar">
            <div class="pagination-box">
                <div class="paging-left">
                    Tổng số: <b>{{totalRecord}}</b> bản ghi
                </div>
            </div>
        </div>
        <Details
            v-if="openDialog"
            @onClose="onCloseDialog"
            :account="currentAccount"
            :state="stateDialog"
            @reloadAccounts="reloadAccounts"
            :accounts="accounts"
        />
    </div>

</template>

<script>

import AccountApi from "../../api/service/account";
import Details from "./Details";
import { mapActions, mapMutations } from "vuex";
import enums from "../../enums";
import resources from "../../resources";
import AccountTable from "./Table";
import { removeVietnameseTones } from "../../utils";
const columnNames = [
    { key: "account_number", text: "Số tài khoản", width: 145 },
    { key: "account_name", text: "Tên tài khoản", width: 250 },
    { key: "property_name", text: "Tính chất", width: 130 },
    { key: "account_name_english", text: "Tên tiếng anh", width: 270 },
    { key: "description", text: "Diễn giải", width: 250 },
    { key: "state", text: "Trạng thái", width: 150 }
];

const properties = [
    { id: 0, name: "Dư nợ" },
    { id: 1, name: "Dư có" },
    { id: 2, name: "Lưỡng tính" },
    { id: 3, name: "Không có số dư" }

];

// các giá trị mặc định của paging và filter
const defaultFilterText = "";
const defaultTotalRecord = 0;
export default {
    name: "AccountSystem",
    components: { Details, AccountTable },
    data() {
        return {
            fixedData: [], // dữ liệu cố định
            columnNames: columnNames,
            accounts: null,
            openDialog: false,
            currentAccount: {},
            stateDialog: enums.dialogState.post,

            filterText: defaultFilterText,
            totalRecord: defaultTotalRecord,

            idTimeout: null, // id của setTimeOut khi thực hiện filter,

            extendTable: false

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
                this.search();
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
         * tính năng search cho bảng
         * Created by: VLVU (5/10/2021)
         */
        search() {
            const textSearch = removeVietnameseTones(this.filterText);
            this.accounts = this.fixedData.filter((item) => {
                return removeVietnameseTones(item.account_number)?.toLowerCase()?.indexOf(textSearch.toLowerCase()) > -1 ||
                    removeVietnameseTones(item.account_name)?.toLowerCase()?.indexOf(textSearch.toLowerCase()) > -1;
            });
        },

        /**
         * Hàm lấy dữ liệu và thông tin đơn vị
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        async getData() {
            try {
                const promise = await AccountApi.getAll();

                this.accounts = promise?.data?.Data.map(item => {
                    const property = properties.find(x => x.id === item.account_category_kind);
                    return {
                        ...item,
                        property_name: property.name,
                        state: "Đang sử dụng"
                    };
                }) ?? [];
                this.fixedData = [...this.accounts];
                this.totalRecord = this.accounts.length;
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

            this.getData();
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
        handleClickEdit(account) {
            this.currentAccount = account[0];
            this.stateDialog = enums.dialogState.put;
            this.openDialog = true;
        },

        /**
         * Hàm khi người dùng dblclick vào 1 hàng hoặc ấn vào chữ sửa trên hàng đó
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        async handleClickDelete(account) {
            this.currentAccount = account;
            const ok = await this.confirmPopup(resources.popup.deleteOne(account[0].account_name, "tài khoản"));
            if (!ok) {
                return;
            }
            this.onDelete();
        },

        /**
         * Hàm khi người dùng dblclick vào 1 hàng hoặc ấn vào chữ sửa trên hàng đó
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        handleClickRelication(account) {
            this.currentAccount = account[0];
            this.stateDialog = enums.dialogState.post;
            this.openDialog = true;
        },

        /**
         * Hàm khi người dùng click view
         * Created by: Vũ Long Vũ (19/7/2021)
         */
        handleClickView(account) {
            this.currentAccount = account[0];
            this.stateDialog = enums.dialogState.view;
            this.openDialog = true;
        },

        /**
         * Hàm xóa 1 account
         * Created by: Vũ Long Vũ (19/10/2021)
         */

        async onDelete() {
            if (this.currentAccount[1] === "is-parent") {
                await this.confirmPopup(resources.popup.deleteAccountFail());
                this.currentAccount = {};
                return;
            }
            try {
                await AccountApi.deleteOne(this.currentAccount[0].account_id);
                this.setToast(resources.toast.deleteSuccess(this.currentAccount[0].account_number, "tài khoản"));
                this.reloadAccounts();
                this.currentAccount = {};
            } catch (error) {
                if (error?.response?.status === enums.statusCode.serverError) {
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
