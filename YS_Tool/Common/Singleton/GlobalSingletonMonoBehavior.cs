using UnityEngine;

namespace YS_Tool.Common.Singleton {
    public class GlobalSingletonMonoBehavior<T> : SingletonMonoBehavior<T> where T : MonoBehaviour {
        protected override void OnAwake() {
            DontDestroyOnLoad(gameObject);

            GameObject obj = GlobalHolder.GetHolder();
            if (obj) {
                transform.SetParent(obj.transform);
            }
        }
    }
}
