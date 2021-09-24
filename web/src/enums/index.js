/**
 * Các enum có trong app
 * Created by: VLVU (20/7/2021)
 */

/**
 * Enum định dạng của giá trị của input
 * Created by: VLVU(2021)
 */
const format = {
	currency: 0,
	number: 1,
	string: 2,
	email: 3,
	phoneNumber: 4

};

/**
 * Trạng thái khi làm viêc của dialog
 * Created by: VLVU (2021)
 */
const dialogState = {
	post: 0,
	put: 1,
	get: 2,
	delete: 3,
	view: 4
};

/**
 * status code trả về của response
 * Created by: VLVU (2021)
 */
const statusCode = {
	serverError: 500,
	success: 200,
	created: 201,
	notFound: 404
};

export default {
	format,
	dialogState,
	statusCode
};
