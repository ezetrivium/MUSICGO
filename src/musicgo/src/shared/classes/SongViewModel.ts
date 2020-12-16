
import { UserViewModel } from './UserViewModel';
import { CategoryViewModel } from './CategoryViewModel';
import { VoteViewModel } from './VoteViewModel';
import { AlbumViewModel } from './AlbumViewModel';

export class SongViewModel{

    public Id: string = '00000000-0000-0000-0000-000000000000';

    public Name: string = "";
    
    public User : UserViewModel | null  = new UserViewModel();

    public Category : CategoryViewModel | null = new CategoryViewModel();

    public Album : AlbumViewModel | null = new AlbumViewModel();  

    public Playbacks : number = 0;
    
    public SongKey : string = "";
    
    public UploadDate :Date = new Date();
    
    public VotesCount:number=0;

    public Votes : VoteViewModel[] = [];

    public SongBytes : any;


}

