import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import vuetify from 'vite-plugin-vuetify'

import FoundationSharedAutoImport from '@dative-gpi/foundation-shared-loader'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    vuetify(),
    FoundationSharedAutoImport({ skipShared: false, skipCore: true, skipAdmin: false })
  ],
  define: { 'process.env': {} },
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    port: 8080,
    host: '0.0.0.0'
  },
  optimizeDeps: {
    include: [
      "ajv",
      "@lexical/selection", 
      "lexical",
      "axios",
      "lodash",
      "color"
    ],
  },
  build: {
    commonjsOptions: {
      include: [/ajv/, /axios/, /lodash/, /node_modules/, /color/],
    },
    assetsDir: "admin/dist",
  },
})
