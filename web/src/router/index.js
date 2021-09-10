import Vue from "vue";
import VueRouter from "vue-router";
import Cash from "../views/Cash";
import Process from "../views/Cash/Process";
import Dashboard from "../views/Dashboard.vue";
import Employee from "../views/Employee";
import ReceiptDetails from "../views/popup/ReceiptDetails";
import Provider from "../views/Provider";
import ReceiptPaymentList from "../views/ReceiptPaymentList";

Vue.use(VueRouter);

const routes = [
	{
		path: "/cash",
		redirect: "/cash/process",
		name: "Cash",
		component: Cash,
		meta: {
			title: "Quy trình | Tiền mặt | Công ty Misa"
		},
		children: [
			{ path: "process", component: Process },
			{
				path: "receipt-payment-list",
				component: ReceiptPaymentList,
				meta: {
					title: "Công ty Misa"
				}
			}
		]
	},

	{
		path: "/categories/employee",
		name: "Employee",
		component: Employee,
		meta: {
			title: "Danh mục | Nhân viên | Công ty Misa"
		}
	},

	{
		path: "/categories/provider",
		name: "Provider",
		component: Provider,
		meta: {
			title: "Nhà cung cấp | Công ty Misa"
		}
	},

	{
		path: "/popup/receipt-details",
		name: "ReceiptDetails",
		component: ReceiptDetails,
		meta: {
			title: "Công ty Misa"
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
