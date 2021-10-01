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
                            v-model.number="currentProvider.provider_type"
                            horizontal
                            :disabled="viewMode"
                        />
                        <div style="margin-left: 100px;">
                            <BaseCheckbox
                                label='Là khách hàng'
                                v-model="currentProvider.is_customer"
                                :disabled="viewMode"
                            />
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

                <div id="provider-details">
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
                                    style="padding-right: 12px"
                                    v-if="currentProvider.provider_type === 0"
                                >
                                    <BaseInput
                                        fullWidth
                                        label="Mã số thuế"
                                        tabindex="1"
                                        name="Mã số thuế"
                                        v-model="currentProvider.tax_code"
                                        :disabled="viewMode"
                                        ref="1"
                                    />
                                </BaseCol>
                                <BaseCol
                                    :padding="0"
                                    :cols="7"
                                    v-if="currentProvider.provider_type === 0"
                                >
                                    <BaseInput
                                        fullWidth
                                        label="Mã nhà cung cấp"
                                        required
                                        tabindex="2"
                                        ref="2"
                                        name="Mã nhà cung cấp"
                                        v-model="currentProvider.provider_code"
                                        :disabled="viewMode"
                                    />
                                </BaseCol>
                            </BaseRow>
                            <BaseRow>
                                <BaseCol
                                    :padding="0"
                                    :cols="7"
                                    style="padding-right: 12px"
                                    v-if="currentProvider.provider_type === 1"
                                >
                                    <BaseInput
                                        fullWidth
                                        label="Mã nhà cung cấp"
                                        required
                                        tabindex="1"
                                        ref="1"
                                        name="Mã nhà cung cấp"
                                        v-model="currentProvider.provider_code"
                                        focusInput
                                        :disabled="viewMode"
                                    />
                                </BaseCol>
                                <BaseCol
                                    :padding="0"
                                    :cols="5"
                                    v-if="currentProvider.provider_type === 1"
                                >
                                    <BaseInput
                                        fullWidth
                                        label="Mã số thuế"
                                        tabindex="2"
                                        ref="2"
                                        name="Mã số thuế"
                                        v-model="currentProvider.tax_code"
                                        :disabled="viewMode"
                                    />
                                </BaseCol>
                            </BaseRow>
                            <BaseRow>
                                <BaseCol
                                    :padding="0"
                                    :cols="12"
                                    v-if="currentProvider.provider_type === 0"
                                >
                                    <BaseInput
                                        fullWidth
                                        label="Tên nhà cung cấp"
                                        required
                                        tabindex="3"
                                        ref="3"
                                        name="Tên nhà cung cấp"
                                        v-model="currentProvider.provider_name"
                                        :disabled="viewMode"
                                    />
                                </BaseCol>
                            </BaseRow>
                            <BaseRow>
                                <BaseCol
                                    :padding="0"
                                    :cols="4"
                                    v-if="currentProvider.provider_type === 1"
                                    style="margin-right:12px"
                                >
                                    <BaseCombobox
                                        fullWidth
                                        label="Xưng hô"
                                        tabindex="3"
                                        name="Xưng hô"
                                        v-model="currentProvider.prefix"
                                        :items="prefixItems"
                                        :disabled="viewMode"
                                    />
                                </BaseCol>
                                <BaseCol
                                    :padding="0"
                                    :cols="8"
                                    v-if="currentProvider.provider_type === 1"
                                >
                                    <BaseInput
                                        fullWidth
                                        label="Tên nhà cung cấp"
                                        required
                                        tabindex="4"
                                        ref="3"
                                        name="Tên nhà cung cấp"
                                        v-model="currentProvider.provider_name"
                                        :disabled="viewMode"
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
                                        v-model="currentProvider.address"
                                        :rows="3"
                                        :disabled="viewMode"
                                    />
                                </BaseCol>
                            </BaseRow>
                        </BaseCol>
                        <BaseCol
                            :padding="0"
                            :cols="6"
                            style="padding-left: 13px"
                        >
                            <BaseRow v-if="currentProvider.provider_type === 0">
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
                                        v-model="currentProvider.phone_number"
                                        :disabled="viewMode"
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
                                        v-model="currentProvider.website"
                                        :disabled="viewMode"
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
                                        ref="4"
                                        name="Nhóm nhà cung cấp"
                                        placeholder=""
                                        :items="groupProviderData"
                                        v-model="groupProvider"
                                        :columnNames="columnNamesProriverGroup"
                                        optionId="provider_group_code"
                                        keyLabel="provider_group_name"
                                        hasAddIcon
                                        optionsTable
                                        multiple
                                        :disabled="viewMode"
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
                                        ref="5"
                                        name="Nhân viên mua hàng"
                                        hasAddIcon
                                        placeholder=""
                                        :columnNames="columnNamesEmployee"
                                        :items="employees"
                                        optionsTable
                                        optionId="employee_id"
                                        keyLabel="employee_code"
                                        v-model="currentProvider.employee_id"
                                        :disabled="viewMode"
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
                            <transition name="slide">
                                <Contact
                                    class="wrap"
                                    v-show="tab === 'contact'"
                                    :providerType="currentProvider.provider_type"
                                    :data="currentProvider"
                                    :isClicked="hasClickTab"
                                    :disabled="viewMode"
                                />
                            </transition>
                            <transition name="slide">
                                <Terms
                                    class="wrap"
                                    v-show="tab === 'terms'"
                                    :isCustomer="currentProvider.is_customer"
                                    :data="currentProvider"
                                    :disabled="viewMode"
                                />
                            </transition>
                            <transition name="slide">
                                <BankAccount
                                    class="wrap"
                                    v-show="tab === 'bankAccount'"
                                    v-model="currentProvider.bank_account_group"
                                    :disabled="viewMode"
                                />
                            </transition>
                            <transition name="slide">
                                <Address
                                    class="wrap"
                                    v-show="tab === 'address'"
                                    :disabled="viewMode"
                                    :data="currentProvider"
                                    :countries="countries"
                                />
                            </transition>
                            <transition name="slide">
                                <div
                                    class="note wrap"
                                    v-show="tab === 'note'"
                                >
                                    <BaseInput
                                        isTextarea
                                        rows="9"
                                        fullWidth
                                        v-model="currentProvider.description"
                                        focusInput
                                        tabindex="9"
                                        :disabled="viewMode"
                                    />
                                </div>
                            </transition>
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
import ProviderApi from "../../../api/service/provider";
import EmployeeApi from "../../../api/service/employee";
import { mapActions, mapMutations } from "vuex";
import resources from "../../../resources";
import enums from "../../../enums";
import utils from "../../../utils";
import Contact from "./Contact.vue";
import Terms from "./Terms.vue";
import BankAccount from "./BankAccount.vue";
import Address from "./Address.vue";

