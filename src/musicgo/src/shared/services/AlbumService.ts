import Axios, { AxiosPromise } from "axios";
import { IService } from '../interfaces/iservice';
import { AlbumViewModel } from '../classes/AlbumViewModel';
import { UserViewModel } from '../classes/UserViewModel';

import store  from '../store/store';


export class AlbumService implements IService<AlbumViewModel> {
    private url: string = process.env.VUE_APP_API;
    add(obj: any): AxiosPromise<any> {
        
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.post(this.url + 'albums/add', obj);
    }
    delete(obj: AlbumViewModel): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.delete(this.url + 'albums/delete/' + obj.Id);
    }

    get(): AxiosPromise<AlbumViewModel[]> {
        throw new Error("Method not implemented.");
    }

    getById(obj: AlbumViewModel): AxiosPromise<AlbumViewModel> {
        throw new Error("Method not implemented.");
    }

    getUserAlbums(): AxiosPromise<UserViewModel> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'albums/getuser');
    }

    update(obj: any): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.put(this.url + 'Albums/update', obj);
    }


}

export default new AlbumService();