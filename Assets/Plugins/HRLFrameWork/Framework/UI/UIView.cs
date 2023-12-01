using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HRL
{
    public class UIViewInfo
{
    public bool mIsFull = true;                 // �Ƿ�ȫ������ȫ��������һ����ɫ��͸������
    public bool mIsConst = false;               // �Ƿ��ڴ��ڣ����Ƴ����н���ʱ����
    public bool mIsUnique = true;               // �Ƿ��һ�޶�������ʱ��Ⲣ����ͬ������
    public bool mHideHudUI = true;              // ����������UI
    public bool mIsRenderWorld = false;         // �Ƿ���Ⱦ����
    public bool mRemoveOtherDialog = true;      // �Ƴ��������н���
    public bool mBtnRemove = true;              // ����ȫ������������ť�Ƴ�����
    public float mAlpha = 0.5f;                 // ����ȫ�������һ����ɫ��������͸����ֵ
    public int mPriority = 1;                   // ��ʾ���ȼ�
}

    public class UIView : MonoBehaviour
    {
        public string UIName => this.GetType().Name;

        #region LifeTime
        public void Awake()
        {
            Init();
        }

        protected void Remove()
        {
            //UIManager.Instance.PopScreen(this);
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
        }
        #endregion

        #region ScreenInfo
        protected UIScreenInfo UIScreenInfo;
        protected virtual void InitUIScreenInfo()
        {
            UIScreenInfo = new UIScreenInfo();
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
    }
}