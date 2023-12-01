#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using System.Linq;
using HRL;
using Sirenix.OdinInspector;

public class LanguageEditorWindow : BasicConfigWindow
{
    private static string mFileName_Language = "Language[{0}]";
    private static string mAssetPath_Language = "Assets/Resources/ScriptableObject/LanguageInfo";
    private static string mTitle_AllAssets_Language = "1.所有文本";

    [MenuItem("配置/多语言/文本")]
    private static void Open()
    {
        var window = GetWindow<LanguageEditorWindow>();
        // 设置标题
        GUIContent titleContent = new GUIContent();
        titleContent.text = "多语言配置";
        window.titleContent = titleContent;
        // 设置位置
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        // 添加基础配置
        if (!AssetPath.ContainsKey("属性路径"))
        {
            AssetPath.Add("默认数据名", mFileName_Language);
            AssetPath.Add("属性路径", mAssetPath_Language);
        }
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        base.BuildMenuTree();
        // 浏览当前所有属性
        LanguageOverview.Instance.UpdateOverview();
        // 将具体属性添加到列表
        if (LanguageOverview.Instance.AllInfos.Length > 0)
        {
            mCurTree.Add(mTitle_AllAssets_Language, new BasicInfoTable<LanguageInfo>(LanguageOverview.Instance.AllInfos));
            mCurTree.AddAllAssetsAtPath(mTitle_AllAssets_Language, mAssetPath_Language, typeof(LanguageInfo), true, true);
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
            if (SirenixEditorGUI.ToolbarButton(new GUIContent("创建新语言配置")))
            {
                int assetNumber = FindAssetNumber(mAssetPath_Language, mFileName_Language);
                Debug.Log(assetNumber);
                string curFileName = string.Format(mFileName_Language, assetNumber);
                ScriptableObjectCreator.ShowDialog<LanguageInfo>(mAssetPath_Language, curFileName, (obj, fileName) =>
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
