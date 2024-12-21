import tseslint from "typescript-eslint";
import pluginVue from "eslint-plugin-vue";

export default tseslint.config(
  // eslint.configs.recommended,
  ...tseslint.configs.recommended,
  ...pluginVue.configs["flat/essential"],
  {
    plugins: {
      'typescript-eslint': tseslint.plugin,
    },
    languageOptions: {
      parserOptions: {
        parser: tseslint.parser,
        project: './tsconfig.app.json',
        projectService: true,
        extraFileExtensions: ['.vue'],
        sourceType: 'module',
      },
    },
  },
  {
    ignores: [
      'node_modules',
      'dist',
      'build',
      'coverage',
      'public',
      'eslint.config.js',
      'tsconfig.json',
      'vite.config.ts',
      'tsconfig.app.json',
      'tsconfig.node.json',
    ],
  },
  {
    rules: {
      "@typescript-eslint/no-explicit-any": "off",
      "@typescript-eslint/no-empty-object-type": "off",
      "@typescript-eslint/no-unsafe-function-type": "off",
      "@typescript-eslint/consistent-type-imports": "error",
      "curly": ["error", "all"],
      "vue/html-indent": ["error", 2, {
        "baseIndent": 1,
        "closeBracket": 0,
        "attribute": 1,
        "alignAttributesVertically": true,
        "ignores": []
      }],
      "vue/script-indent": ["error", 2, {
        "baseIndent": 0,
        "switchCase": 1,
        "ignores": []
      }],
      "vue/require-v-for-key": "off",
      "vue/valid-v-slot": "off",
      "vue/no-dupe-keys": "off",
      "vue/return-in-computed-property": "off",
      "vue/multi-word-component-names": 'off',
      "vue/first-attribute-linebreak": ["error", {
        "singleline": "below",
        "multiline": "below"
      }],
      "vue/max-attributes-per-line": ["error", {
        "singleline": {
          "max": 1
        },
        "multiline": {
          "max": 1
        }
      }],
      "vue/html-closing-bracket-newline": ["error", {
        "singleline": "never",
        "multiline": "always",
        "selfClosingTag": {
          "singleline": "never",
          "multiline": "always"
        }
      }]
    },
  }
);