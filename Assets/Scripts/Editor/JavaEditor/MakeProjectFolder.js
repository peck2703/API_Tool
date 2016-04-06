/*#pragma strict
//Generate Folders in our project
import System.IO;

//Add a menu item 
//Generate Folders and names from script
@MenuItem("Project Tools/Make Folders")
static function MakeFolder()
{
    GenerateFolders();
}

static function GenerateFolders()
{
    var projectPath : String = Application.dataPath + "/";
    Directory.CreateDirectory(projectPath+ "Audio");
    Directory.CreateDirectory(projectPath+ "Characters");
    Directory.CreateDirectory(projectPath+ "Fonts");
    Directory.CreateDirectory(projectPath+ "Materials");
    Directory.CreateDirectory(projectPath+ "Meshes");
    Directory.CreateDirectory(projectPath+ "Prefabs");
    Directory.CreateDirectory(projectPath+ "Resources");
    Directory.CreateDirectory(projectPath+ "Scenes");
    Directory.CreateDirectory(projectPath+ "Scripts");
    Directory.CreateDirectory(projectPath+ "Shaders");
    Directory.CreateDirectory(projectPath+ "Sprites");
    Directory.CreateDirectory(projectPath+ "Textures");

    AssetDatabase.Refresh();
}*/