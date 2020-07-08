import { get } from '../helpers';
import { customerServiceBaseURL } from '../constants';

export class CategoryService {
    constructor() {
        this.serverBaseURL = customerServiceBaseURL;

        this.topCategoriesURL = `${this.serverBaseURL}/categories/top`;
    }

    getTopCategories() {
        return get(this.topCategoriesURL);
    }
}