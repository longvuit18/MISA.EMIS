<template>
    <div id="navbar">
        <div class="logo-site">
            <div
                :class="!isToggle ? 'toggle' : 'toggle-2'"
                @click="clickToggle"
            ></div>
            <img
                src="../../assets/icon/logo.svg"
                height="32"
                v-show="!isToggle"
            />
        </div>
        <div class="nav-content">

            <router-link
                v-for="route in routesDeveloping"
                :key="route.key"
                class="nav-item"
                :to="'/' + route.key"
                active-class="active"
            >
                <div class="nav-icon">
                    <div :class="'icon icon-size ' + route.key"></div>
                </div>
                <div
                    class="nav-text"
                    v-show="!isToggle"
                >{{route.text}}</div>
            </router-link>
        </div>
    </div>
</template>

<script>
export default {
    name: "TheNavbar",
    data() {
        return {
            routesDeveloping: [{ key: "cash", text: "Tiền mặt" }]
        };
    },
    props: {
        isToggle: {
            type: Boolean,
            default: () => false
        }
    },

    methods: {
        clickToggle() {
            if (!this.isToggle) {
                return;
            }
            this.$emit("clickToggle");
        }
    }
};
</script>

<style scoped>
#navbar {
    height: 100vh;
    background-color: #38393d;
    /* width: 220px; */
    border-right: 1px solid #e5e5e5;
    box-sizing: border-box;
    overflow: auto;
    /* transition: all 0.4s; */
}

#navbar .logo-site {
    height: 48px;
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
}

#navbar .toggle {
    width: 24px;
    height: 24px;
    margin-right: 10px;
    background: url("../../assets/icon/icon.svg") no-repeat -424px -86px;
}
#navbar .toggle-2 {
    background: url("../../assets/icon/icon.svg") no-repeat -312px -32px;
    width: 24px;
    height: 24px;
    margin: 0 10px;
    cursor: pointer;
}

#navbar .logo {
    background: url("../../assets/icon/logo.svg") no-repeat;
    height: 32px;
    width: 70px;
}

.nav-content {
    width: 100%;
    margin-top: 14px;
}

#navbar .nav-item {
    display: flex;
    height: 42px;
    line-height: 42px;
    padding-left: 14px;
    text-decoration: none;
    cursor: pointer;
    position: relative;
}

#navbar .nav-item:hover:before {
    content: "";
    background-color: #ebedf0;
    color: #fff;
    width: calc(100% + 6px);
    cursor: pointer;
    position: absolute;
    left: -6px;
    height: 42px;
    top: 0;
    opacity: 0.2;
}

#navbar .nav-item .nav-icon {
    display: flex;
    align-items: center;
    margin-right: 8px;
}
#navbar .active {
    position: relative;
    font-weight: 700;
    border-left: 6px solid #2ca01c;
}

#navbar .active:before {
    content: "";
    background-color: #ebedf0;
    color: #fff;
    width: calc(100% + 6px);
    cursor: pointer;
    position: absolute;
    left: -6px;
    height: 42px;
    top: 0;
    opacity: 0.2;
}

#navbar a {
    color: #fff;
}

/* icon */
.icon {
    background: url("../../assets/icon/icon.svg") no-repeat;
}

.icon-size {
    width: 24px;
    height: 24px;
    min-width: 24px;
    min-height: 24px;
}

.dashboard {
    background-position: -30px -1628px;
}

.cash {
    background-position: -73px -1628px;
}
.bank {
    background-position: -117px -1628px;
}
.bag {
    background-position: -161px -1628px;
}
.cart {
    background-position: -204px -1628px;
}
.invoice {
    background-position: -249px -1628px;
}
.stock {
    background-position: -291px -1628px;
}
.tools {
    background-position: -336px -1628px;
}
.fixed-assets {
    background-position: -378px -1630px;
}
.tax {
    background-position: -423px -1630px;
}
.general {
    background-position: -510px -1628px;
}
.budget {
    background-position: -378px -1659px;
}
.price {
    background-position: -467px -1628px;
}
.report {
    background-position: -547px -1628px;
}
.finance {
    background-position: -204px -1664px;
}

.user {
    background-position: -120px -1664px;
}
</style>
