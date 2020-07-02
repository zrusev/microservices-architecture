import { post } from '../helpers';
import { identityServiceBaseURL } from '../constants';

export class IdentityService {
    constructor() {
        this.serverBaseURL = identityServiceBaseURL;
        
        this.loginURL = `${this.serverBaseURL}/users/login`;
        this.facebookLoginUrl = `${this.serverBaseURL}/externalauth/facebook`;
        this.registerURL = `${this.serverBaseURL}/users/register`;
    }

    login(credentials) {
        const url = credentials.facebookLoginUrl 
            ? this.facebookLoginUrl 
            : this.loginURL;
        
            return post(url, credentials);
    }

    register(credentials) {
        return post(this.registerURL, credentials);
    }

    logout() {
        return window.localStorage.removeItem('auth_token');
    }
}