using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace YS_Tool.UI {
    [RequireComponent(typeof(RectTransform))]
    public class ScaledButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

        #region Field Private

        private RectTransform _selfRectT;

        #endregion



        #region Method Unity Call

        protected virtual void Awake() {
            _selfRectT = GetComponent<RectTransform>();
        }

        #endregion



        #region Method PointerEventHandler

        public void OnPointerDown(PointerEventData e) {
            var scale = transform.localScale;
            scale.x = 0.9f * (scale.x > 0 ? 1 : -1);
            scale.y = 0.9f * (scale.y > 0 ? 1 : -1);
            scale.z = 1;
            _selfRectT.DOScale(scale, 0.2f);
        }

        public void OnPointerUp(PointerEventData e) {
            var scale = transform.localScale;
            scale.x = scale.x > 0 ? 1 : -1;
            scale.y = scale.y > 0 ? 1 : -1;
            scale.z = 1;
            _selfRectT.DOScale(scale, 0.2f);
        }

        #endregion

    }
}
