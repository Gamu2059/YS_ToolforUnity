using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using YS_Tool.Common.Utility;

namespace YS_Tool.Event {
	/// <summary>
	/// 長押しを検出するハンドラコンポーネントです。
	/// 元URL : http://fantom1x.blog130.fc2.com/blog-entry-251.html
	/// </summary>
	public class LongClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler {
		[SerializeField] private float _validTime = 1f;

		public float ValidTime {
			get { return _validTime; }
			set { _validTime = value; }
		}

		[SerializeField] private UnityEvent _onLongClick = new UnityEvent();

		public UnityEvent OnLongClick {
			get { return _onLongClick; }
		}

		private float _requiredTime;
		private bool _isPressing = false;

		private void Update() {
			if (!_isPressing) return;
			if (Time.time >= _requiredTime) {
				EventUtility.SafeInvokeUnityEvent(_onLongClick);
				_isPressing = false;
			}
		}

		public void OnPointerDown(PointerEventData e) {
			if (!_isPressing) {
				_isPressing = true;
				_requiredTime = Time.time + _validTime;
			} else {
				_isPressing = false;
			}
		}

		public void OnPointerUp(PointerEventData e) {
			_isPressing = false;
		}

		public void OnPointerExit(PointerEventData e) {
			_isPressing = false;
		}
	}
}