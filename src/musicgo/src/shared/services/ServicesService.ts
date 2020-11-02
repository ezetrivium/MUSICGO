import Axios, { AxiosPromise } from "axios";
import { IService } from '../interfaces/iservice';
import { ServiceViewModel } from '../classes/ServiceViewModel';

import store  from '../store/store';


export class ServicesService implements IService<ServiceViewModel> {
    private url: string = process.env.VUE_APP_API;
    add(obj: ServiceViewModel): AxiosPromise<any> {
        throw new Error("Method not implemented.");
    }
    delete(obj: ServiceViewModel): AxiosPromise<ServiceViewModel> {
        throw new Error("Method not implemented.");
    }
    get(): AxiosPromise<ServiceViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'services/get');
    }
    getById(obj: ServiceViewModel): AxiosPromise<ServiceViewModel> {
        throw new Error("Method not implemented.");
    }
    update(obj: ServiceViewModel): AxiosPromise<ServiceViewModel> {
        throw new Error("Method not implemented.");
    }

}

export default new ServicesService();