import { customerConstants } from '../../constants';
import { CustomerService } from '../../services';
import { alertActions } from './alert.actions';

const service = new CustomerService();

export const customerActions = {
    create: (firstName, lastName, address1, address2, phoneNumber) => {
        const request = customer => ({ type: customerConstants.CREATE_REQUEST, customer });
        const success = customer => ({ type: customerConstants.CREATE_SUCCESS, customer });
        const failure = error => ({ type: customerConstants.CREATE_FAILUREE, error });

        return dispatch => {
            dispatch(request({ firstName }));

            service.createCustomer({ firstName, lastName, address1, address2, phoneNumber })
                .then(
                    customer => {
                        dispatch(success(customer));
                    },
                    error => {
                        dispatch(failure(error));
                        dispatch(alertActions.error(error));
                    }
                );
        }
    },
    get: (id) => {
        const request = customer => ({ type: customerConstants.GET_REQUEST, customer });
        const success = customer => ({ type: customerConstants.GET_SUCCESS, customer });
        const failure = error => ({ type: customerConstants.GET_FAILURE, error });

        return dispatch => {
            dispatch(request({ id }));

            service.getCustomerById({ id })
                .then(
                    customer => {
                        dispatch(success(customer));
                    },
                    error => {
                        dispatch(failure(error));
                        dispatch(alertActions.error(error));
                    }
                )
        };
    }
};