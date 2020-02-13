import { Dictionary } from 'vue-router/types/router';

export class LanguageViewModel {
    public Id: number = 0;
    public Name: string = '';
    public Code: string = '';
    public Dictionary: Dictionary<string> | null = null;
}
