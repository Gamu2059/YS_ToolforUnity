using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YS_Tool.UI {
    public class BaseScene : MonoBehaviour {

        #region Field Private

        private bool m_IsInit;

        #endregion



#region Method Unity Call

        protected virtual void Start() {
            if (!m_IsInit) {
                OnInit();
                m_IsInit = true;
            }
        }

#endregion



        protected virtual void OnInit() { }

        public virtual void OnChangeEnableBefore() { }

        public virtual void OnChangeEnableAfter() { }

        public virtual void OnChangeDisableBefore() { }

        public virtual void OnChangeDisableAfter() { }
    }
}