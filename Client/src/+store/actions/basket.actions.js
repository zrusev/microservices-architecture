import { basketConstants } from '../../constants';
import { alertActions } from './alert.actions';
import { BasketService } from '../../services';

const service = new BasketService();

export const basketActions = {
    addToBasket: (item) => {
        const request = items => ({ type: basketConstants.ADD_REQUEST, items });
        const success = items => ({ type: basketConstants.ADD_SUCCESS, items });
        const failure = error => ({ type: basketConstants.ADD_FAILURE, error });

        return dispatch => {
            dispatch(request([]));

            service.add(item)
                .then(
                    storage => {
                        dispatch(success(storage.items));
                    },
                    error => {
                        dispatch(failure(error));
                        dispatch(alertActions.error(error));
                    }
                );
        }
    },
    removeFromBasket: (item) => {
        const request = items => ({ type: basketConstants.REMOVE_REQUEST, items });
        const success = items => ({ type: basketConstants.REMOVE_SUCCESS, items });
        const failure = error => ({ type: basketConstants.REMOVE_FAILURE, error });

        return dispatch => {
            dispatch(request([]));

            service.remove(item)
                .then(
                    storage => {
                        dispatch(success(storage.items));
                    },
                    error => {
                        dispatch(failure(error));
                        dispatch(alertActions.error(error));
                    }
                );
        }
    },

    clearBasket: () => {
        const request = items => ({ type: basketConstants.CLEAR_REQUEST, items });
        const success = items => ({ type: basketConstants.CLEAR_SUCCESS, items });
        const failure = error => ({ type: basketConstants.CLEAR_FAILURE, error });

        return dispatch => {
            dispatch(request([]));

            service.clear()
                .then(
                    storage => {
                        dispatch(success(storage.items));
                    },
                    error => {
                        dispatch(failure(error));
                        dispatch(alertActions.error(error));
                    }
                );
        }
    }
};