export function extractParams(path: string, pattern: string): Record<string, string> | null {
  // Remplacer les paramètres nommés dans le schéma par des groupes de capture d'expression régulière
  const regexPattern = pattern.replace(/:([a-zA-Z0-9_]+)/g, '(?<$1>[^/]+)');
  const regex = new RegExp(`^.*${regexPattern}.*$`);
  
  // Essayer de faire correspondre l'URL au schéma
  const match = path.match(regex);

  if (match) {
      // Retourner les paramètres nommés trouvés
      return match.groups as Record<string, string>;
  } else {
      return null;
  }
}