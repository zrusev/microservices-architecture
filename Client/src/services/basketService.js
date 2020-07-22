import { getStorage, setStorage, clearStorage } from '../helpers/index';

export class BasketService {
    add(item) {
        return getStorage()
            .then(storage => {
                storage.items.push(item);
                storage.total = storage.items.length;

                return setStorage(storage);
            });
    }

    remove(item){
        return getStorage()
            .then(storage => {
                const index = storage.items.findIndex(storageItem => storageItem.id === item.id && storageItem.name === item.name);

                if(index > -1){
                    storage.items.splice(index, 1);
                }

                storage.total = storage.items.length;

                return setStorage(storage);
            });
    }

    clear() {
        return clearStorage();
    }
}