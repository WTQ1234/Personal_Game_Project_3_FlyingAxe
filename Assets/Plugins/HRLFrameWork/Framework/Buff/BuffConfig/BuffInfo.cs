using System.Linq;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.Utilities;
using Sirenix.OdinInspector;

namespace HRL
{
    [SerializeField]
    public class BuffInfo : BasicInfo
    {
        public string desc;
        [Title("Attr")]
        public bool RemoveAttrWhenFinished;
        public Dictionary<string, int> dictAttr;

        [Title("����ִ�У��������")]
        public bool ExcuteAtOnce = false;

        [Title("Tick"), HideIf("ExcuteAtOnce")]
        public int TickTotal = 0;   // 0, -1 Ϊ������tick
        public float TickSecond = 0.1f;

        public BuffBase Buff;
    }

    public enum BuffTickType
    {
        NoTick,
        Once,

    }
}