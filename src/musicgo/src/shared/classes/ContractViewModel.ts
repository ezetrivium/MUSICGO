import { ServiceViewModel } from './ServiceViewModel';
import { UserViewModel } from './UserViewModel';
export class ContractViewModel{
    public Id: number = 0;
    public HireDate : Date = new Date();
    public ExpirationDate : Date = new Date();
    public Service : ServiceViewModel | null = null;
    public User : UserViewModel | null = null;

}