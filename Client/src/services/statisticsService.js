import { get } from '../helpers';
import { statisticsServiceBaseURL } from '../constants';

export class StatisticsService {
    constructor() {
        this.serverBaseURL = statisticsServiceBaseURL;

        this.seenProductURL = `${this.serverBaseURL}/seenproduct/totalviews`;
        this.boughtProdcutsURL = `${this.serverBaseURL}/boughtproduct/boughtproducts`;
    }

    totalViews(id) {
        get(`${this.seenProductURL}/${id}`);
    }

    boughtProducts(userId) {
        get(`${this.boughtProdcutsURL}/${userId}`);
    }
}