import Vue from 'vue'
import VueI18n, { LocaleMessages, LocaleMessage } from 'vue-i18n'


Vue.use(VueI18n)


export default class I18nGenerator {
    static generate(messages: LocaleMessages): VueI18n {
        return new VueI18n({
            locale: process.env.VUE_APP_I18N_LOCALE || 'es',
            fallbackLocale: process.env.VUE_APP_I18N_FALLBACK_LOCALE || 'es',
            silentTranslationWarn: true,
            LocaleMessages: messages
        });
    }

}