const columnNamesProriverGroup = [
    { key: "provider_group_code", text: "Mã nhóm KH, NCC", width: 170 },
    { key: "provider_group_name", text: "Tên nhóm KH, NCC", width: 190 }
];

const prefixItems = [
    { key: "Anh", text: "Anh" },
    { key: "Chị", text: "Chị" },
    { key: "Ông", text: "Ông" },
    { key: "Bà", text: "Bà" },
    { key: "Miss", text: "Miss" },
    { key: "Mr", text: "Mr" },
    { key: "Mrs", text: "Mrs" }
];

const columnNamesEmployee = [
    { key: "employee_code", text: "Mã nhân viên", width: 180 },
    { key: "employee_name", text: "Tên nhân viên", width: 210 }

];

const nameCountryDefault = "Viet Nam"; // khóa chính của VN trong db
export default {
    name: "ProviderDetails",
    components: { Contact, Terms, BankAccount, Address },
    props: {
        provider: Object,
        state: Number,
        countries: {
            type: Array,
            default: () => []
        }
    },

    data: function () {
        return {
            currentProvider: {
                ...this.provider,
                country: this.provider?.country ?? nameCountryDefault, // id của việt nam trong db
                provider_type: this.provider?.provider_type ?? 0,
                personal_contact_identity_date: utils.formatDateValueInput(this.provider?.personal_contact_identity_date)
            },
            disabledButton: true,
            // kiểm tra xem người dùng đã sửa hay thao tác gì với form chưa
            edited: false,
            // loading
            loading: false,

            currentState: this.state,
            viewMode: this.state === enums.dialogState.view,

            tab: "contact",
            hasClickTab: false,

            columnNamesProriverGroup,
            groupProviderData: [
                { provider_group_code: "NCC-1", provider_group_name: "Vũ Long Vũ 1" },
                { provider_group_code: "NCC-2", provider_group_name: "Vũ Long Vũ 2" },
                { provider_group_code: "NCC-3", provider_group_name: "Vũ Long Vũ 3" },
                { provider_group_code: "NCC-4", provider_group_name: "Vũ Long Vũ 4" },
                { provider_group_code: "NCC-5", provider_group_name: "Vũ Long Vũ 5" },
                { provider_group_code: "NCC-6", provider_group_name: "Vũ Long Vũ 6" },
                { provider_group_code: "NCC-7", provider_group_name: "Vũ Long Vũ 7" }
            ],
            groupProvider: this.provider?.provider_group?.split(";") || [],

            prefixItems,
            columnNamesEmployee,
            employees: []

        };
    },

    async mounted() {
        try {
            this.loading = true;
            if (this.currentState === enums.dialogState.post) {
                await this.setNewProviderCode();
                this.$refs["1"].$refs.BaseInput.focus();
                this.edited = false;
            } else {
                this.$refs["1"].$refs.BaseInput.focus();
            }
            await this.getAllEmployees();
            this.loading = false;
        } catch (error) {
            console.error(error);
            this.loading = false;
        }
    },

    watch: {
        currentProvider: {
            handler(value) {
                this.edited = true;
            },
            deep: true
        },

        "currentProvider.provider_type": {
            handler() {
                this.hasClickTab = false;
            }
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
            this.hasClickTab = true;
        },
        /**
         * Set 1 mã nhà cung cấp mới lấy từ server về
         * Created by: VLVU (24/9/2021)
         */
        async setNewProviderCode() {
            try {
                const promise = await ProviderApi.getProviderCode();
                this.currentProvider = { ...this.currentProvider, provider_code: promise.data ?? "" };
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * lấy toàn bộ nhân viên
         * Created by: VLVU (24/9/2021)
         */
        async getAllEmployees() {
            try {
                const promise = await EmployeeApi.getAll();
                this.employees = promise.data?.Data ?? [];
            } catch (error) {
                console.log(error);
            }
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
            // trạng thái xem không xảy ra gì cả
            if (this.viewMode) {
                return;
            }
            const validate = await this.validate();
            if (!validate) {
                return;
            }
            let promise = null;
            try {
                this.loading = true;
                if (this.currentState === enums.dialogState.post) {
                    promise = await ProviderApi.insertOne({ ...this.currentProvider, provider_group: this.groupProvider.join(";") });
                } else {
                    promise = await ProviderApi.updateOne(this.currentProvider.provider_id, { ...this.currentProvider, provider_group: this.groupProvider.join(";") });
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
                    this.setToast(resources.toast.addSuccess(this.currentProvider.provider_code, "nhà cung cấp"));
                } else {
                    this.setToast(resources.toast.updateSuccess(this.currentProvider.provider_code, "nhà cung cấp"));
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
                    this.$emit("reloadProviders");
                    return;
                }
                this.$emit("onClose", { hasReloadProviders: true });
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
