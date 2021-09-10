<template>
    <div>
        <BaseDialog isOpen>
            <div class="e-box">
                <div class="dialog-header">
                    <div class="header-left">
                        <div class="title">Thông tin nhân viên</div>
                        <div
                            style="padding:0 20px;"
                            v-tooltip="'Tính năng đang phát triển'"
                        >
                            <BaseCheckbox label="Là khách hàng" />
                        </div>
                        <div
                            style="padding:0 20px;"
                            v-tooltip="'Tính năng đang phát triển'"
                        >
                            <BaseCheckbox label="Là nhà cung cấp" />
                        </div>
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
                                        label="Mã nhân viên"
                                        required
                                        tabindex="1"
                                        v-model="currentEmployee.EmployeeCode"
                                        ref="1"
                                        name="Mã nhân viên"
                                    />
                                </BaseCol>
                                <BaseCol
                                    :padding="0"
                                    :cols="7"
                                >
                                    <BaseInput
                                        fullWidth
                                        label="Tên"
                                        required
                                        tabindex="2"
                                        v-model="currentEmployee.EmployeeName"
                                        ref="2"
                                        name="Họ và tên"
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
                                        label="Đơn vị"
                                        required
                                        tabindex="3"
                                        ref="3"
                                        name="Đơn vị"
                                        :items="currentDepartments"
                                        :defaultItem="defaultDepartment"
                                        v-model="currentEmployee.DepartmentId"
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
                                        label="Chức danh"
                                        tabindex="4"
                                        v-model="currentEmployee.EmployeePosition"
                                        name="Chức danh"
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
                                    <BaseDatePicker
                                        fullWidth
                                        label="Ngày sinh"
                                        tabindex="5"
                                        v-model="currentEmployee.DateOfBirth"
                                        format="DD/MM/YYYY"
                                        value-type="YYYY-MM-DD"
                                        name="Ngày sinh"
                                    />
                                </BaseCol>
                                <!-- Giới tính la radio -->
                                <BaseCol
                                    :padding="0"
                                    :cols="7"
                                >
                                    <div class="gender-form">
                                        <span>Giới tính</span>
                                        <div class="radio-group">
                                            <BaseRadio
                                                label="Nam"
                                                :value="0"
                                                @change="getGender"
                                                :checked="!currentEmployee.Gender || currentEmployee.Gender === 0"
                                                style="margin-right: 20px"
                                                tabindex="6"
                                            />
                                            <BaseRadio
                                                label="Nữ"
                                                :value="1"
                                                @change="getGender"
                                                :checked="currentEmployee.Gender === 1"
                                                style="margin-right: 20px"
                                                tabindex="7"
                                            />
                                            <BaseRadio
                                                label="Khác"
                                                :value="2"
                                                @change="getGender"
                                                :checked="currentEmployee.Gender === 2"
                                                tabindex="8"
                                            />
                                        </div>
                                    </div>
                                </BaseCol>
                            </BaseRow>
                            <BaseRow>
                                <BaseCol
                                    :padding="0"
                                    :cols="7"
                                    style="padding-right: 6px"
                                >
                                    <BaseInput
                                        fullWidth
                                        label="Số CMND"
                                        tabindex="9"
                                        v-model="currentEmployee.IdentityNumber"
                                        name="Số chưng minh thư"
                                    />
                                </BaseCol>
                                <BaseCol
                                    :padding="0"
                                    :cols="5"
                                >
                                    <BaseDatePicker
                                        label="Ngày Cấp"
                                        tabindex="10"
                                        v-model="currentEmployee.IdentityDate"
                                        name="Ngày cấp"
                                        format="DD/MM/YYYY"
                                        value-type="YYYY-MM-DD"
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
                                        label="Nơi cấp"
                                        tabindex="11"
                                        v-model="currentEmployee.IdentityPlace"
                                        name="Nơi cấp"
                                    />
                                </BaseCol>
                            </BaseRow>
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
                                tabindex="12"
                                v-model="currentEmployee.Address"
                                name="Địa chỉ"
                            />
                        </BaseCol>
                    </BaseRow>
                    <BaseRow>
                        <BaseCol
                            :padding="0"
                            :cols="3"
                            style="padding-right: 6px"
                        >
                            <BaseInput
                                fullWidth
                                label="ĐT di động"
                                tabindex="13"
                                v-model="currentEmployee.PhoneNumber"
                                name="Điện thoại di động"
                            />
                        </BaseCol>
                        <BaseCol
                            :padding="0"
                            :cols="3"
                            style="padding-right: 6px"
                        >
                            <BaseInput
                                fullWidth
                                label="ĐT cố định"
                                tabindex="14"
                                v-model="currentEmployee.TelephoneNumber"
                                name="Điện thoại cố định"
                            />
                        </BaseCol>
                        <BaseCol
                            :padding="0"
                            :cols="3"
                            style="padding-right: 6px"
                        >
                            <BaseInput
                                fullWidth
                                label="Email"
                                tabindex="15"
                                v-model="currentEmployee.Email"
                                name="Email"
                            />
                        </BaseCol>
                    </BaseRow>
                    <BaseRow>
                        <BaseCol
                            :padding="0"
                            :cols="3"
                            style="padding-right: 6px"
                        >
                            <BaseInput
                                fullWidth
                                label="Tài khoản ngân hàng"
                                tabindex="16"
                                v-model="currentEmployee.BankAccountNumber"
                                name="Tài khoản ngân hàng"
                            />
                        </BaseCol>
                        <BaseCol
                            :padding="0"
                            :cols="3"
                            style="padding-right: 6px"
                        >
                            <BaseInput
                                fullWidth
                                label="Tên ngân hàng"
                                tabindex="17"
                                v-model="currentEmployee.BankName"
                                name="Tên ngân hàng"
                            />
                        </BaseCol>
                        <BaseCol
                            :padding="0"
                            :cols="3"
                            style="padding-right: 6px"
                        >
                            <BaseInput
                                fullWidth
                                label="Chi nhánh"
                                tabindex="18"
                                v-model="currentEmployee.BankBranchName"
                                name="Tên chi nhánh"
                            />
                        </BaseCol>
                    </BaseRow>
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

export default {
    name: "TheEmployeeDetails",
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
            disabledButton: true,
            // kiểm tra xem người dùng đã sửa hay thao tác gì với form chưa
            edited: false,
            // loading
            loading: false,

            currentState: this.state

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
