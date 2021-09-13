<template>
    <div id="app">
        <TheNavbar
            :style="'width:' + widthNavbar + 'px'"
            :isToggle="isToggle"
            @clickToggle="clickToggle"
        />
        <div
            id="content"
            :style="width"
        >
            <TheHeader
                @clickToggle="clickToggle"
                :isToggle="isToggle"
            />
            <div class="route-view">
                <router-view />
            </div>
        </div>
        <ToastMessage :newToast="$store.state.toastMessage.toast" />
        <BasePopup ref="popup" />
        <portal-target
            name="combobox"
            multiple
        >
        </portal-target>
        <portal-target
            name="dropdown-button"
            multiple
        >
        </portal-target>
    </div>

</template>

<script>
import TheHeader from "./components/layout/TheHeader.vue";
import TheNavbar from "./components/layout/TheNavbar.vue";

const widthNavbar = 180; // giá trị width ban đầu của navbar
export default {
    components: {
        TheHeader,
        TheNavbar
    },

    data() {
        return {
            widthNavbar: widthNavbar,
            isToggle: false
        };
    },

    mounted() {
        // truyền ref của Basepopup vào store
        this.$store.dispatch("popup/setPopupRef", this.$refs.popup);
    },

    computed: {
        width() {
            return `width: calc(100% - ${this.widthNavbar + 1}px)`; // 1px là của border
        }
    },

    methods: {
        clickToggle() {
            if (this.widthNavbar === widthNavbar) {
                this.widthNavbar = 52;

                this.isToggle = true;
                return;
            }
            this.widthNavbar = widthNavbar;

            this.isToggle = false;
        }
    }

};
</script>
<style lang="css" src="./style.css"></style>
<style lang="css">
#app {
    display: flex;
}
.route-view {
    padding-left: 20px;
}
</style>
