using System.Linq;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.Utilities;
using Sirenix.OdinInspector;
using HRL;

[SerializeField]
public class LevelInfo : BasicInfo
{
    public int maxExp = 10;
    [Title("�����˼�ʱ���ӵ�����ֵ����")]
    public int addHp = 0;
}
