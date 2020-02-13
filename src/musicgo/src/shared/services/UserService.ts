import Axios, { AxiosPromise } from "axios";
import { IService } from '../interfaces/iservice';
import { UserViewModel } from '../classes/UserViewModel';



export class UserService implements IService<UserViewModel> {
    private url: string = process.env.VUE_APP_API;
    add(obj: UserViewModel): AxiosPromise<UserViewModel> {
        throw new Error("Method not implemented.");
    }
    delete(obj: UserViewModel): AxiosPromise<UserViewModel> {
        throw new Error("Method not implemented.");
    }
    get(): AxiosPromise<UserViewModel[]> {
        throw new Error("Method not implemented.");
    }
    getById(obj: UserViewModel): AxiosPromise<UserViewModel> {
        throw new Error("Method not implemented.");
    }
    update(obj: UserViewModel): AxiosPromise<UserViewModel> {
        throw new Error("Method not implemented.");
    }
    
    login(obj: UserViewModel): AxiosPromise<UserViewModel>{
        return Axios.post(this.url + 'login',obj);
    }


}

export default new UserService();