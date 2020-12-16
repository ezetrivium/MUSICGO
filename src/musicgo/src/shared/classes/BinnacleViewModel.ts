
import { UserViewModel } from './UserViewModel';
export class BinnacleViewModel{
    public Id: string = '00000000-0000-0000-0000-000000000000';
    public Description : string = "";
    public Date : Date = new Date();
    public User : UserViewModel | null = new UserViewModel();

}