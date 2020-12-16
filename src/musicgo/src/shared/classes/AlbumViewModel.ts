import { UserViewModel } from './UserViewModel';
import { SongViewModel } from './SongViewModel';

export class AlbumViewModel{

    public Id: string = '00000000-0000-0000-0000-000000000000';
    
    public Name: string = "";

    public User : UserViewModel | null = new UserViewModel();

    public ImgKey : string ="";

    public UploadDate :Date = new Date();

    public ImageBase64: string | null | ArrayBuffer = '';

    public Songs: SongViewModel[] = [];
}



    