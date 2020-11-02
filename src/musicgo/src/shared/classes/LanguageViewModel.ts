import { Dictionary } from 'vue-router/types/router';
import { Guid } from "guid-typescript";
export class LanguageViewModel {
    public Id: string='00000000-0000-0000-0000-000000000000';
    public Name: string = '';
    public Code: string = '';
    public Dictionary: Dictionary<string> | null = null;
}
