import Axios, { AxiosPromise } from "axios";
import { IService } from '../interfaces/iservice';
import { VoteViewModel } from '../classes/VoteViewModel';
import { RecoverPasswordViewModel } from '../classes/RecoverPasswordViewModel';
import { AuthenticationViewModel } from '../classes/AuthenticationViewModel';
import store  from '../store/store';


export class VoteService implements IService<VoteViewModel> {
    private url: string = process.env.VUE_APP_API;
    add(obj: VoteViewModel): AxiosPromise<any> {
        
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.post(this.url + 'votes/add', obj);
    }
    delete(obj: VoteViewModel): AxiosPromise<any> {
        throw new Error("Method not implemented.");
    }

    get(): AxiosPromise<VoteViewModel[]> {
        throw new Error("Method not implemented.");
    }

    getUserVotes(): AxiosPromise<VoteViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'votes/getuser');
    }

    getById(obj: VoteViewModel): AxiosPromise<VoteViewModel> {
        throw new Error("Method not implemented.");
    }


    update(obj: VoteViewModel): AxiosPromise<any> {
        throw new Error("Method not implemented.");
    }

 

}

export default new VoteService();