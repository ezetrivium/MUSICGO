import { ServiceViewModel } from './ServiceViewModel';
import { UserViewModel } from './UserViewModel';
import { Guid } from "guid-typescript";
export class ContractViewModel{
    public Id: string = '00000000-0000-0000-0000-000000000000';
    public HireDate : Date = new Date();
    public ExpirationDate : Date = new Date();
    public Service : ServiceViewModel | null = new ServiceViewModel();
    // public User : UserViewModel | null = new UserViewModel();

}