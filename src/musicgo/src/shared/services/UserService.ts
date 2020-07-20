import Axios, { AxiosPromise } from "axios";
import { IService } from '../interfaces/iservice';
import { UserViewModel } from '../classes/UserViewModel';
import { RecoverPasswordViewModel } from '../classes/RecoverPasswordViewModel';
import { AuthenticationViewModel } from '../classes/AuthenticationViewModel';
import store  from '../store/store';


export class UserService implements IService<UserViewModel> {
    private url: string = process.env.VUE_APP_API;
    add(obj: UserViewModel): AxiosPromise<UserViewModel> {
        throw new Error("Method not implemented.");
    }
    delete(obj: UserViewModel): AxiosPromise<UserViewModel> {
        throw new Error("Method not implemented.");
    }
    get(): AxiosPromise<UserViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'users/get');
    }
    getById(obj: UserViewModel): AxiosPromise<UserViewModel> {
        throw new Error("Method not implemented.");
    }
    update(obj: UserViewModel): AxiosPromise<UserViewModel> {
        throw new Error("Method not implemented.");
    }
    
    login(obj: UserViewModel): AxiosPromise<UserViewModel>{
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.post(this.url + 'login',obj);
    }

    getAuthToken(obj: AuthenticationViewModel): AxiosPromise<string>{
        const params = new URLSearchParams();
        params.append('username', obj.username);
        params.append('password', obj.password);
        params.append('grant_type', obj.grant_type);
        return Axios.post(this.url + 'token',params);
    }


    recoverPassword(userName: UserViewModel): AxiosPromise<string>{

        return Axios.post(this.url + 'account/recoverpassword', userName);
    }

    updatePassword(obj: RecoverPasswordViewModel): AxiosPromise<string>{
        return Axios.post(this.url + 'account/updatepassword',obj);
    }


}

export default new UserService();