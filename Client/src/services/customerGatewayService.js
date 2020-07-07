import { get } from '../helpers';
import { customerGatewayServiceBaseURL } from '../constants';

export class CustomerGatewayService {
    constructor() {
        this.serverBaseURL = customerGatewayServiceBaseURL;

        this.topProductsURL = `${this.serverBaseURL}/products/top`;
    }

    topProducts() {
        return get(this.topProductsURL);
    }
}