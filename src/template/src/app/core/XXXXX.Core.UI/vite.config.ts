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
    FoundationSharedAutoImport()
  ],
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
      "@lexical/clipboard", 
      "@lexical/history", 
      "@lexical/link",
      "@lexical/plain-text",
      "@lexical/rich-text",
      "@lexical/selection", 
      "@lexical/text", 
      "@lexical/utils",
      "@novnc/novnc",
      "lexical",
      "axios",
      "lodash",
      "color",
      "ajv"
    ],
  },
  build: {
    commonjsOptions: {
      include: [/ajv/, /axios/, /lodash/, /node_modules/, /color/],
    },
    assetsDir: "dist",
  },
})
