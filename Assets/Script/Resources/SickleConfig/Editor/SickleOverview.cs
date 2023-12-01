using System.Linq;
using System.Data;
using Sirenix.Utilities;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

[GlobalConfig("Assets/Resources/ScriptableObject/EditorOverviews")]
public class SickleOverview : GlobalConfig<SickleOverview>
{
    [ReadOnly]
    [ListDrawerSettings(Expanded = true)]
    public SickleInfo[] AllInfos;

    [Button(ButtonSizes.Medium), PropertyOrder(-1)]
    public void UpdateOverview()
    {
        // Finds and assigns all scriptable objects of type Character
        this.AllInfos = AssetDatabase.FindAssets("t:SickleInfo")
            .Select(guid => AssetDatabase.LoadAssetAtPath<SickleInfo>(AssetDatabase.GUIDToAssetPath(guid)))
            .ToArray();
    }
}
