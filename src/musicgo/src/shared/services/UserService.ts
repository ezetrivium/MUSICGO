import Axios, { AxiosPromise } from "axios";
import { IService } from '../interfaces/iservice';
import { UserViewModel } from '../classes/UserViewModel';
import { RecoverPasswordViewModel } from '../classes/RecoverPasswordViewModel';
import { AuthenticationViewModel } from '../classes/AuthenticationViewModel';
import store  from '../store/store';
import { UserReportViewModel } from '../classes/UserReportViewModel';


export class UserService implements IService<UserViewModel> {
    private url: string = process.env.VUE_APP_API;
    add(obj: any): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.post(this.url + 'users/add', obj);
    }

    subscribe(obj: any): AxiosPromise<any> {
        
        return Axios.post(this.url + 'users/subscribe', obj);
    }

    delete(obj: UserViewModel): AxiosPromise<any> {
        
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.delete(this.url + 'users/delete/' + obj.Id);
    }

    deleteMyProfile(obj: UserViewModel): AxiosPromise<any> {
        
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.delete(this.url + 'users/deletemyprofile/' + obj.Id);
    }

    get(): AxiosPromise<UserViewModel[]> {
        throw new Error("Method not implemented.");
    }

    getReport(): AxiosPromise<UserReportViewModel> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'users/getreport');
    }

    getRefresh(refresh:boolean): AxiosPromise<UserViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'users/get',{
            params: {
                refresh : refresh
            }
        });
    }
    getById(obj: UserViewModel): AxiosPromise<UserViewModel> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'users/getbyid', {
            params: {
                id : obj.Id
            }
        });
    }
    getProfile(obj: UserViewModel): AxiosPromise<UserViewModel> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'users/getprofile', {
            params: {
                id : obj.Id
            }
        });
    }
    update(obj: any): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.post(this.url + 'users/update', obj);
    }

    myProfile(obj: any): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.post(this.url + 'users/myprofile', obj);
    }

    setUserPermissions(obj: UserViewModel): AxiosPromise<any> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.post(this.url + 'users/setuserpermissions', obj);
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