import { get } from '../helpers';
import { customerServiceBaseURL } from '../constants';

export class ManufacturerService {
    constructor() {
        this.serverBaseURL = customerServiceBaseURL;

        this.topManufacturersURL = `${this.serverBaseURL}/manufacturers/top`;
    }

    getTopManufacturers() {
        return get(this.topManufacturersURL);
    }
}