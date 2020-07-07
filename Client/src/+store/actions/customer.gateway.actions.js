import { customerGatewayConstants } from '../../constants';
import { CustomerGatewayService } from '../../services';
import { alertActions } from './alert.actions';

const service = new CustomerGatewayService();

export const customerGatewayActions = {
    topProducts: () => {
        const request = products => ({ type: customerGatewayConstants.TOP_PRODUCTS_REQUEST, products });
        const success = products => ({ type: customerGatewayConstants.TOP_PRODUCTS_SUCCESS, products });
        const failure = error => ({ type: customerGatewayConstants.TOP_PRODUCTS_FAILURE, error });

        return dispatch => {
            dispatch(request([]));

            service.topProducts()
                .then(
                    products => {
                        dispatch(success(products));
                    },
                    error => {
                        dispatch(failure(error));
                        dispatch(alertActions.error(error));
                    }
                );
        }
    },
};