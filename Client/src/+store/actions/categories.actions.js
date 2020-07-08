import { categoriesConstants } from '../../constants';
import { alertActions } from './alert.actions';
import { CategoryService } from '../../services';

const service = new CategoryService();

export const categoriesActions = {
    topCategories: () => {
        const request = categories => ({ type: categoriesConstants.GET_REQUEST, categories });
        const success = categories => ({ type: categoriesConstants.GET_SUCCESS, categories });
        const failure = error => ({ type: categoriesConstants.GET_FAILURE, error });

        return dispatch => {
            dispatch(request([]));

            service.getTopCategories()
                .then(
                    categories => {
                        dispatch(success(categories));
                    },
                    error => {
                        dispatch(failure(error));
                        dispatch(alertActions.error(error));
                    }
                )
        };
    }
};