export class LanguageHelper {
    private static readonly langKey: string = "lang"

    static get locale(): string {
        const lang = localStorage.getItem(this.langKey);
        return lang ? lang : "es";
    }

    static set locale(lang: string) {
        localStorage.setItem(this.langKey, lang);
    }

    static removeLanguage() {
        localStorage.removeItem(this.langKey);
    }
}