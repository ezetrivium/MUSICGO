import { AxiosPromise } from "axios";

export interface IService<T> {
    add(obj: T): AxiosPromise<T>;
    delete(obj: T): AxiosPromise<T>;
    get(): AxiosPromise<T[]>;
    getById(obj: T): AxiosPromise<T>;
    update(obj: T): AxiosPromise<T>;
}
