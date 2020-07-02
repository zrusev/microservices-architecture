import { get, post } from '../helpers';
import { customerServiceBaseURL } from '../constants';

export class CustomerService {
    constructor() {
        this.serverBaseURL = customerServiceBaseURL;

        this.createCustomerURL = `${this.serverBaseURL}/customers/create`;
        this.getCustomerURL = `${this.serverBaseURL}/customers/get`;
    }

    createCustomer(data) {
        return post(this.createCustomerURL, data);
    }

    getCustomerById(id) {
        return get(`${this.getCustomerURL}?id=${id}`);
    }
}