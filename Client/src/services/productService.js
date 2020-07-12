import { get, post, put, remove } from '../helpers';
import { customerServiceBaseURL } from '../constants';

export class ProductService {
    constructor() {
        this.serverBaseURL = customerServiceBaseURL;

        this.searchProductsURL = `${this.serverBaseURL}/products`;
        this.detailsProductURL = `${this.serverBaseURL}/products`;
        this.createProductURL = `${this.serverBaseURL}/products/create`;
        this.editProductURL = `${this.serverBaseURL}/products/edit`;
        this.deleteProductURL = `${this.serverBaseURL}/products/delete`;
    }

    getProducts(page, category, manufacturer, name) {
        const url = `${this.searchProductsURL}` +
            ((page || '') && `?page=${page}`) +
            ((category || '') && `&category=${category}`) +
            ((manufacturer || '') && `&manufacturer=${manufacturer}`) +
            ((name || '') && `&name=${name}`);

        return get(url);
    }

    getProductDetails(id, name) {
        return get(`${this.detailsProductURL}/details?id=${id}&name=${name}`);
    }

    createProduct(data) {
        return post(this.createProductURL, data);
    }

    editProduct(id, data) {
        return put(`${this.editProductURL}/${id}`, data);
    }

    deleteProduct(id) {
        return remove(`${this.editProductURL}/${id}`);
    }
}