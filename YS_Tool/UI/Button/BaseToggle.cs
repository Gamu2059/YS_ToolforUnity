using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YS_Tool.Common.Utility;

namespace YS_Tool.UI {
    public class BaseToggle : MonoBehaviour {

        #region Field Inspector

        [SerializeField]
        private bool m_IsEnabled;

        [SerializeField]
        private bool m_InitEnabled;

        [SerializeField]
        private Sprite m_EnabledSprite;

        [SerializeField]
        private Sprite m_DisabledSprite;

        [SerializeField]
        private Toggle.ToggleEvent m_OnValueChanged = new Toggle.ToggleEvent();

        #endregion



        #region Field Private

        private Button _selfButton;

        private Image _selfImage;

        #endregion



        #region Property Public

        public bool Enabled {
            get { return m_IsEnabled; }
        }

        public Toggle.ToggleEvent OnValueChanged {
            get { return m_OnValueChanged; }
        }

        #endregion



        #region Method Unity Call

        private void Awake() {
            _selfButton = GetComponent<Button>();
            _selfImage = GetComponent<Image>();

            _selfButton.onClick.AddListener(OnClick);
            SetEnabledWithoutEvent(m_InitEnabled);
        }

        #endregion



        #region Method Protected

        protected virtual void OnClick() {
            SetEnabled(!Enabled);
        }

        #endregion



        #region Method Public

        /// <summary>
        /// 指定した状態へと切り替えます。
        /// 状態が切り替わった場合、状態変化をイベントとして発行します。
        /// ただし、直前と同じ状態を指定した場合は、変化せず、イベントを発行しません。
        /// </summary>
        public void SetEnabled(bool isEnabled) {
            if (m_IsEnabled == isEnabled) return;

            SetEnabledWithoutEvent(isEnabled);
            EventUtility.SafeInvokeUnityEvent(m_OnValueChanged, m_IsEnabled);
        }

        /// <summary>
        /// 指定した状態へと切り替えます。
        /// ただし、状態変化をイベントとして発行しません。
        /// </summary>
        public void SetEnabledWithoutEvent(bool isEnabled) {
            if (m_IsEnabled == isEnabled) return;

            m_IsEnabled = isEnabled;
            _selfImage.sprite = m_IsEnabled ? m_EnabledSprite : m_DisabledSprite;
        }

        #endregion

    }
}
