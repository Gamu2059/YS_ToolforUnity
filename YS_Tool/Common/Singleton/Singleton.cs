using UnityEngine;

namespace YS_Tool.Common.Singleton {
    /// <summary>
    /// シングルトンパターンを実装するための基底クラス。
    /// </summary>
    public abstract class Singleton<T> where T : class, new() {

        #region Field Private

        private static T m_Instance;

        private static System.Object m_LockObj = new System.Object();

        #endregion



        #region Method Protected

        protected Singleton() {
#if DEBUG_ON
            Debug.AssertFormat(m_Instance == null, "Same {0} is already exist!", typeof(T));
#endif
            OnConstructor();
        }

        ~Singleton() {
            m_Instance = null;
        }

        protected virtual void OnConstructor() { }

        #endregion


        #region Method Public

        public static T GetInstance() {
            // 複数同時に生成しないようにロックする
            lock (m_LockObj) {
                if (m_Instance == null) {
                    m_Instance = new T();
                }

                return m_Instance;
            }
        }

        #endregion
    }
}
