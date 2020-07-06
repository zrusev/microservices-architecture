import { productsConstants } from '../../constants';
import { alertActions } from './alert.actions';
import { ProductService } from '../../services';

const service = new ProductService();

export const productsActions = {
    get: (page) => {
        const request = products => ({ type: productsConstants.GET_REQUEST, products });
        const success = products => ({ type: productsConstants.GET_SUCCESS, products });
        const failure = error => ({ type: productsConstants.GET_FAILURE, error });

        return dispatch => {
            dispatch(request([]));

            service.getProducts(page)
                .then(
                    products => {
                        dispatch(success(products));
                    },
                    error => {
                        dispatch(failure(error));
                        dispatch(alertActions.error(error));
                    }
                )
        };
    }
};