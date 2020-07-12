import { statistictsConstants } from '../../constants';
import { alertActions } from './alert.actions';
import { StatisticService } from '../../services';

const service = new StatisticService();

export const statisticsActions = {
    getSeenProduct: (id) => {
        const request = seenProduct => ({ type: statistictsConstants.GET_REQUEST, seenProduct });
        const success = seenProduct => ({ type: statistictsConstants.GET_SUCCESS, seenProduct });
        const failure = error => ({ type: statistictsConstants.GET_FAILURE, error });

        return dispatch => {
            dispatch(request({}));

            service.totalViews(id)
                .then(
                    seenProduct => {
                        dispatch(success(seenProduct));
                    },
                    error => {
                        dispatch(failure(error));
                        dispatch(alertActions.error(error));
                    }
                );
        }
    },
    incrementSeenProduct: (id) => {
        const request = seenProduct => ({ type: statistictsConstants.GET_REQUEST, seenProduct });
        const success = seenProduct => ({ type: statistictsConstants.GET_SUCCESS, seenProduct });
        const failure = error => ({ type: statistictsConstants.GET_FAILURE, error });

        return dispatch => {
            dispatch(request({}));

            service.addProductView(id)
                .then(
                    seenProduct => {
                        dispatch(success(seenProduct));
                    },
                    error => {
                        dispatch(failure(error));
                        dispatch(alertActions.error(error));
                    }
                );
        }
    },
};