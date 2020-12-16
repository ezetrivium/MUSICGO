import Axios, { AxiosPromise } from "axios";
import { IService } from '../interfaces/iservice';
import { BinnacleViewModel } from '../classes/BinnacleViewModel';

import store  from '../store/store';


export class BinnacleService implements IService<BinnacleViewModel> {
    private url: string = process.env.VUE_APP_API;
    add(obj: BinnacleViewModel): AxiosPromise<any> {
        
        throw new Error("Method not implemented.");
    }
    delete(obj: BinnacleViewModel): AxiosPromise<BinnacleViewModel> {
        throw new Error("Method not implemented.");
    }

    get(): AxiosPromise<BinnacleViewModel[]> {
        throw new Error("Method not implemented.");
    }

    getWithFilters(dateto : any, dateFrom : any, userName : string): AxiosPromise<BinnacleViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'binnacle/get',{
            params: {
                dateTo : dateto,
                dateFrom : dateFrom,
                userName : userName
            }
        });
    }

    getById(obj: BinnacleViewModel): AxiosPromise<BinnacleViewModel> {
        throw new Error("Method not implemented.");
    }
    update(obj: BinnacleViewModel): AxiosPromise<BinnacleViewModel> {
        throw new Error("Method not implemented.");
    }


}

export default new BinnacleService();