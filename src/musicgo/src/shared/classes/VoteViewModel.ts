import { UserViewModel } from './UserViewModel';
import { SongViewModel } from './SongViewModel';

export class VoteViewModel{
    public Positive: boolean = false;

    public User : UserViewModel | null = new UserViewModel();

    public TimeToPositive : number = 0;

    public Song: SongViewModel = new SongViewModel();
}

