import Vue from 'vue';
import VueI18n, { LocaleMessages, LocaleMessage } from 'vue-i18n';


Vue.use(VueI18n);

const i18n = new VueI18n({
     locale: process.env.VUE_APP_I18N_LOCALE || 'es',
     fallbackLocale: process.env.VUE_APP_I18N_FALLBACK_LOCALE || 'es',
     silentTranslationWarn: true,
  });
// export default class I18n {
//         return new VueI18n({
//             // locale: process.env.VUE_APP_I18N_LOCALE || 'es',
//             // fallbackLocale: process.env.VUE_APP_I18N_FALLBACK_LOCALE || 'es',
//             // silentTranslationWarn: true,
//             // messages: messages
//         });
// }
export default i18n;