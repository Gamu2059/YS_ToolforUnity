using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using YS_Tool.Common.Utility;

namespace YS_Tool.UI {
    public abstract class BaseDialog : MonoBehaviour {

        #region Field Inspector

        [SerializeField]
        private Button m_CloseButton;

        [SerializeField]
        private BaseDialogButton[] m_DialogButtons;

        #endregion



        #region Field Private

        private Action<int> m_Result;

        private bool m_IsShowing = false;

        #endregion



        #region Property Public

        public Button CloseButton {
            get { return m_CloseButton; }
        }

        public BaseDialogButton[] DialogButtons {
            get { return m_DialogButtons; }
        }

        public bool IsShowing {
            get { return m_IsShowing; }
        }

        #endregion



        #region Method Unity Call

        protected virtual void Start() {
            m_CloseButton.onClick.AddListener(OnClickClose);
            for (int i = 0; i < m_DialogButtons.Length; i++) {
                m_DialogButtons[i].InitDialogButton(this, i);
            }
        }

        #endregion



        #region Method Protected

        /// <summary>
        /// クローズボタンを押された時は、 -1 を結果として返す。
        /// </summary>
        protected virtual void OnClickClose() {
            EventUtility.SafeInvokeAction(m_Result, -1);
            CloseDialogAuto();
        }

        protected abstract void CloseDialogAuto();

        protected virtual void OnShow(Action onComplete) {
            EventUtility.SafeInvokeAction(onComplete);
        }

        protected virtual void OnHide(Action onComplete) {
            EventUtility.SafeInvokeAction(onComplete);
        }

        #endregion



        #region Method Public

        public void Show(Action onComplete, Action<int> result = null) {
            if (result != null) {
                m_Result = result;
            }
            OnShow(() => {
                m_IsShowing = true;
                EventUtility.SafeInvokeAction(onComplete);
            });
        }

        public void Hide(Action onComplete) {
            OnHide(() => {
                EventUtility.SafeInvokeAction(onComplete);
                m_IsShowing = false;
            });
        }

        public void OnClickDialogButton(int idx) {
            EventUtility.SafeInvokeAction(m_Result, idx);
        }

        #endregion

    }
}
