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
    [Title("升到此级时增加的生命值上限")]
    public int addHp = 0;
}
