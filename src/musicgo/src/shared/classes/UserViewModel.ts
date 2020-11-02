import { LanguageViewModel } from './LanguageViewModel';
import { PermissionViewModel } from './PermissionViewModel';
import { ContractViewModel } from './ContractViewModel';
import { Guid } from "guid-typescript";
export class UserViewModel{
    public Id: string = '00000000-0000-0000-0000-000000000000';
    public Language: LanguageViewModel | null = new LanguageViewModel();
    public Permissions: PermissionViewModel[] = [];
    public Email: string = '';
    public UserName: string ='';
    public Name: string = '';
    public LastName: string = '';
    public Password: string = '';
    public ArtistName: string = '';
    public Playbacks: number = 0;
    public Blocked : boolean = false;
    public User : UserViewModel | null = null;
    
    public ImageBase64: string | null | ArrayBuffer = '';
    public Contract: ContractViewModel | null = new ContractViewModel();
}