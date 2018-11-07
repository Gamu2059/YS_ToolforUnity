using UnityEngine;

namespace YS_Tool.Common.Singleton {
    public class LocalHolder : MonoBehaviour {

        #region Field Private

        private static GameObject m_Holder;

        #endregion



        #region Method Unity Call

        private void Awake() {
            if (CheckExistHolder()) {
#if DEBUG_ON
                Debug.LogFormat("<color=red>{0} is already exist!</color>", typeof(LocalHolder).Name);
				Debug.LogFormat("<color=red>    And Destroy object where the name is {0}</color>", gameObject.name);
#endif
                Destroy(gameObject);
            } else {
#if DEBUG_ON
                Debug.LogFormat("<color=blue>{0} is spawn!</color>", typeof(LocalHolder).Name);
#endif
                InitHolder();
            }
        }

        private void OnDestroy() {
#if DEBUG_ON
            Debug.LogFormat("<color=blue>{0} is destroyed!</color>", typeof(LocalHolder).Name);
#endif
        }

        #endregion



        #region Method Public

        public static GameObject GetHolder() {
            if (!CheckExistHolder()) {
                GameObject obj = new GameObject(typeof(LocalHolder).Name);
                obj.AddComponent<LocalHolder>();
                m_Holder = obj;
            }
            return m_Holder;
        }

        public static bool CheckExistHolder() {
            return m_Holder;
        }

        #endregion



        #region Method Private

        private void InitHolder() {
            gameObject.name = typeof(LocalHolder).Name;
            m_Holder = gameObject;
        }

        #endregion

    }
}
