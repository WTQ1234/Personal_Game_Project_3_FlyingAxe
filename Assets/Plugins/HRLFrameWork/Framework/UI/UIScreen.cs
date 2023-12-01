using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace HRL
{
    [System.Serializable]
    public class UIScreenInfo
    {
        public bool mIsFull = true;                 // �Ƿ�ȫ������ȫ��������һ����ɫ��͸������
        public bool mIsConst = false;               // �Ƿ��ڴ��ڣ����Ƴ����н���ʱ����
        public bool mIsUnique = true;               // �Ƿ��һ�޶�������ʱ��Ⲣ����ͬ������
        public bool mHideHudUI = true;              // ����������UI
        public bool mIsRenderWorld = false;         // �Ƿ���Ⱦ����
        public bool mHideLastDialog = true;         // �Ƴ���һ������
        public bool mTopDialog = false;             // �ڶ���Canvas
        //public bool mRemoveOtherDialog = true;      // �Ƴ��������н���
        [HideInInspector]
        public bool mBtnRemove = true;              // ����ȫ������������ť�Ƴ�����
        [HideInInspector]
        public float mAlpha = 0.5f;                 // ����ȫ�������һ����ɫ��������͸����ֵ
        [HideInInspector]
        public int mPriority = 1;                   // ��ʾ���ȼ�
    }

    public class UIScreen : MonoBehaviour
    {
        public string UIName => this.GetType().Name;

        [BoxGroup("Basic Screen Info")]
        [SerializeField] UIScreenInfo UIScreenInfo = new UIScreenInfo();

        [BoxGroup("Basic Comp Allow Null")]
        [SerializeField] UI_Comp_Mask comp_Mask;

        #region LifeTime
        public void Awake()
        {
            Init();
        }

        protected void Remove()
        {
            UIManager.Instance.PopScreen(this);
        }

        public virtual void OnShown()
        {
            Debug.Log(this);
            gameObject.SetActive(true);
        }

        public virtual void OnHide()
        {
            gameObject.SetActive(false);
        }
        #endregion

        #region Init
        // Init ֻ����Awake��ʱ��ִ��һ��
        protected virtual void Init()
        {
            Debug.Log($"Init UIScreen {UIName}");
            InitUIScreenInfo();
            if (comp_Mask != null)
            {
                comp_Mask.Init(this);
            }
        }
        #endregion

        #region ScreenInfo

        protected virtual void InitUIScreenInfo()
        {

        }

        public UIScreenInfo GetScreenInfo()
        {
            return UIScreenInfo;
        }
        #endregion

        #region Animation
        protected void DoShowAnimation()
        {

        }

        protected void DoHideAnimation()
        {

        }
        #endregion

        #region SubComp
        public void OnClick_Mask()
        {
            Remove();
        }
        #endregion

        #region Event
        protected void AddListener(string messengeEventType, System.Delegate handler)
        {
            Messenger.Instance.AddListener(messengeEventType, handler);
        }

        protected void RemoveListener(string messengeEventType, System.Delegate handler)
        {
            Messenger.Instance.RemoveListener(messengeEventType, handler);
        }
        #endregion
    }
}
