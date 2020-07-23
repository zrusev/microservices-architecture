import { post } from '../helpers';
import { orderServiceBaseURL } from '../constants';

export class OrderService {
    constructor() {
        this.serverBaseURL = orderServiceBaseURL;

        this.createOrderURL = `${this.serverBaseURL}/orders/create`;
    }

    newOrder(order) {
        return post(this.createOrderURL, order);
    }
}