<template>
    <div id="process">
        <div class="content-area">
            <div class="process-and-utilies-area">
                <div class="process-area">
                    <div class="header-text">
                        Nghiệp vụ tiền mặt
                    </div>
                    <div class="main">
                        <div class="button receive">
                            Thu tiền
                        </div>
                        <div class="button payment">
                            Chi tiền
                        </div>
                        <div class="button audit">
                            Kiểm kê quỹ
                        </div>
                    </div>
                </div>
                <div class="utilies-area">
                    <BaseRow>
                        <BaseCol
                            :cols="3"
                            :padding="0"
                        >
                            <div class="content">
                                <div class="icon icon-size-24 mi-process-customer"></div>
                                <div class="text">Khách hàng</div>
                            </div>
                        </BaseCol>
                        <BaseCol
                            :cols="3"
                            :padding="0"
                        >
                            <div
                                class="content"
                                @click="toProviderPage"
                            >
                                <div class="icon icon-size-24 mi-process-provider"></div>
                                <div class="text">Nhà cung cấp</div>
                            </div>
                        </BaseCol>
                        <BaseCol
                            :cols="3"
                            :padding="0"
                        >
                            <div
                                class="content"
                                @click="toEmployeePage"
                            >
                                <div class="icon icon-size-24 mi-process-employee"></div>
                                <div class="text">Nhân viên</div>
                            </div>
                        </BaseCol>
                        <BaseCol
                            :cols="3"
                            :padding="0"
                        >
                            <div
                                class="content no-border"
                                @click="toAccountSystemPage"
                            >
                                <div class="icon icon-size-24 mi-calculate"></div>
                                <div class="text">Hệ thống tài khoản</div>
                            </div>
                        </BaseCol>
                    </BaseRow>
                </div>
            </div>
            <div class="report-area">
                <div class="header-text">
                    Báo cáo
                </div>
                <div class="main">
                    <ul>
                        <li>Dòng tiền</li>
                        <li>Bảng kê số dư tiền trong ngày</li>
                        <li>Sổ kế toán chi tiết quỹ theo tiền mặt</li>
                        <li>S03a2-DN: Sổ nhật ký chi tiền</li>
                        <li>S03a1-DN: Sổ nhật ký thu tiền</li>
                    </ul>
                    <div class="footer">Tất cả báo cáo</div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "Process",

    methods: {
        toEmployeePage() {
            this.$router.push("/categories/employee");
        },
        toProviderPage() {
            this.$router.push("/categories/provider");
        },
        toAccountSystemPage() {
            this.$router.push("/categories/account");
        }
    }
};
</script>

<style scoped>
#process {
    height: calc(100% - 64px);
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
}

.content-area {
    display: flex;
}

.process-area {
    background-color: #fff;
    border: 1px solid #e5e5e5;
    border-radius: 3px;
    width: 690px;
    height: 380px;
}

.report-area {
    background-color: #fff;
    border: 1px solid #e5e5e5;
    border-radius: 3px;
    margin-left: 10px;
    width: 300px;
    height: 460px;
    display: flex;
    flex-direction: column;
    position: relative;
}

.process-area .header-text {
    text-transform: uppercase;
    height: 40px;
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 14px;
    font-weight: 700;
    border-bottom: 1px solid #e5e5e5;
}

.report-area .header-text {
    text-transform: uppercase;
    height: 40px;
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 14px;
    font-weight: 700;
    border-bottom: 1px solid #e5e5e5;
}

.process-area .main {
    height: 340px;
    position: relative;
    background-image: url("../../../assets/img/qt_TienMat.svg");
    background-repeat: no-repeat;
}

.process-area .main .button {
    width: 90px;
    padding-top: 60px;
    padding-bottom: 10px;
    position: absolute;
    transition: all 0.2s ease;
    text-align: center;
    cursor: pointer;
}

.process-area .main .button:hover {
    box-shadow: 0 3px 6px rgb(0 0 0 / 16%);
}

.process-area .main .receive {
    top: 40px;
    left: 90px;
}

.process-area .main .payment {
    top: 215px;
    left: 90px;
}
.process-area .main .audit {
    top: 125px;
    left: 335px;
    width: 125px !important;
}
.report-area .main ul {
    padding: 0 16px;
}

.report-area .main ul li {
    display: flex;
    align-items: center;
    padding: 16px 0;
    border-bottom: 1px solid #e5e5e5;
    height: 74px;
    cursor: pointer;
}

.report-area .main ul li:hover {
    color: #0075c0;
}

.report-area .main ul li:before {
    content: "\2022";
    color: #646464;
    font-size: 24px;
    margin-right: 12px;
}

.report-area .footer {
    border-top: 1px solid #e5e5e5;
    cursor: pointer;
    height: 44px;
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 13px;
    color: #0075c0;
}
.utilies-area {
    background-color: #fff;
    border: 1px solid #e5e5e5;
    border-radius: 3px;
    margin-top: 10px;
    height: 70px;
}

.utilies-area .content {
    display: flex;
    height: 70px;
    border-right: 1px solid #e5e5e5;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    cursor: pointer;
}

.utilies-area .content:hover {
    box-shadow: 0 3px 6px rgb(0 0 0 / 16%);
}
</style>
