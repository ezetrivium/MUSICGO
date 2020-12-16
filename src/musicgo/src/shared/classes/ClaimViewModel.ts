import { SongViewModel } from './SongViewModel';
import { StateViewModel } from './StateViewModel';
import { UserViewModel } from './UserViewModel';

export class ClaimViewModel{
    public Description: string = "";

    public SongClaimed : SongViewModel = new SongViewModel();

    public State : StateViewModel = new StateViewModel();

    public User : UserViewModel = new UserViewModel();

    public Date : Date = new Date();
}
