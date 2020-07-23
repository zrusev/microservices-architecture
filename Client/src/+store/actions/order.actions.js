import { orderConstants } from '../../constants';
import { alertActions } from './alert.actions';
import { OrderService } from '../../services';

const service = new OrderService();

export const orderActions = {
    create: (items) => {
        const request = order => ({ type: orderConstants.CREATE_REQUEST, order });
        const success = order => ({ type: orderConstants.CREATE_SUCCESS, order });
        const failure = error => ({ type: orderConstants.CREATE_FAILURE, error });

        return dispatch => {
            dispatch(request({}));

            const order = {
                status: 1,
                orderDate: new Date(),
                products: items.reduce((acc, cur, ind) => {
                    acc.push({
                        productId: +(cur.id),
                        itemId: ind + 1,
                        quantity: 1
                    });

                    return acc;
                }, [])
            };

            service.newOrder(order)
                .then(
                    order => {
                        dispatch(success(order));
                        dispatch(alertActions.success({ message: `New order with ID: ${order.id} has been created`, severity: 'success' }));
                    },
                    error => {
                        dispatch(failure(error));
                        dispatch(alertActions.error(error));
                    }
                )
        };
    }
};