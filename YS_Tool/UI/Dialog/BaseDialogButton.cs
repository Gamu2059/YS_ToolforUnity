using UnityEngine.UI;
using UnityEngine;

namespace YS_Tool.UI {
    public class BaseDialogButton : Button {

        #region Field Private

        private BaseDialog m_Dialog;

        private int m_Idx;

        private Text m_Title;

        #endregion



        #region Property Public

        public Text Title {
            get {
                if (!m_Title) m_Title = GetComponentInChildren<Text>();
                return m_Title;
            }
        }

        #endregion



        #region Method Unity Call

        protected override void Awake() {
            base.Awake();
            m_Title = GetComponentInChildren<Text>();
        }

        #endregion



        public void InitDialogButton(BaseDialog dialog, int idx) {
            m_Dialog = dialog;
            m_Idx = idx;
            onClick.AddListener(OnClick);
        }

        private void OnClick() {
            m_Dialog.OnClickDialogButton(m_Idx);
        }

    }
}
