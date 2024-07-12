// Plugins
import vue from '@vitejs/plugin-vue'
import vuetify, { transformAssetUrls } from 'vite-plugin-vuetify'
import FoundationSharedAutoImport from "@dative-gpi/foundation-shared-loader"

// Utilities
import { defineConfig } from 'vite'
import { fileURLToPath, URL } from 'node:url'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue({
      template: { transformAssetUrls }
    }),
    // https://github.com/vuetifyjs/vuetify-loader/tree/next/packages/vite-plugin
    vuetify({
      autoImport: true,
    }),
    FoundationSharedAutoImport()
  ],
  define: { 'process.env': {} },
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    },
    extensions: [
      '.js',
      '.json',
      '.jsx',
      '.mjs',
      '.ts',
      '.tsx',
      '.vue',
    ],
  },
  server: {
    port: 3000,
  },
  optimizeDeps: {
    include: ["ajv", "axios", "lodash", "color", "@lexical/selection"],
  },
  build: {
    commonjsOptions: {
      include: [/ajv/, /axios/, /lodash/, /node_modules/, /color/],
    },
    assetsDir: "dist",
  },
})
