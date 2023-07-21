// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  css: [
    '~/assets/plugins/bootstrap-v5.3.0/css/bootstrap.min.css',
    '~/assets/plugins/fontawesome.free-v5.15.4/css/all.min.css',
    '~/assets/plugins/jquery.ui-v1.13.2/css/jquery-ui.min.css',
    '~/assets/plugins/adminlte-v3.2.0/css/adminlte.min.css',
  ],

  app: {
    head: {
      script: [
        {
          src: 'assets/plugins/adminlte-v3.2.0/js/adminlte.min.js',
        },
      ],
    },
  },

  devtools: { enabled: true },

  modules: ['@invictus.codes/nuxt-vuetify'],

  vuetify: {
    /* vuetify options */
    vuetifyOptions: {
      // @TODO: list all vuetify options
    },

    moduleOptions: {
      /* nuxt-vuetify module options */
      treeshaking: true,
      useIconCDN: true,

      /* vite-plugin-vuetify options */
      styles: true,
      autoImport: true,
      importLabComponents: false,
    },
  },
})
