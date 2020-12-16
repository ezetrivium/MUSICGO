import Axios, { AxiosPromise } from "axios";
import { IService } from '../interfaces/iservice';
import { BackupData } from '../classes/BackupData';

import store  from '../store/store';


export class BackupService implements IService<BackupData> {
    private url: string = process.env.VUE_APP_API;
    add(obj: BackupData): AxiosPromise<any> {
        
        throw new Error("Method not implemented.");
    }
    delete(obj: BackupData): AxiosPromise<BackupData> {
        throw new Error("Method not implemented.");
    }
    get(): AxiosPromise<BackupData[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'backup/get');
    }

    getById(obj: BackupData): AxiosPromise<BackupData> {
        throw new Error("Method not implemented.");
    }
    update(obj: BackupData): AxiosPromise<BackupData> {
        throw new Error("Method not implemented.");
    }


    set(): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.post(this.url + 'backup/set');
    }


    restore(BackupData : BackupData): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.post(this.url + 'backup/restore',BackupData);
    }




}

export default new BackupService();