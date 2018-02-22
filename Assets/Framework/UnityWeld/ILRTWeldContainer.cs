using Framework.Hotfix;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityWeld.Binding;

namespace UnityWeld.Binding.ILRT
{
    public class ILRTWeldContainer : MonoBehaviour, IViewModelProvider
    {
        private object          mViewModel          = null;
        [HideInInspector][SerializeField]
        private string          mViewModelTypeName  = string.Empty;

        private HotfixObject    mMBHotfixObj;
        public  HotfixObject    MBHotfixObject      { get { return this.mMBHotfixObj; } }

        [HideInInspector][SerializeField]
        private string          mHotfixName;
        public  string          HotfixName          { get { return mHotfixName; } set { mHotfixName = value; } }

        [HideInInspector][SerializeField]
        private bool            mNeedUpdate;
        public bool             NeedUpdate          { get { return mNeedUpdate; } set { mNeedUpdate = value; } }

        private string          mParentType         = "WindHotfix.Core.THotfixMB`1<{0}>";

        public object GetViewModel()
        {
            return this.mViewModel;
        }

        public string GetViewModelTypeName()
        {
            return this.mHotfixName;
        }

        public string ViewModelTypeName
        {
            get { return this.mViewModelTypeName;  }
            set { this.mViewModelTypeName = value; }
        }

        protected virtual void Awake()
        {
            this.InitHotfixMB();
        }

        protected virtual void Start()
        {
            if (mMBHotfixObj == null) return;
            mMBHotfixObj.InvokeParent(this.mParentType, "Start_Proxy");
        }

        protected virtual void Update()
        {
            if (!mNeedUpdate) return;

            if (mMBHotfixObj == null) return;
            mMBHotfixObj.InvokeParent(this.mParentType, "Update_Proxy");
        }

        protected virtual void OnDestroy()
        {
            if (mMBHotfixObj != null)
                mMBHotfixObj.InvokeParent(this.mParentType, "OnDestroy_Proxy");
            
            mMBHotfixObj = null;
        }

        protected virtual void OnEnable()
        {
            if (mMBHotfixObj == null) return;
            mMBHotfixObj.InvokeParent(this.mParentType, "OnEnable_Proxy");
        }

        protected virtual void OnDisable()
        {
            if (mMBHotfixObj == null) return;
            mMBHotfixObj.InvokeParent(this.mParentType, "OnDisable_Proxy");
        }

        protected void InitHotfixMB()
        {
            if (mMBHotfixObj == null && !string.IsNullOrEmpty(mHotfixName))
            {
                this.mParentType = string.Format(this.mParentType, this.mHotfixName);
                this.mMBHotfixObj = HotfixManager.Instance.Instantiate(this.mHotfixName);
                this.mMBHotfixObj.InvokeParent(this.mParentType, "Awake_Proxy", this.gameObject);
            }
        }

        public void Initialize(string rHotfixName, bool bNeedUpdate = false)
        {
            this.mHotfixName = rHotfixName;
            this.mNeedUpdate = bNeedUpdate;
            this.Awake();
        }
    }
}
