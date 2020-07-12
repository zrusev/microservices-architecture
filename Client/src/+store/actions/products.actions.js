import { productsConstants } from '../../constants';
import { alertActions, statisticsActions } from './index';
import { ProductService } from '../../services';

const service = new ProductService();

export const productsActions = {
    get: (page, category, manufacturer, name) => {
        const request = products => ({ type: productsConstants.GET_REQUEST, products });
        const success = products => ({ type: productsConstants.GET_SUCCESS, products });
        const failure = error => ({ type: productsConstants.GET_FAILURE, error });

        return dispatch => {
            dispatch(request([]));

            service.getProducts(page, category, manufacturer, name)
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
    },
    getProduct: (id, name) => {
        const request = product => ({ type: productsConstants.GET_DETAILS_REQUEST, product });
        const success = product => ({ type: productsConstants.GET_DETAILS_SUCCESS, product });
        const failure = error => ({ type: productsConstants.GET_DETAILS_FAILURE, error });

        return dispatch => {
            dispatch(request(null));

            service.getProductDetails(id, name)
                .then(
                    product => {
                        dispatch(success(product));

                        dispatch(statisticsActions.getSeenProduct(id));
                        dispatch(statisticsActions.incrementSeenProduct(id));
                    },
                    error => {
                        dispatch(failure(error));
                        dispatch(alertActions.error(error));
                    }
                )
        };
    }
};