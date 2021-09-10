<template>
    <div>
        <BaseDialog isOpen>
            <div class="e-box">
                <div class="dialog-header">
                    <div class="header-left">
                        <div class="title">Thông tin nhà cung cấp</div>

                        <BaseRadioGroup
                            style="padding: 0 13px;"
                            :values="[{value: 0, text: 'Tổ chức'}, {value: 1, text: 'Cá nhân'}]"
                            :value="0"
                            horizontal
                        />
                        <div
                            style="margin-left: 100px;"
                            v-tooltip="'Tính năng đang phát triển'"
                        >
                            <BaseCheckbox label="Là khách hàng" />
                        </div>
                        <!--
                        <div
                            style="padding:0 20px;"
                            v-tooltip="'Tính năng đang phát triển'"
                        >
                            <BaseCheckbox label="Là nhà cung cấp" />
                        </div> -->
                    </div>
                    <div class="header-right">

                        <div
                            class="helper-button"
                            v-tooltip="'Tính năng đang phát triển'"
                        ></div>
                        <div
                            class="close-button"
                            @click="onClose"
                            v-tooltip="'Đóng'"
                        ></div>
                    </div>
                </div>

                <div id="employee-details">
                    <BaseRow>
                        <BaseCol
                            :padding="0"
                            :cols="6"
                            style="padding-right: 13px"
                        >
                            <BaseRow>
                                <BaseCol
                                    :padding="0"
                                    :cols="5"
                                    style="padding-right: 6px"
                                >
                                    <BaseInput
                                        fullWidth
                                        label="Mã số thuế"
                                        tabindex="1"
                                        ref="1"
                                        name="Mã số thuế"
                                    />
                                </BaseCol>
                                <BaseCol
                                    :padding="0"
                                    :cols="7"
                                >
                                    <BaseInput
                                        fullWidth
                                        label="Mã nhà cung cấp"
                                        required
                                        tabindex="2"
                                        ref="2"
                                        name="Mã nhà cung cấp"
                                    />
                                </BaseCol>
                            </BaseRow>
                            <BaseRow>
                                <BaseCol
                                    :padding="0"
                                    :cols="12"
                                >
                                    <BaseInput
                                        fullWidth
                                        label="Tên nhà cung cấp"
                                        required
                                        tabindex="3"
                                        ref="3"
                                        name="Tên nhà cung cấp"
                                    />
                                </BaseCol>
                            </BaseRow>
                            <BaseRow>
                                <BaseCol
                                    :padding="0"
                                    :cols="12"
                                >
                                    <BaseInput
                                        fullWidth
                                        label="Địa chỉ"
                                        tabindex="4"
                                        name="Địa chỉ"
                                        placeholder="VD: Số 82 Duy Tân, Dịch Vọng Hậu, Cầu Giấy, Hà Nội"
                                        isTextarea
                                        :rows="3"
                                    />
                                </BaseCol>
                            </BaseRow>
                        </BaseCol>
                        <BaseCol
                            :padding="0"
                            :cols="6"
                            style="padding-left: 13px"
                        >
                            <BaseRow>
                                <BaseCol
                                    :padding="0"
                                    :cols="5"
                                    style="padding-right: 6px"
                                >
                                    <BaseInput
                                        fullWidth
                                        label="Điện thoại"
                                        tabindex="5"
                                        name="Điện thoại"
                                    />
                                </BaseCol>
                                <BaseCol
                                    :padding="0"
                                    :cols="7"
                                >
                                    <BaseInput
                                        fullWidth
                                        label="Website"
                                        tabindex="6"
                                        name="Website"
                                    />
                                </BaseCol>
                            </BaseRow>
                            <BaseRow>
                                <BaseCol
                                    :padding="0"
                                    :cols="12"
                                >
                                    <BaseCombobox
                                        fullWidth
                                        label="Nhóm nhà cung cấp"
                                        tabindex="7"
                                        ref="7"
                                        name="Nhóm nhà cung cấp"
                                    />
                                </BaseCol>
                            </BaseRow>
                            <BaseRow>
                                <BaseCol
                                    :padding="0"
                                    :cols="12"
                                >
                                    <BaseCombobox
                                        fullWidth
                                        label="Nhân viên mua hàng"
                                        tabindex="8"
                                        ref="8"
                                        name="Nhân viên mua hàng"
                                    />
                                </BaseCol>
                            </BaseRow>
                        </BaseCol>
                    </BaseRow>
                    <div class="other-information">
                        <div class="tabs">
                            <div
                                :class="{'action': tab === 'contact'}"
                                class="tab"
                                @click="() => getTab('contact')"
                            >Liên hệ</div>
                            <div
                                class="tab"
                                :class="{'action': tab === 'terms'}"
                                @click="() => getTab('terms')"
                            >Điều khoản thanh toán</div>
                            <div
                                class="tab"
                                :class="{'action': tab === 'bankAccount'}"
                                @click="() => getTab('bankAccount')"
                            >Tài khoản ngân hàng</div>
                            <div
                                class="tab"
                                :class="{'action': tab === 'address'}"
                                @click="() => getTab('address')"
                            >Địa chỉ khác</div>
                            <div
                                class="tab"
                                :class="{'action': tab === 'note'}"
                                @click="() => getTab('note')"
                            >Ghi chú</div>
                        </div>
                        <div class="details">
                            <Contact v-if="tab === 'contact'" />
                            <Terms v-if="tab === 'terms'" />
                            <BankAccount v-if="tab === 'bankAccount'" />
                            <Address v-if="tab === 'address'" />
                            <div
                                class="note"
                                v-if="tab === 'note'"
                            >
                                <BaseInput
                                    isTextarea
                                    rows="11"
                                    fullWidth
                                />
                            </div>
                        </div>

                    </div>
                    <div class="divider">
                        <hr>
                    </div>
                    <div class="dialog-footer">
                        <div>
                            <BaseButton
                                buttonName="Hủy"
                                secondaryButton
                                @click="() => onClose(false)"
                            />
                        </div>
                        <div class="dialog-footer-right">
                            <div class="btn-first">
                                <BaseButton
                                    buttonName="Cất"
                                    secondaryButton
                                    @click="() => onSave()"
                                />
                            </div>
                            <BaseButton
                                buttonName="Cất và thêm"
                                @click="() => onSave(true)"
                            />
                        </div>
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

    </div>
