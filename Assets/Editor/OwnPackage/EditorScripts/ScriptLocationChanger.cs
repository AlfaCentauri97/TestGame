using UnityEditor;
using UnityEngine;

public class ScriptLocationChanger : Editor
{
    [MenuItem("GameObject/Create Custom C# Script", false, 10)]
    static void CreateScript()
    {
        string folderPath = "Assets/Scripts";
        
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "Scripts");
        }
        
        string defaultName = "NewScript";
        string path = AssetDatabase.GenerateUniqueAssetPath($"{folderPath}/{defaultName}.cs");

        string template = "using UnityEngine;\n\npublic class " + defaultName + " : MonoBehaviour\n{\n\tvoid Start() {}\n\tvoid Update() {}\n}";

        System.IO.File.WriteAllText(path, template);

        AssetDatabase.Refresh();

        Object asset = AssetDatabase.LoadAssetAtPath<Object>(path);
        Selection.activeObject = asset;
    }
}