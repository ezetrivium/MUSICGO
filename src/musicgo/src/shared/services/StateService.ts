import Axios, { AxiosPromise } from "axios";
import { IService } from '../interfaces/iService';
import { StateViewModel } from '../classes/StateViewModel';

import store  from '../store/store';


export class StateService implements IService<StateViewModel> {
    private url: string = process.env.VUE_APP_API;
    add(obj: StateViewModel): AxiosPromise<any> {
        throw new Error("Method not implemented.");
    }
    delete(obj: StateViewModel): AxiosPromise<StateViewModel> {
        throw new Error("Method not implemented.");
    }
    get(): AxiosPromise<StateViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'states/get');
    }
    getById(obj: StateViewModel): AxiosPromise<StateViewModel> {
        throw new Error("Method not implemented.");
    }
    update(obj: StateViewModel): AxiosPromise<StateViewModel> {
        throw new Error("Method not implemented.");
    }

}

export default new StateService();