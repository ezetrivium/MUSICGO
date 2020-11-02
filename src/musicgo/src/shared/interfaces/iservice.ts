import { AxiosPromise } from "axios";

export interface IService<T> {
    add(obj: T): AxiosPromise<any>;
    delete(obj: T): AxiosPromise<T>;
    get(): AxiosPromise<T[]>;
    getById(obj: T): AxiosPromise<T>;
    update(obj: T): AxiosPromise<T>;
}
