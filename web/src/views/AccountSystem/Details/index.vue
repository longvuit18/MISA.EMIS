<template>
    <div>
        <BaseDialog
            isOpen
            :customContentStyle="{'justify-content': 'flex-end'}"
        >
            <div
                class="account-box"
                :style="dialogWidth"
            >
                <div class="header">
                    <div class="header-left">
                        <div class="title">Thêm tài khoản</div>
                    </div>
                    <div class="header-right">
                        <div
                            class="helper-button"
                            v-tooltip="'Tính năng đang phát triển'"
                        ></div>
                        <div
                            class="close-button"
                            @click="onClose"
                            v-tooltip="'Đóng (Esc)'"
                        ></div>
                    </div>
                </div>

                <div id="account-details">
                    <BaseRow>
                        <BaseCol :cols="3">
                            <BaseInput
                                label="Số tài khoản"
                                required
                                name="Số tài khoản"
                                tabindex="1"
                                ref="1"
                                fullWidth
                                v-model="currentAccount.account_number"
                                :disabled="viewMode"
                            />
                        </BaseCol>
                    </BaseRow>
                    <BaseRow>
                        <BaseCol
                            :cols="6"
                            class="pr-12"
                        >
                            <BaseInput
                                label="Tên tài khoản"
                                required
                                name="Tên tài khoản"
                                tabindex="2"
                                ref="2"
                                fullWidth
                                v-model="currentAccount.account_name"
                                :disabled="viewMode"
                            />
                        </BaseCol>
                        <BaseCol :cols="6">
                            <BaseInput
                                label="Tên tiếng anh"
                                name="Tên tiếng anh"
                                tabindex="3"
                                fullWidth
                                v-model="currentAccount.account_name_english"
                                :disabled="viewMode"
                            />
                        </BaseCol>
                    </BaseRow>
                    <BaseRow>
                        <BaseCol
                            :cols="3"
                            class="pr-12"
                        >
                            <BaseCombobox
                                label="Tài khoản tổng hợp"
                                name="Tài khoản tổng hợp"
                                tabindex="4"
                                fullWidth
                                optionsTable
                                treeTable
                                treeColumnId="account_id"
                                optionId="account_id"
                                keyLabel="account_number"
                                :columnNames="columnNames"
                                :items="accounts"
                                v-model="currentAccount.parent_id"
                                :disabled="viewMode"
                            />
                        </BaseCol>
                        <BaseCol
                            :cols="3"
                            class="pr-12"
                        >
                            <BaseCombobox
                                label="Tính chất"
                                required
                                name="Tính chất"
                                tabindex="5"
                                ref="5"
                                fullWidth
                                v-model="currentAccount.account_category_kind"
                                :items="properties"
                                optionId="id"
                                keyLabel="name"
                                :disabled="viewMode"
                            />
                        </BaseCol>
                    </BaseRow>
                    <BaseRow>
                        <BaseCol>
                            <BaseInput
                                label="Diễn giải"
                                isTextarea
                                rows="3"
                                name="Số tài khoản"
                                tabindex="6"
                                fullWidth
                                v-model="currentAccount.desciption"
                                :disabled="viewMode"
                            />
                        </BaseCol>
                    </BaseRow>
                    <BaseRow>
                        <BaseCol>
                            <BaseCheckbox
                                label="Có hoạch toán ngoại lệ"
                                tabindex="7"
                                v-model="currentAccount.is_postable_in_foreign_currency"
                                :disabled="viewMode"
                            />
                        </BaseCol>
                    </BaseRow>
                    <BaseRow>
                        <BaseCol :padding="10">
                            <div
                                class="monitor"
                                @click="showMonitorDetails = !showMonitorDetails"
                            >
                                <div class="monitor-title">Theo dõi chi tiết theo</div>
                                <div
                                    :class="{'arrow-right': !showMonitorDetails}"
                                    class="collapse-icon icon icon-size-16 mi-arrow-right--black"
                                />
                            </div>
                        </BaseCol>
                    </BaseRow>
                    <transition name="slide">
                        <div
                            class="monitor-details-area"
                            v-if="showMonitorDetails"
                        >
                            <BaseRow>
                                <BaseCol :cols="6">
                                    <BaseRow>
                                        <BaseCol :cols="10">
                                            <BaseRow class="pb-12 items-center">
                                                <BaseCol :cols="6">
                                                    <BaseCheckbox
                                                        label="Đối tượng"
                                                        tabindex="8"
                                                        v-model="currentAccount.detail_by_account_object"
                                                        :disabled="viewMode"
                                                    />
                                                </BaseCol>
                                                <BaseCol :cols="6">
                                                    <BaseCombobox
                                                        tabindex="9"
                                                        fullWidth
                                                        :disabled="!currentAccount.detail_by_account_object || viewMode"
                                                        v-model="currentAccount.account_object_type"
                                                        :items="objects"
                                                        optionId="id"
                                                        keyLabel="name"
                                                        readonly
                                                    />
                                                </BaseCol>
                                            </BaseRow>
                                        </BaseCol>
                                    </BaseRow>
                                    <BaseRow>
                                        <BaseCol :cols="10">
                                            <BaseRow class="pb-12 items-center">
                                                <BaseCol :cols="6">
                                                    <BaseCheckbox
                                                        label="Đối tượng THCP"
                                                        tabindex="8"
                                                        v-model="currentAccount.detail_by_job"
                                                        :disabled="viewMode"
                                                    />
                                                </BaseCol>
                                                <BaseCol :cols="6">
                                                    <BaseCombobox
                                                        tabindex="9"
                                                        fullWidth
                                                        :items="warnOrRequire"
                                                        optionId="id"
                                                        keyLabel="name"
                                                        readonly
                                                        :disabled="!currentAccount.detail_by_job || viewMode"
                                                        v-model="currentAccount.detail_by_job_kind"
                                                    />
                                                </BaseCol>
                                            </BaseRow>
                                        </BaseCol>
                                    </BaseRow>
                                    <BaseRow>
                                        <BaseCol :cols="10">
                                            <BaseRow class="pb-12 items-center">
                                                <BaseCol :cols="6">
                                                    <BaseCheckbox
                                                        label="Đơn đặt hàng"
                                                        tabindex="8"
                                                        v-model="currentAccount.detail_by_order"
                                                        :disabled="viewMode"
                                                    />
                                                </BaseCol>
                                                <BaseCol :cols="6">
                                                    <BaseCombobox
                                                        tabindex="9"
                                                        fullWidth
                                                        :items="warnOrRequire"
                                                        optionId="id"
                                                        keyLabel="name"
                                                        readonly
                                                        :disabled="!currentAccount.detail_by_order || viewMode"
                                                        v-model="currentAccount.detail_by_order_kind"
                                                    />
                                                </BaseCol>
                                            </BaseRow>
                                        </BaseCol>
                                    </BaseRow>
                                    <BaseRow>
                                        <BaseCol :cols="10">
                                            <BaseRow class="pb-12 items-center">
                                                <BaseCol :cols="6">
                                                    <BaseCheckbox
                                                        label="Hợp đồng mua"
                                                        tabindex="8"
                                                        v-model="currentAccount.detail_by_pu_contract"
                                                        :disabled="viewMode"
                                                    />
                                                </BaseCol>
                                                <BaseCol :cols="6">
                                                    <BaseCombobox
                                                        tabindex="9"
                                                        fullWidth
                                                        :items="warnOrRequire"
                                                        optionId="id"
                                                        keyLabel="name"
                                                        readonly
                                                        :disabled="!currentAccount.detail_by_pu_contract || viewMode"
                                                        v-model="currentAccount.detail_by_pu_contract_kind"
                                                    />
                                                </BaseCol>
                                            </BaseRow>
                                        </BaseCol>
                                    </BaseRow>
                                    <BaseRow>
                                        <BaseCol :cols="10">
                                            <BaseRow class="items-center">
                                                <BaseCol :cols="6">
                                                    <BaseCheckbox
                                                        label="Đơn vị"
                                                        tabindex="8"
                                                        v-model="currentAccount.detail_by_department"
                                                        :disabled="viewMode"
                                                    />
                                                </BaseCol>
                                                <BaseCol :cols="6">
                                                    <BaseCombobox
                                                        tabindex="9"
                                                        fullWidth
                                                        :items="warnOrRequire"
                                                        optionId="id"
                                                        keyLabel="name"
                                                        readonly
                                                        :disabled="!currentAccount.detail_by_department || viewMode"
                                                        v-model="currentAccount.detail_by_department_kind"
                                                    />
                                                </BaseCol>
                                            </BaseRow>
                                        </BaseCol>
                                    </BaseRow>
                                </BaseCol>
                                <BaseCol :cols="6">
                                    <BaseRow>
                                        <BaseCol :cols="10">
                                            <BaseRow class="pb-12 items-center">
                                                <BaseCol :cols="6">
                                                    <BaseCheckbox
                                                        label="Tài khoảng ngân hàng"
                                                        tabindex="8"
                                                        v-model="currentAccount.detail_by_bank_account"
                                                        :disabled="viewMode"
                                                    />
                                                </BaseCol>
                                            </BaseRow>
                                        </BaseCol>
                                    </BaseRow>
                                    <BaseRow>
                                        <BaseCol :cols="10">
                                            <BaseRow class="pb-12 items-center">
                                                <BaseCol :cols="6">
                                                    <BaseCheckbox
                                                        label="Công trình"
                                                        tabindex="8"
                                                        v-model="currentAccount.detail_by_project_work"
                                                        :disabled="viewMode"
                                                    />
                                                </BaseCol>
                                                <BaseCol :cols="6">
                                                    <BaseCombobox
                                                        tabindex="9"
                                                        fullWidth
                                                        :items="warnOrRequire"
                                                        optionId="id"
                                                        keyLabel="name"
                                                        readonly
                                                        :disabled="!currentAccount.detail_by_project_work || viewMode"
                                                        v-model="currentAccount.detail_by_project_work_kind"
                                                    />
                                                </BaseCol>
                                            </BaseRow>
                                        </BaseCol>
                                    </BaseRow>
                                    <BaseRow>
                                        <BaseCol :cols="10">
                                            <BaseRow class="pb-12 items-center">
                                                <BaseCol :cols="6">
                                                    <BaseCheckbox
                                                        label="Hợp đồng bán"
                                                        tabindex="8"
                                                        v-model="currentAccount.detail_by_contract"
                                                        :disabled="viewMode"
                                                    />
                                                </BaseCol>
                                                <BaseCol :cols="6">
                                                    <BaseCombobox
                                                        tabindex="9"
                                                        fullWidth
                                                        :items="warnOrRequire"
                                                        optionId="id"
                                                        keyLabel="name"
                                                        readonly
                                                        :disabled="!currentAccount.detail_by_contract || viewMode"
                                                        v-model="currentAccount.detail_by_contract_kind"
                                                    />
                                                </BaseCol>
                                            </BaseRow>
                                        </BaseCol>
                                    </BaseRow>
                                    <BaseRow>
                                        <BaseCol :cols="10">
                                            <BaseRow class="pb-12 items-center">
                                                <BaseCol :cols="6">
                                                    <BaseCheckbox
                                                        label="Khoản mục CP"
                                                        tabindex="8"
                                                        v-model="currentAccount.detail_by_expense_item"
                                                        :disabled="viewMode"
                                                    />
                                                </BaseCol>
                                                <BaseCol :cols="6">
                                                    <BaseCombobox
                                                        tabindex="9"
                                                        fullWidth
                                                        :items="warnOrRequire"
                                                        optionId="id"
                                                        keyLabel="name"
                                                        readonly
                                                        :disabled="!currentAccount.detail_by_expense_item || viewMode"
                                                        v-model="currentAccount.detail_by_expense_item_kind"
                                                    />
                                                </BaseCol>
                                            </BaseRow>
                                        </BaseCol>
                                    </BaseRow>
                                    <BaseRow>
                                        <BaseCol :cols="10">
                                            <BaseRow class="items-center">
                                                <BaseCol :cols="6">
                                                    <BaseCheckbox
                                                        label="Mã thống kê"
                                                        tabindex="8"
                                                        v-model="currentAccount.detail_by_list_item"
                                                        :disabled="viewMode"
                                                    />
                                                </BaseCol>
                                                <BaseCol :cols="6">
                                                    <BaseCombobox
                                                        tabindex="9"
                                                        fullWidth
                                                        :items="warnOrRequire"
                                                        optionId="id"
                                                        keyLabel="name"
                                                        readonly
                                                        :disabled="!currentAccount.detail_by_list_item || viewMode"
                                                        v-model="currentAccount.detail_by_list_item_kind"
                                                    />
                                                </BaseCol>
                                            </BaseRow>
                                        </BaseCol>
                                    </BaseRow>
                                </BaseCol>
                            </BaseRow>
                        </div>
                    </transition>
                </div>
                <div class="footer">
                    <div>
                        <BaseButton
                            buttonName="Hủy"
                            secondaryButton
                            @click="() => onClose(false)"
                        />
                    </div>
                    <div class="footer-right">
                        <div class="btn-first">
                            <BaseButton
                                buttonName="Cất"
                                secondaryButton
                                @click="() => onSave()"
                                v-tooltip="'Cất (Ctrl + S)'"
                            />
                        </div>
                        <BaseButton
                            buttonName="Cất và thêm"
                            @click="() => onSave(true)"
                            v-tooltip="'Cất (Ctrl + Shift + S)'"
                        />
                    </div>
                </div>
                <div
                    class="resize flex"
                    @click="fullWidth = !fullWidth"
                >
                    <div
                        class="resize-icon"
                        :class="{'rotate-icon': fullWidth}"
                    />
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

    </div>
