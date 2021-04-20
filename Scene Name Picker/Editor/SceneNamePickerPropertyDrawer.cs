using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomPropertyDrawer(typeof(SceneNamePicker))]
public class SceneNamePickerPropertyDrawer : PropertyDrawer
{
    private int selectedIndex;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property.serializedObject.Update();

        SceneNamePicker picker = (SceneNamePicker)attribute;
        string currentSceneName = property.stringValue;

        string[] validSceneNames = GetValidSceneNames(picker.showPath);

        if (IsValidSceneName(currentSceneName, picker.showPath))
        {
            selectedIndex = validSceneNames.ToList().IndexOf(currentSceneName);
        }

        selectedIndex = EditorGUI.Popup(position, property.displayName, selectedIndex, validSceneNames);
        property.stringValue = validSceneNames[selectedIndex];

        property.serializedObject.ApplyModifiedProperties();
    }

private string[] GetValidSceneNames(bool includePath)
{
    EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
    string[] sceneNames = EditorBuildSettingsScene.GetActiveSceneList(scenes);

    for (int i = 0; i < sceneNames.Length; i++)
    {
        if (includePath)
        {
            sceneNames[i] = sceneNames[i].Replace("Assets/", "");
            sceneNames[i] = sceneNames[i].Replace(".unity", "");
        }
        else
        {
            sceneNames[i] = AssetDatabase.LoadAssetAtPath<SceneAsset>(sceneNames[i]).name;
        }
    }

    return sceneNames;
}

    private bool IsValidSceneName(string sceneName, bool includePath)
    {
        string[] sceneNames = GetValidSceneNames(includePath);

        for (int i = 0; i < sceneNames.Length; i++)
        {
            if (sceneName == sceneNames[i])
            {
                return true;
            }
        }

        return false;
    }
}