import os

def replace_in_files(root_dir, old_text, new_text):
    # Modifier le contenu des fichiers
    for foldername, subfolders, filenames in os.walk(root_dir, topdown=False):  # topdown=False pour parcourir les dossiers enfants avant les parents
        for filename in filenames:
            filepath = os.path.join(foldername, filename)
            
            with open(filepath, 'r', encoding='utf-8', errors='ignore') as file:
                content = file.read()
            
            content = content.replace(old_text, new_text)
            
            with open(filepath, 'w', encoding='utf-8', errors='ignore') as file:
                file.write(content)

def rename_files(root_dir, old_text, new_text):
    # Renommer les fichiers
    for foldername, subfolders, filenames in os.walk(root_dir, topdown=False):  # topdown=False pour parcourir les dossiers enfants avant les parents
        for filename in filenames:
            if old_text in filename:
                src = os.path.join(foldername, filename)
                dst = os.path.join(foldername, filename.replace(old_text, new_text))
                os.rename(src, dst)

def rename_folders(root_dir, old_text, new_text):
    for foldername, subfolders, filenames in os.walk(root_dir, topdown=False):
        last_folder = os.path.basename(foldername)
        if old_text in last_folder:
            new_foldername = foldername.replace(old_text, new_text)
            os.rename(foldername, new_foldername)

def main():
    root_dir = "tests"
    old_text = "Template"
    new_text = "Extension"
    
    replace_in_files(root_dir, old_text, new_text)
    rename_files(root_dir, old_text, new_text)
    rename_folders(root_dir, old_text, new_text)
    print("Remplacement terminé!")

if __name__ == "__main__":
    main()