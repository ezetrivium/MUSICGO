import { UserViewModel } from "../../shared/classes/UserViewModel";
import { PermissionViewModel } from "../../shared/classes/PermissionViewModel";
import store from "../../shared/store/store";

export class Permissionshelper{

    private static permissionValidated : boolean = false;

    static HasPermission(permission :string) : boolean
    {
        let user : UserViewModel = store.getters['user'];
        
        if(user.Permissions!=null && user.Permissions.length>0){
            this.IteratePermissions(user.Permissions,permission);
        }
        return this.permissionValidated;
    }

    private static IteratePermissions(permissions : PermissionViewModel[], permissionToValidate : string){
        for (let p of permissions) {
            if(p.Name.toLowerCase() == permissionToValidate.toLowerCase()){
                this.permissionValidated = true;
            }
            else{
                if(p.Permissions != null && p.Permissions.length > 0 ){
                    this.IteratePermissions(p.Permissions,permissionToValidate);
                }
            }          
        }
    }

}