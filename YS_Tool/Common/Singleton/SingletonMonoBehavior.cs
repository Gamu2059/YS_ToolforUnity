using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YS_Tool.Common.Singleton {
    /// <summary>
    /// シングルトンパターンを実装したMonoBehaviorの基底クラス。
    /// ただし、これを直接継承しないで下さい。
    /// </summary>
    public abstract class SingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour {

        #region Field Privte

        private static T m_Instance;

        #endregion



        #region Method Unity Call

        protected virtual void Awake() {
            if (CheckExistInstance()) {
#if DEBUG_ON
                Debug.LogFormat("<color=red>Same {0} is already exist!</color>", typeof(T));
				Debug.LogFormat("<color=red>    And Destroy object where the name is {0}</color>", gameObject.name);
#endif
                Destroy(gameObject);
            } else {
#if DEBUG_ON
                Debug.LogFormat("<color=blue>{0} is spawn!</color>", typeof(T));
#endif
                SetInstance(gameObject);
                OnAwake();
            }
        }

        protected virtual void OnDestroy() {
#if DEBUG_ON
            Debug.LogFormat("<color=blue>{0} is destroyed!</color>", typeof(T));
#endif
        }

        #endregion



        #region Method Public

        public static T GetInstance() {
            if (!CheckExistInstance()) {
                GameObject obj = new GameObject();
                m_Instance = obj.AddComponent<T>();
            }
            return m_Instance;
        }

        public static bool CheckExistInstance() {
            return m_Instance;
        }

        #endregion



        #region Method Private

        private void SetInstance(GameObject obj) {
            m_Instance = obj.GetComponent<T>();
            gameObject.name = typeof(T).Name;
        }

        #endregion



        #region Method Abstract

        protected abstract void OnAwake();

        #endregion
    }
}