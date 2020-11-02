import Axios, { AxiosPromise } from "axios";
import { IService } from '../interfaces/iservice';
import { UserViewModel } from '../classes/UserViewModel';
import { PreferenceViewModel } from '../classes/PreferenceViewModel';

import { AuthenticationViewModel } from '../classes/AuthenticationViewModel';
import store  from '../store/store';


export class ContractService implements IService<UserViewModel> {
    private url: string = process.env.VUE_APP_API;
    add(obj: UserViewModel): AxiosPromise<any> {
        
        return Axios.post(this.url + 'contract/contractservice', obj);
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


    preference(obj: UserViewModel): AxiosPromise<any> {
        return Axios.post(this.url + 'mercadopago/preference', obj);
    }

    notification(extRef: string): AxiosPromise<any> {
        return Axios.get(this.url + 'mercadopago/notification',{
            params: {
                externalReference : extRef
            }
        });
    }
}

export default new ContractService();