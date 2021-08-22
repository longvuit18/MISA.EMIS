import Vue from "vue";
import VueRouter from "vue-router";
import Dashboard from "../views/Dashboard.vue";
import Employee from "../views/Employee";

Vue.use(VueRouter);

const routes = [
	{
		path: "/employee",
		name: "Employee",
		component: Employee,
		meta: {
			title: "Nhân viên | Danh mục | Công ty Misa"
		}
	},
	{
		path: "*",
		name: "Dashboard",
		component: Dashboard
	}
];

const router = new VueRouter({
	mode: "history",
	base: process.env.BASE_URL,
	routes
});
router.beforeEach((to, _from, next) => {
	if (to.meta && to.meta.title) {
		document.title = to.meta.title;
	}
	next();
});
export default router;
