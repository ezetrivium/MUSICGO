import { LanguageViewModel } from './LanguageViewModel';
import { PermissionViewModel } from './PermissionViewModel';

export class UserViewModel{
    public Id: number = 0;
    public Language: LanguageViewModel | null = null;
    public Permissions: PermissionViewModel[] = [];
    public Email: string = '';
    public UserName: string ='';
    public Name: string = '';
    public LastName: string = '';
    public Password: string = '';
    public ArtistName: string = '';
    public Playbacks: number = 0;
}