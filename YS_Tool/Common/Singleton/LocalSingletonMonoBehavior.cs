using UnityEngine;

namespace YS_Tool.Common.Singleton {
    public class LocalSingletonMonoBehavior<T> : SingletonMonoBehavior<T> where T : MonoBehaviour {
        protected override void OnAwake() {
            GameObject obj = LocalHolder.GetHolder();
            if (obj) {
                transform.SetParent(obj.transform);
            }
        }
    }
}