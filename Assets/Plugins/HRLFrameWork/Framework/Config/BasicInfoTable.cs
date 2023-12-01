#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Sirenix.OdinInspector;

namespace HRL
{
    public class BasicInfoTable<T> where T : BasicInfo
    {
        [FormerlySerializedAs("allInfos")]  // �˴������޸����֣������ᶪʧ֮ǰ�������õ���Ϣ
        [TableList(IsReadOnly = true, AlwaysExpanded = true), ShowInInspector]
        private readonly List<InfoWrapper> allInfos;

        public T this[int index]
        {
            get { return this.allInfos[index].Info; }
        }

        public BasicInfoTable(IEnumerable<T> chapters)
        {
            this.allInfos = chapters.Select(x => new InfoWrapper(x)).ToList();
        }

        private class InfoWrapper
        {
            private T info;

            public T Info
            {
                get { return this.info; }
            }

            public InfoWrapper(T _info)
            {
                this.info = _info;
            }
        }
    }
}
#endif