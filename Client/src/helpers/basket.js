import { initialState } from '../+store/reducers/basket.reducer';

export const getStorage = () => new Promise((resolve, reject) => {
    if(!window.localStorage.getItem('user_basket')) {
        window.localStorage.setItem('user_basket', JSON.stringify(initialState));
    }

    resolve(JSON.parse(window.localStorage.getItem('user_basket')));
});

export const setStorage = storage => new Promise((resolve, reject) => {
    window.localStorage.setItem('user_basket', JSON.stringify(storage));

    resolve(JSON.parse(window.localStorage.getItem('user_basket')));
});

export const clearStorage = () => new Promise((resolve, reject) => {
    window.localStorage.setItem('user_basket', JSON.stringify(initialState));

    resolve(initialState);
});
