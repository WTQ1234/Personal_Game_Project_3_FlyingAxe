using System.Linq;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.Utilities;
using Sirenix.OdinInspector;

[GlobalConfig("Assets/Resources/ScriptableObject/EditorOverviews")]
public class LevelOverview : GlobalConfig<LevelOverview>
{
    [ReadOnly]
    [ListDrawerSettings(Expanded = true)]
    public LevelInfo[] AllInfos;

    [Button(ButtonSizes.Medium), PropertyOrder(-1)]
    public void UpdateOverview()
    {
        // Finds and assigns all scriptable objects of type Character
        this.AllInfos = AssetDatabase.FindAssets("t:LevelInfo")
            .Select(guid => AssetDatabase.LoadAssetAtPath<LevelInfo>(AssetDatabase.GUIDToAssetPath(guid)))
            .ToArray();
    }
}
