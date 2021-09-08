<template>
    <div id="cash">
        <div class="nav">
            <router-link
                class="nav-item"
                to="/cash/process"
                active-class="active"
            >Quy trình</router-link>
            <router-link
                class="nav-item"
                to="/cash/receipt-payment-list"
                active-class="active"
            >Thu, chi tiền</router-link>
        </div>
        <router-view />
    </div>
</template>

<script>
export default {
    name: "Cash"
};
</script>

<style scoped>
#cash {
    position: relative;
    height: calc(100vh - 48px);
    transition: all 0.4s;
}
#cash .nav {
    display: flex;
    border-spacing: 0;
    border-bottom: 2px solid #d4d7dc;
    padding-top: 12px;
}

#cash .nav .active {
    border-bottom: 4px solid #2ca01c;
    font-weight: bold;
}
#cash .nav .nav-item {
    padding: 14px 20px;
    font-size: 13px;
    cursor: pointer;
    position: relative;
    top: 2px;
    text-decoration: none;
    color: inherit;
}
#cash .nav .nav-item:hover {
    color: #2ca01c;
}
</style>
