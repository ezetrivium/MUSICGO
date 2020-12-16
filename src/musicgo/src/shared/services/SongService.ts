import Axios, { AxiosPromise } from "axios";
import { IService } from '../interfaces/iservice';
import { SongViewModel } from '../classes/SongViewModel';
import { UserViewModel } from '../classes/UserViewModel';

import store  from '../store/store';
import { CategoryViewModel } from '../classes/CategoryViewModel';


export class SongService implements IService<SongViewModel> {
    private url: string = process.env.VUE_APP_API;
    add(obj: any): AxiosPromise<any> {
        
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.post(this.url + 'songs/add', obj);
    }
    calculateSuccess(obj: SongViewModel): AxiosPromise<any> {
        
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.post(this.url + 'songs/calculatesuccess', obj);
    }
    delete(obj: SongViewModel): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.delete(this.url + 'songs/delete/' + obj.Id);
    }

    get(): AxiosPromise<SongViewModel[]> {
        throw new Error("Method not implemented.");
    }

    getReport(): AxiosPromise<SongViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'songs/getreport');
    }

    getRefresh(refresh:boolean): AxiosPromise<SongViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'songs/get',{
            params: {
                refresh : refresh
            }
        });
    }


    getById(obj: SongViewModel): AxiosPromise<SongViewModel> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'songs/getbyid', {
            params: {
                id : obj.Id
            }
        });
    }

    getUserSongs(): AxiosPromise<UserViewModel> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'songs/getuser');
    }

    getSongsToPlay(refresh:boolean): AxiosPromise<SongViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'songs/toplay', {
            params: {
                refresh : refresh
            }
        });
    }

    getSongsVoted(): AxiosPromise<SongViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'songs/voted');
    }

    update(obj: SongViewModel): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.put(this.url + 'songs/update', obj);
    }

    play(obj: SongViewModel): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'sample/getsample', {
            params: {
                songKey : obj.SongKey
            }
        });
    }


}

export default new SongService();