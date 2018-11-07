using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using YS_Tool.Common.Singleton;
using YS_Tool.Common.Utility;

namespace YS_Tool.UI {
    public class BaseDialogManager<T> : LocalSingletonMonoBehavior<T> where T : MonoBehaviour {

        protected virtual void OnBeforeShowDialog(BaseDialog dialog, Action onComplete) {
            EventUtility.SafeInvokeAction(onComplete);
        }

        protected virtual void OnAfterShowDialog(BaseDialog dialog, Action onComplete) {
            EventUtility.SafeInvokeAction(onComplete);
        }

        protected virtual void OnBeforeHideDialog(BaseDialog dialog, Action onComplete) {
            EventUtility.SafeInvokeAction(onComplete);
        }

        protected virtual void OnAfterHideDialog(BaseDialog dialog, Action onComplete) {
            EventUtility.SafeInvokeAction(onComplete);
        }



        #region Method Public

        public void ShowDialog(BaseDialog dialog, Action onComplete = null, Action<int> result = null) {
            if (!dialog) return;
            if (dialog.IsShowing) return;

            OnBeforeShowDialog(dialog, () => {
                dialog.Show(() => {
                    OnAfterShowDialog(dialog, onComplete);
                }, result);
            });
        }

        public void HideDialog(BaseDialog dialog, Action onComplete = null) {
            if (!dialog) return;
            if (!dialog.IsShowing) return;

            OnBeforeHideDialog(dialog, () => {
                dialog.Hide(() => {
                    OnAfterHideDialog(dialog, onComplete);
                });
            });
        }

        #endregion

    }
}

