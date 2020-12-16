import Axios, { AxiosPromise } from "axios";
import { IService } from '../interfaces/iservice';
import { ClaimViewModel } from '../classes/ClaimViewModel';


import store  from '../store/store';


export class ClaimService implements IService<ClaimViewModel> {
    private url: string = process.env.VUE_APP_API;
    add(obj: ClaimViewModel): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.post(this.url + 'claim/add', obj);
    }



    delete(obj: ClaimViewModel): AxiosPromise<any> {
        throw new Error("Method not implemented.");
    }
    get(): AxiosPromise<ClaimViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'claims/get');
    }

    getUserClaims(): AxiosPromise<ClaimViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'claims/getuser');
    }

    getById(obj: ClaimViewModel): AxiosPromise<ClaimViewModel> {
        throw new Error("Method not implemented.");
    }

 
    update(obj: ClaimViewModel): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.put(this.url + 'claim/update', obj);
    }

}

export default new ClaimService();