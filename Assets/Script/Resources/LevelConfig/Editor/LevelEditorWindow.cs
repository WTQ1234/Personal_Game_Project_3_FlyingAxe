#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using System.Linq;
using HRL;
using Sirenix.OdinInspector;

public class LevelEditorWindow : BasicConfigWindow
{
    private static string mFileName_Level = "Level[{0}]";
    private static string mAssetPath_Level = "Assets/Resources/ScriptableObject/LevelInfo";
    private static string mTitle_AllAssets_Level = "1.所有等级";

    [MenuItem("配置/主流程/等级")]
    private static void Open()
    {
        var window = GetWindow<LevelEditorWindow>();
        // 设置标题
        GUIContent titleContent = new GUIContent();
        titleContent.text = "等级配置";
        window.titleContent = titleContent;
        // 设置位置
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        // 添加基础配置
        if (!AssetPath.ContainsKey("属性路径"))
        {
            AssetPath.Add("默认数据名", mFileName_Level);
            AssetPath.Add("属性路径", mAssetPath_Level);
        }
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        base.BuildMenuTree();
        // 浏览当前所有属性
        LevelOverview.Instance.UpdateOverview();
        // 将具体属性添加到列表
        if (LevelOverview.Instance.AllInfos.Length > 0)
        {
            mCurTree.Add(mTitle_AllAssets_Level, new BasicInfoTable<LevelInfo>(LevelOverview.Instance.AllInfos));
            mCurTree.AddAllAssetsAtPath(mTitle_AllAssets_Level, mAssetPath_Level, typeof(LevelInfo), true, true);
        }
        // 后续处理
        AfterCreateBuildMenuTree();
        return mCurTree;
    }

    protected override void OnBeginDrawEditors()
    {
        if (this.MenuTree == null)
        {
            return;
        }
        var selected = this.MenuTree?.Selection?.FirstOrDefault();
        var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;
        // 绘制工具栏
        SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
        {
            if (selected != null)
            {
                GUILayout.Label(selected.Name);
            }
            if (SirenixEditorGUI.ToolbarButton(new GUIContent("选中当前文件")))
            {
                SelectCurAssetFile();
            }
            if (SirenixEditorGUI.ToolbarButton(new GUIContent("创建新等级配置")))
            {
                int assetNumber = FindAssetNumber(mAssetPath_Level, mFileName_Level);
                string curFileName = string.Format(mFileName_Level, assetNumber);
                ScriptableObjectCreator.ShowDialog<LevelInfo>(mAssetPath_Level, curFileName, (obj, fileName) =>
                {
                    obj.Id = assetNumber;
                    obj.Name = obj.name;
                    obj.FileName = fileName;
                    obj.InitAfterCreateFile();
                    base.TrySelectMenuItemWithObject(obj);
                });
            }
        }
        SirenixEditorGUI.EndHorizontalToolbar();
    }
}
#endif
