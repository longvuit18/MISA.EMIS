<template>
    <div id="header">
        <div class="header-left">
            <div
                class="toggle"
                @click="clickToggle"
                v-if="!isToggle"
            >
            </div>
            <div class="select-company">
                <select
                    name="company"
                    class="short-content"
                    v-tooltip.right-start="'Tính năng chưa phát triển'"
                >
                    <option
                        value="1"
                        class="short-content"
                    >
                        CÔNG TY CỔ PHẦN MISA
                    </option>
                </select>
            </div>
        </div>
        <div class="account">
            <div class="notification"></div>
            <div class="account-avatar"></div>
            <div class="account-name">Vũ Long Vũ</div>
            <div
                class="account-more"
                v-tooltip="'Tính năng chưa phát triển'"
            ></div>
        </div>

    </div>
</template>

<script>
export default {
    props: {
        isToggle: {
            type: Boolean,
            default: () => false
        }
    },
    name: "TheHeader",
    methods: {
        clickToggle() {
            this.$emit("clickToggle");
        }
    }
};
</script>

<style scoped>
#header {
    width: 100%;
    height: 48px;
    background-color: #fff;
    display: flex;

    box-sizing: border-box;
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.header-left {
    display: flex;
    align-items: center;
}
.header-left .toggle {
    background: url("../../assets/icon/icon.svg") no-repeat -843px -30px;
    width: 24px;
    height: 24px;
    margin: 0 10px;
    cursor: pointer;
}
.header-left .select-company {
    margin-left: 16px;
}

.account {
    margin-right: 30px;
    display: flex;
    align-items: center;
}

.account-avatar {
    width: 30px;
    height: 30px;
    border-radius: 50%;
    background-image: url("../../assets/img/default-avatar.jpg");
    background-size: contain;
}
.account-name {
    font-weight: 600;
    font-size: 14px;
    margin: 0px 6px 0px 6px;
}
.account-more {
    background: url("../../assets/icon/icon.svg") no-repeat;
    cursor: pointer;
    background-position: -1076px -32px;
    width: 14px;
    height: 14px;
}
.notification {
    background: url("../../assets/icon/icon.svg") no-repeat;
    width: 24px;
    height: 24px;
    background-position: -788px -30px;
    margin-right: 20px;
}

select {
    border: none;
    outline: none;
    font-size: 14px;
    font-weight: 600;
    font-family: "Notosans";
}

.short-content {
    width: 200px;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
}
</style>
