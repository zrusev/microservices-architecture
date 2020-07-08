import { manufacturersConstants } from '../../constants';
import { alertActions } from './alert.actions';
import { ManufacturerService } from '../../services';

const service = new ManufacturerService();

export const manufacturersActions = {
    topManufacturers: () => {
        const request = manufacturers => ({ type: manufacturersConstants.GET_REQUEST, manufacturers });
        const success = manufacturers => ({ type: manufacturersConstants.GET_SUCCESS, manufacturers });
        const failure = error => ({ type: manufacturersConstants.GET_FAILURE, error });

        return dispatch => {
            dispatch(request([]));

            service.getTopManufacturers()
                .then(
                    manufacturers => {
                        dispatch(success(manufacturers));
                    },
                    error => {
                        dispatch(failure(error));
                        dispatch(alertActions.error(error));
                    }
                )
        };
    }
};