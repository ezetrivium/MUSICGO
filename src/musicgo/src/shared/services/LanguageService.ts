import Axios, { AxiosPromise } from "axios";
import { IService } from '../interfaces/iservice';
import { LanguageViewModel } from '../classes/LanguageViewModel';

import store  from '../store/store';


export class LanguageService implements IService<LanguageViewModel> {
    private url: string = process.env.VUE_APP_API;
    add(obj: LanguageViewModel): AxiosPromise<any> {
        throw new Error("Method not implemented.");
    }
    delete(obj: LanguageViewModel): AxiosPromise<LanguageViewModel> {
        throw new Error("Method not implemented.");
    }
    get(): AxiosPromise<LanguageViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'languages/get');
    }
    getById(obj: LanguageViewModel): AxiosPromise<LanguageViewModel> {
        throw new Error("Method not implemented.");
    }
    update(obj: LanguageViewModel): AxiosPromise<LanguageViewModel> {
        throw new Error("Method not implemented.");
    }

}

export default new LanguageService();