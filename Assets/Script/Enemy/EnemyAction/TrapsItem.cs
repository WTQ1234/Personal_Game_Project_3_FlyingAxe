
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Traps
{
    /// <summary>
    /// 可放置物品父类
    /// </summary>
    public class TrapsItem : MonoBehaviour
    {
        public GameObject body;
        private BoxCollider2D boxCollider2D;
        private bool currentTrggerState;
        public bool isPlace;
        public bool isLock;

        protected virtual void Awake()
        {
            //boxCollider2D = GetComponent<BoxCollider2D>();
            //currentTrggerState = boxCollider2D.isTrigger;
        }
    }
}