import Axios, { AxiosPromise } from "axios";
import { IService } from '../interfaces/iservice';
import { LanguageViewModel } from '../classes/LanguageViewModel';

import store  from '../store/store';


export class LanguageService implements IService<LanguageViewModel> {
    private url: string = process.env.VUE_APP_API;
    add(obj: LanguageViewModel): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.post(this.url + 'language/add', obj);
    }

    addDictionary(obj: LanguageViewModel): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.post(this.url + 'language/adddictionary', obj);
    }


    updateDictionary(obj: LanguageViewModel): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.put(this.url + 'language/updatedictionary', obj);
    }


    delete(obj: LanguageViewModel): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.delete(this.url + 'language/delete/' + obj.Id);
    }
    get(): AxiosPromise<LanguageViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'languages/get');
    }

    getDictionaryKeysAll(): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'languages/getdictionarykeysall');
    }

    getDictionaryKeys(languageID:String): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'languages/getdictionarykeys', {
            params: {
                languageID : languageID
            }
        });
    }

    getById(obj: LanguageViewModel): AxiosPromise<LanguageViewModel> {
        return Axios.get(this.url + 'languages/getbyid', {
            params: {
                id : obj.Id
            }
        });
    }

    getByCode(obj: string): AxiosPromise<LanguageViewModel> {
        return Axios.get(this.url + 'languages/getbycode', {
            params: {
                code : obj
            }
        });
    }
    update(obj: LanguageViewModel): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.put(this.url + 'language/update', obj);
    }

}

export default new LanguageService();