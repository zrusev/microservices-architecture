import { get, put } from '../helpers';
import { statisticsServiceBaseURL } from '../constants';

export class StatisticService {
    constructor() {
        this.serverBaseURL = statisticsServiceBaseURL;

        this.seenProductURL = `${this.serverBaseURL}/seenproducts`;
        this.boughtProdcutsURL = `${this.serverBaseURL}/boughtproduct/boughtproducts`;
    }

    totalViews(id) {
        return get(`${this.seenProductURL}/${id}`);
    }

    addProductView(id){
        return put(`${this.seenProductURL}/${id}`);
    }

    boughtProducts(userId) {
        return get(`${this.boughtProdcutsURL}/${userId}`);
    }
}