</template>

<script>
import AccountiApi from "../../../api/service/account";
import { mapActions, mapMutations } from "vuex";
import resources from "../../../resources";
import enums from "../../../enums";

const columnNames = [
    { key: "account_number", text: "Số tài khoản" },
    { key: "account_name", text: "Tên tài khoản" }
];

const objects = [
    { id: 0, name: "Nhà cung cấp" },
    { id: 1, name: "Khách hàng" },
    { id: 2, name: "Nhân viên" }
];

const warnOrRequire = [
    { id: 0, name: "Chỉ cảnh báo" },
    { id: 1, name: "Bắt buộc nhập" }
];

const properties = [
    { id: 0, name: "Dư nợ" },
    { id: 1, name: "Dư có" },
    { id: 2, name: "Lưỡng tính" },
    { id: 3, name: "Không có số dư" }

];
export default {
    name: "AccountDetails",
    props: {
        account: Object,
        state: Number,
        accounts: {
            type: Array,
            default: () => []
        }
    },

    data: function () {
        return {
            format: enums.format,
            currentAccount: {
                ...this.account,
                account_category_kind: this.account?.account_category_kind || 0
            },
            disabledButton: true,
            // kiểm tra xem người dùng đã sửa hay thao tác gì với form chưa
            edited: false,
            // loading
            loading: false,

            currentState: this.state,
            viewMode: this.state === enums.dialogState.view,

            showMonitorDetails: true,
            fullWidth: false,

            columnNames,
            objects,
            warnOrRequire,
            properties

        };
    },

    async mounted() {
        document.addEventListener("keydown", (e) => this.listenerKeyPress(e));

        if (this.currentState === enums.dialogState.post) {
            this.$refs["1"].$refs.BaseInput.focus();
            this.edited = false;
        } else {
            this.$refs["1"].$refs.BaseInput.focus();
        }
    },
    destroyed() {
        document.removeEventListener("keydown", (e) => this.listenerKeyPress(e));
    },

    watch: {
        currentAccount: {
            handler(value, oldValue) {
                this.edited = true;
            },
            deep: true
        },

        "currentAccount.detail_by_account_object": {
            handler(value) {
                if (value === true) {
                    this.currentAccount.account_object_type = 0;
                } else {
                    this.currentAccount.account_object_type = null;
                }
            }
        },
        "currentAccount.detail_by_contract": {
            handler(value) {
                if (value === true) {
                    this.currentAccount.detail_by_contract_kind = 0;
                } else {
                    this.currentAccount.detail_by_contract_kind = null;
                }
            }
        },
        "currentAccount.detail_by_department": {
            handler(value) {
                if (value === true) {
                    this.currentAccount.detail_by_department_kind = 0;
                } else {
                    this.currentAccount.detail_by_department_kind = null;
                }
            }
        },

        "currentAccount.detail_by_expense_item": {
            handler(value) {
                if (value === true) {
                    this.currentAccount.detail_by_expense_item_kind = 0;
                } else {
                    this.currentAccount.detail_by_expense_item_kind = null;
                }
            }
        },
        "currentAccount.detail_by_job": {
            handler(value) {
                if (value === true) {
                    this.currentAccount.detail_by_job_kind = 0;
                } else {
                    this.currentAccount.detail_by_job_kind = null;
                }
            }
        },

        "currentAccount.detail_by_list_item": {
            handler(value) {
                if (value === true) {
                    this.currentAccount.detail_by_list_item_kind = 0;
                } else {
                    this.currentAccount.detail_by_list_item_kind = null;
                }
            }
        },

        "currentAccount.detail_by_order": {
            handler(value) {
                if (value === true) {
                    this.currentAccount.detail_by_order_kind = 0;
                } else {
                    this.currentAccount.detail_by_order_kind = null;
                }
            }
        },

        "currentAccount.detail_by_project_work": {
            handler(value) {
                if (value === true) {
                    this.currentAccount.detail_by_project_work_kind = 0;
                } else {
                    this.currentAccount.detail_by_project_work_kind = null;
                }
            }
        },

        "currentAccount.detail_by_pu_contract": {
            handler(value) {
                if (value === true) {
                    this.currentAccount.detail_by_pu_contract_kind = 0;
                } else {
                    this.currentAccount.detail_by_pu_contract_kind = null;
                }
            }
        }
    },

    computed: {
        dialogWidth() {
            return this.fullWidth ? {} : { width: "50%" };
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

        /**
         * validete account number
         * Created by: VLVU (4/10/2021)
         */
        validateAccountNumber() {
            const parentId = this.currentAccount.parent_id;
            if (parentId) {
                const children = this.accounts.filter(item => item.parent_id === parentId);
                const parent = this.accounts.find(item => item.account_id === parentId);
                if (!this.currentAccount.account_number.startsWith(parent.account_number)) {
                    this.setToast(resources.toast.accountNumberInvalid());
                    return false;
                };
                if (this.currentState !== enums.dialogState.put && this.accounts.findIndex(item => item.account_number === this.currentAccount.account_number) > -1) {
                    this.setToast(resources.toast.accountNumberDuplicate());
                    return false;
                }

                const indexDuplicate = children.findIndex(item => this.currentAccount.account_number.startsWith(item.account_number));
                if (indexDuplicate > -1) {
                    if (children[indexDuplicate]?.account_id === this.currentAccount.account_id) {
                        return true;
                    }
                    this.setToast(resources.toast.accountNumberDuplicateChild(children[indexDuplicate].account_number));
                    return false;
                }
            }
            return true;
        },

        /**
         * handle ấn vào đóng dialog
         * @param {boolean} confirm có hiện xác nhận popup hay không
         * Created by: VLVU (2021)
         */
        async onClose(confirm = true) {
            if (this.edited && confirm) {
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
            // chưa edit thì đóng form luôn
            this.$emit("onClose");
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
            Object.values(this.$refs).sort((a, b) => a[0] < b[0]).forEach((item, index) => {
                item.$refs.BaseInput.focus();
                item.$refs.BaseInput.blur();
                if (item.error === true) {
                    isError = true;
                    // thêm vị trí lỗi vào array
                    indexErrors.push(index);
                    // thêm câu thông báo lỗi vào array
                    errorsMessage.push(item.errorMessage);
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
         * Hàm cất dữ liệu, hàm này có thể thêm mới hoặc sửa dữ liệu
         * @param {bool} keepCreating Check xem có phải button cất và thêm không
         * Created by: VLVU (21/8/2021)
         */
        async onSave(keepCreating = false) {
            if (this.viewMode) {
                return;
            }
            const validate = await this.validate();
            if (!validate) {
                return;
            }
            const validateAccountNumber = this.validateAccountNumber();
            if (!validateAccountNumber) {
                return;
            }
            let promise = null;
            try {
                this.loading = true;
                // set level
                let level = 0;
                const parentId = this.currentAccount.parent_id;
                if (parentId) {
                    level = this.accounts.find(item => item.account_id === parentId).level + 1;
                }
                if (this.currentState === enums.dialogState.post) {
                    promise = await AccountiApi.insertOne({ ...this.currentAccount, level });
                } else {
                    promise = await AccountiApi.updateOne(this.currentAccount.account_id, { ...this.currentAccount, level });
                }

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
                if (this.currentState === enums.dialogState.post) {
                    this.setToast(resources.toast.addSuccess(this.currentAccount.account_name, "tài khoản"));
                } else {
                    this.setToast(resources.toast.updateSuccess(this.currentAccount.account_name, "tài khoản"));
                }
                this.currentEmployee = {};
                if (keepCreating) {
                    this.$refs["1"].$refs.BaseInput.focus();
                    this.loading = false;
                    this.edited = false;
                    // this.defaultDepartment = null;
                    // set lại currentState là post để nó có thể thêm mới
                    this.currentState = enums.dialogState.post;
                    this.$emit("reloadAccounts");
                    return;
                }
                this.$emit("onClose", { hasReloadAccounts: true });
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
        }
    }

};
</script>

<style scoped src="./style.css">
</style>
