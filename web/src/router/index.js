import Vue from "vue";
import VueRouter from "vue-router";
import AccountSystem from "../views/AccountSystem";
import Cash from "../views/Cash";
import Process from "../views/Cash/Process";
import Dashboard from "../views/Dashboard.vue";
import Employee from "../views/Employee";
import PaymentDetails from "../views/popup/PaymentDetails";
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
			title: "Quy trình | Tiền mặt | Công ty Misa",
			showPopupView: true
		},
		children: [
			{
				path: "process",
				name: "Process",
				component: Process
			},
			{
				path: "receipt-payment-list",
				name: "ReceiptPaymentList",
				component: ReceiptPaymentList,
				meta: {
					title: "Công ty Misa"
				}
			},

			{
				path: "/popup/payment-details/:id",
				name: "PaymentDetailsFromPaymentViewWithId",
				components: { default: ReceiptPaymentList, "popup-view": PaymentDetails },
				meta: {
					title: "Công ty Misa"
				}
			},
			{
				path: "/popup/payment-details",
				name: "PaymentDetailsFromPaymentView",
				components: { default: ReceiptPaymentList, "popup-view": PaymentDetails },
				meta: {
					title: "Công ty Misa"
				},
				props: true
			},
			{
				path: "/popup/payment-details",
				name: "PaymentDetailsFromProcessView",
				components: { default: Process, "popup-view": PaymentDetails },
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
		path: "/categories/account",
		name: "AccountSystem",
		component: AccountSystem,
		meta: {
			title: "Hệ thống tài khoản | Công ty Misa"
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
