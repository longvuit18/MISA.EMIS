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
                            v-tooltip="'Đóng'"
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
                                fullWidth
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
                                fullWidth
                            />
                        </BaseCol>
                        <BaseCol :cols="6">
                            <BaseInput
                                label="Tên tiếng anh"
                                name="Tên tiếng anh"
                                tabindex="3"
                                fullWidth
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
                                fullWidth
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
                            />
                        </BaseCol>
                    </BaseRow>
                    <BaseRow>
                        <BaseCol>
                            <BaseCheckbox
                                label="Có hoạch toán ngoại lệ"
                                tabindex="7"
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
                                                />
                                            </BaseCol>
                                            <BaseCol :cols="6">
                                                <BaseCombobox
                                                    tabindex="9"
                                                    fullWidth
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
                                                />
                                            </BaseCol>
                                            <BaseCol :cols="6">
                                                <BaseCombobox
                                                    tabindex="9"
                                                    fullWidth
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
                                                />
                                            </BaseCol>
                                            <BaseCol :cols="6">
                                                <BaseCombobox
                                                    tabindex="9"
                                                    fullWidth
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
                                                />
                                            </BaseCol>
                                            <BaseCol :cols="6">
                                                <BaseCombobox
                                                    tabindex="9"
                                                    fullWidth
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
                                                />
                                            </BaseCol>
                                            <BaseCol :cols="6">
                                                <BaseCombobox
                                                    tabindex="9"
                                                    fullWidth
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
                                                />
                                            </BaseCol>
                                            <BaseCol :cols="6">
                                                <BaseCombobox
                                                    tabindex="9"
                                                    fullWidth
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
                                                />
                                            </BaseCol>
                                            <BaseCol :cols="6">
                                                <BaseCombobox
                                                    tabindex="9"
                                                    fullWidth
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
                                                />
                                            </BaseCol>
                                            <BaseCol :cols="6">
                                                <BaseCombobox
                                                    tabindex="9"
                                                    fullWidth
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
                                                />
                                            </BaseCol>
                                            <BaseCol :cols="6">
                                                <BaseCombobox
                                                    tabindex="9"
                                                    fullWidth
                                                />
                                            </BaseCol>
                                        </BaseRow>
                                    </BaseCol>
                                </BaseRow>
                            </BaseCol>
                        </BaseRow>
                    </div>
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
                            />
                        </div>
                        <BaseButton
                            buttonName="Cất và thêm"
                            @click="() => onSave(true)"
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

            currentState: this.state,

            showMonitorDetails: true,
            fullWidth: false

        };
    },

    async mounted() {
        if (this.currentState === enums.dialogState.post) {
            await this.setNewEmployeeCode();
            // this.$refs["1"].$refs.BaseInput.focus();
            this.edited = false;
        } else {
            // this.$refs["1"].$refs.BaseInput.focus();
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
        },

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
