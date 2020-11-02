import Axios, { AxiosPromise } from "axios";
import { IService } from '../interfaces/iservice';
import { PermissionViewModel } from '../classes/PermissionViewModel';

import store  from '../store/store';


export class PermissionsService implements IService<PermissionViewModel> {
    private url: string = process.env.VUE_APP_API;
    add(obj: PermissionViewModel): AxiosPromise<any> {
        throw new Error("Method not implemented.");
    }
    delete(obj: PermissionViewModel): AxiosPromise<PermissionViewModel> {
        throw new Error("Method not implemented.");
    }
    get(): AxiosPromise<PermissionViewModel[]> {
        throw new Error("Method not implemented.");
    }
    getRoots(): AxiosPromise<PermissionViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'permissions/getroots');
    }
    getById(obj: PermissionViewModel): AxiosPromise<PermissionViewModel> {
        throw new Error("Method not implemented.");
    }
    update(obj: PermissionViewModel): AxiosPromise<PermissionViewModel> {
        throw new Error("Method not implemented.");
    }

}

export default new PermissionsService();