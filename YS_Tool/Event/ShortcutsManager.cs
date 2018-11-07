using UnityEngine;
using YS_Tool.Common.Singleton;

namespace YS_Tool.Event {
    public class ShortcutsManager : LocalSingletonMonoBehavior<ShortcutsManager> {
        [SerializeField]
        private MultiKeyPush m_MultiKeyPusher;

        public MultiKeyPush MultiKeyPusher {
            get { return m_MultiKeyPusher; }
        }
    }
}