</template>

<script>
import EmployeeApi from "../../../api/service/employee";
import { mapActions, mapMutations } from "vuex";
import resources from "../../../resources";
import enums from "../../../enums";
import utils from "../../../utils";
import Contact from "./Contact.vue";
import Terms from "./Terms.vue";
import BankAccount from "./BankAccount.vue";
import Address from "./Address.vue";

export default {
    name: "ProviderDetails",
    components: { Contact, Terms, BankAccount, Address },
    props: {
        employee: Object,
        departments: Array,
        state: Number
    },

    data: function () {
        return {
            format: enums.format,
            currentDepartments: this.departments,
            currentEmployee: {
                ...this.employee,
                DateOfBirth: utils.formatDateValueInput(this.employee?.DateOfBirth),
                Gender: this.employee?.Gender || 0,
                IdentityDate: utils.formatDateValueInput(this.employee?.IdentityDate)
            },

            genders: [
                { value: 0, text: "Name" },
                { value: 1, text: "Nữ" },
                { value: 2, text: "Khác" }
            ],
            disabledButton: true,
            // kiểm tra xem người dùng đã sửa hay thao tác gì với form chưa
            edited: false,
            // loading
            loading: false,

            currentState: this.state,

            tab: "contact"

        };
    },

    async mounted() {
        if (this.currentState === enums.dialogState.post) {
            await this.setNewEmployeeCode();
            this.$refs["1"].$refs.BaseInput.focus();
            this.edited = false;
        } else {
            this.$refs["1"].$refs.BaseInput.focus();
        }
    },

    watch: {
        currentEmployee: {
            handler(value) {
                this.edited = true;
            },
            deep: true
        }
    },

    computed: {
        /**
         * Set giá trị mặc định cho đơn vị
         * Created by: VLVU (18/8/2021)
         */
        defaultDepartment() {
            if (!this.currentEmployee?.DepartmentId) {
                return null;
            }
            return this.currentDepartments.find(item => item.value === this.currentEmployee.DepartmentId);
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
         * Lấy tên của tab hiện tại
         */
        getTab(name) {
            this.tab = name;
        },
        /**
         * Set 1 mã nhân viên mới lấy từ server về
         * Created by: VLVU (18/8/2021)
         */
        async setNewEmployeeCode() {
            try {
                const promise = await EmployeeApi.getEmployeeCode();
                this.currentEmployee = { ...this.currentEmployee, EmployeeCode: promise.data };
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * Lấy dữ liệu về giới tính
         * Created by: VLVU (15/8/2021)
         */
        getGender(e) {
            this.currentEmployee = { ...this.currentEmployee, Gender: Number(e.target.value) };
        },
        /**
         * handle ấn vào đóng dialog
         * @param {boolean} confirm có hiện xác nhận popup hay không
         * Created by: VLVU (2021)
         */
        async onClose(confirm = true) {
            if (this.edited && confirm) {
                const ok = await this.confirmPopup(resources.popup.confirmCloseEmployeeDetailForm);
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
         * Lấy dữ liệu phòng ban
         * Created by: VLVU (2021)
         */
        getDepartment(result) {
            this.currentEmployee = { ...this.currentEmployee, DepartmentId: result.value };
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
            const validate = await this.validate();
            if (!validate) {
                return;
            }
            let promise = null;
            try {
                this.loading = true;
                if (this.currentState === enums.dialogState.post) {
                    promise = await EmployeeApi.insertOne(this.currentEmployee);
                } else {
                    promise = await EmployeeApi.updateOne(this.currentEmployee.EmployeeId, this.currentEmployee);
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
                    this.setToast(resources.toast.addEmployeeSuccess(this.currentEmployee.EmployeeCode));
                } else {
                    this.setToast(resources.toast.updateEmployeeSuccess(this.currentEmployee.EmployeeCode));
                }
                this.currentEmployee = {};
                if (keepCreating) {
                    await this.setNewEmployeeCode();
                    this.$refs["1"].$refs.BaseInput.focus();
                    this.loading = false;
                    this.edited = false;
                    // this.defaultDepartment = null;
                    // set lại currentState là post để nó có thể thêm mới
                    this.currentState = enums.dialogState.post;
                    this.$emit("reloadEmployees");
                    return;
                }
                this.$emit("onClose", { hasReloadEmployees: true });
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
