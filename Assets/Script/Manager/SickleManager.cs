using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HRL;
using UnityEngine.UI;



public class SickleManager : MonoSingleton<SickleManager>
{
    public List<SickleInfo> dataPools;
    private Dictionary<int, SickleInfo> dict_sickleData = new Dictionary<int, SickleInfo>();

    public List<SickleItem> items;

    public int defaultSickleId = 0;
    public SickleItem default_item;
    public Transform list_parent;

    // 充能
    public float progressSpeed = 1;
    private float progress = 0;

    [Header("斧子数量")]
    public int maxNum = 3;
    public int maxExtraNum = 3;
    public List<int> dataIds;
    public List<int> dataIds_extra;

    protected override void Init()
    {
        base.Init();
        dict_sickleData = ConfigManager.Instance.GetAllInfo<SickleInfo>();
        dataIds = new List<int>();
        dataIds_extra = new List<int>();
    }

    private void Start()
    {
        for(int i = 0; i < maxNum; i++)
        {
            dataIds.Add(0);
        }
        InitSickleItemUI();
    }

    private void Update()
    {
        Check();
    }

    public SickleInfo GetSickleInfoBySickleId(int sickleId)
    {
        return dict_sickleData[sickleId];
    }

    public void OnRecycleSickle(Sickle sickle)
    {
        if (dataIds_extra.Count < maxExtraNum)
        {
            dataIds_extra.Add(sickle.sickleInfo.Id);
        }
    }

    public bool OnGetNextSickleData(out SickleInfo sickleInfo)
    {
        if (dataIds_extra.Count > 0)
        {
            int index = dataIds_extra.Count - 1;
            int sickleId = dataIds_extra[index];
            sickleInfo = GetSickleInfoBySickleId(sickleId);
            dataIds_extra.RemoveAt(index);
            return true;
        }
        if (dataIds.Count > 0)
        {
            int index = dataIds.Count - 1;
            int sickleId = dataIds[0];
            sickleInfo = dict_sickleData[sickleId];
            dataIds.RemoveAt(index);
            return true;
        }
        sickleInfo = null;
        return false;
    }

    void Check()
    {
        if (dataIds.Count < maxNum)
        {
            progress += Time.deltaTime * progressSpeed;
            if (progress > 1)
            {
                dataIds.Insert(0, defaultSickleId);
                progress = 0;
            }
        }
        RefreshSickleItemUI();
    }

    void RefreshSickleItemUI()
    {
        for (int i = 0; i < maxNum; i++)
        {
            SickleItem sickleItem = items[i];
            // 如果有data
            if (dataIds.Count > i)
            {
                SickleInfo info = GetSickleInfoBySickleId(dataIds[i]);
                sickleItem.InitData(info, false);
                sickleItem.SetFillPercent(1);
            }
            // 如果都没有
            else if (i > dataIds.Count)
            {
                // TODO: 显示下一个斧子的图片
                sickleItem.SetFillPercent(0);
            }
            // 如果都没有
            else
            {
                // TODO: 显示下一个斧子的图片
                sickleItem.SetFillPercent(progress);
            }
        }
        for (int i = 0; i < Mathf.Max(dataIds_extra.Count, items.Count - 3); i++)
        {
            int index = i + maxNum;
            if (dataIds_extra.Count > i)
            {
                SickleInfo info = GetSickleInfoBySickleId(dataIds_extra[i]);
                // 有item
                if (items.Count > index)
                {
                    SickleItem item = items[index];
                    item.InitData(info, true);
                    continue;
                }
                else
                {
                    SickleItem item = Instantiate<SickleItem>(info.sickleItem, list_parent);
                    item.InitData(info, true);
                    items.Add(item);
                }
            }
            if (items.Count > index)
            {
                SickleItem item = items[index];
                if (dataIds_extra.Count > i)
                {
                    SickleInfo info = GetSickleInfoBySickleId(dataIds_extra[i]);
                    item.InitData(info, true);
                    continue;
                }
                else
                {
                    items.Remove(item);
                    Destroy(item.gameObject);
                }
            }
        }
    }

    void InitSickleItemUI()
    {
        for (int i = 0; i < dataIds.Count; i++)
        {
            int defaultId = 0;
            SickleInfo sickleInfo = GetSickleInfoBySickleId(defaultId);
            SickleItem item = Instantiate<SickleItem>(sickleInfo.sickleItem, list_parent);
            item.InitData(dict_sickleData[dataIds[i]], false);
            items.Add(item);
        }
    }
}
