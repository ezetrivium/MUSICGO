import Axios, { AxiosPromise } from "axios";
import { IService } from '../interfaces/iservice';
import { CategoryViewModel } from '../classes/CategoryViewModel';


import store  from '../store/store';


export class CategoryService implements IService<CategoryViewModel> {
    private url: string = process.env.VUE_APP_API;
    add(obj: CategoryViewModel): AxiosPromise<any> {
        
        throw new Error("Method not implemented.");
    }
    delete(obj: CategoryViewModel): AxiosPromise<any> {

        throw new Error("Method not implemented.");
    }

    get(): AxiosPromise<CategoryViewModel[]> {
        Axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken
        return Axios.get(this.url + 'Categories/get');
    }


    getById(obj: CategoryViewModel): AxiosPromise<CategoryViewModel> {
        throw new Error("Method not implemented.");
    }



    update(obj: CategoryViewModel): AxiosPromise<any> {
        throw new Error("Method not implemented.");
    }


}

export default new CategoryService();