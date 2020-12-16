import Axios, { AxiosPromise } from "axios";
import { IService } from '../interfaces/iservice';
import { PermissionViewModel } from '../classes/PermissionViewModel';
import { PermissionsParentViewModel } from '../classes/PermissionsParentViewModel';

import store  from '../store/store';


export class PermissionsService implements IService<PermissionViewModel> {
    private url: string = process.env.VUE_APP_API;
    add(obj: PermissionViewModel): AxiosPromise<any> {
        throw new Error("Method not implemented.");
    }

    addPermissionsParent(obj: PermissionsParentViewModel): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.post(this.url + 'permissions/add', obj);
    }
    delete(obj: PermissionViewModel): AxiosPromise<PermissionViewModel> {
        throw new Error("Method not implemented.");
    }
    get(): AxiosPromise<PermissionViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'permissions/get');
    }

    getGroups(): AxiosPromise<PermissionViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'permissions/getgroups');
    }

    getChilds(): AxiosPromise<PermissionViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'permissions/getchilds');
    }

    getRoots(): AxiosPromise<PermissionViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'permissions/getroots');
    }
    getById(obj: PermissionViewModel): AxiosPromise<PermissionViewModel> {
        throw new Error("Method not implemented.");
    }
    update(obj: PermissionViewModel): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.put(this.url + 'permissions/update', obj);
    }

    addPermissionGroup(obj: PermissionsParentViewModel): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.post(this.url + 'permissionsgroup/add', obj);
    }

}

export default new PermissionsService();