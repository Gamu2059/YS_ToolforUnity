using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YS_Tool.Common.Singleton;
using YS_Tool.Common.Utility;

namespace YS_Tool.UI {
    public class BaseSceneManager<T> : LocalSingletonMonoBehavior<T> where T : MonoBehaviour {

        #region Field Inspector

        [SerializeField]
        private BaseScene[] m_Scenes;

        [SerializeField]
        private int m_StartSceneIdx;

        [SerializeField]
        private BaseScene m_CurrentScene;

        #endregion



        #region Method Unity Call

        protected virtual void Start() {
            InitScenes();
        }

        #endregion



        #region Method Private

        private void InitScenes() {
            if (m_Scenes == null) return;

            // 一度全てのシーンを無効化
            foreach (var scene in m_Scenes) {
                if (!scene) continue;
                scene.OnChangeDisableBefore();
                DisableScene(scene);
                scene.OnChangeDisableAfter();
            }

            // 初期シーンに選ばれたシーンを有効化
            if (ArrayUtility.IsOutOfArray(m_Scenes, m_StartSceneIdx)) return;
            var startScene = m_Scenes[m_StartSceneIdx];
            startScene.OnChangeEnableBefore();
            EnableScene(startScene);
            startScene.OnChangeEnableAfter();

            m_CurrentScene = startScene;
        }

        private int GetSceneIdx<U>() where U : BaseScene {
            if (m_Scenes == null) return -1;

            for (int i = 0; i < m_Scenes.Length; i++) {
                if (m_Scenes[i].GetType() == typeof(U)) return i;
            }

            return -1;
        }

        #endregion



        #region Method Protected

        protected virtual void OnChangeSceneBefore(BaseScene current, BaseScene next, Action onComplete) {
            EventUtility.SafeInvokeAction(onComplete);
        }

        protected virtual void OnChangeScene(BaseScene current, BaseScene next, Action onComplete) {
            EventUtility.SafeInvokeAction(onComplete);
        }

        protected virtual void OnChangeSceneAfter(BaseScene current, BaseScene next, Action onComplete) {
            EventUtility.SafeInvokeAction(onComplete);
        }

        protected virtual void EnableScene(BaseScene scene) {
            if (scene) scene.gameObject.SetActive(true);
        }

        protected virtual void DisableScene(BaseScene scene) {
            if (scene) scene.gameObject.SetActive(false);
        }

        #endregion



#region Method Public

        public void ChangeScene<U>(Action onComplete = null) where U : BaseScene {
            if (m_Scenes == null) return;

            var nextIdx = GetSceneIdx<U>();
            if (ArrayUtility.IsOutOfArray(m_Scenes, nextIdx)) return;

            BaseScene next = m_Scenes[nextIdx];

            // シーン切替前の処理
            OnChangeSceneBefore(m_CurrentScene, next, () => {
                if (m_CurrentScene) m_CurrentScene.OnChangeDisableBefore();
                next.OnChangeEnableBefore();

                // シーン切替中の処理
                OnChangeScene(m_CurrentScene, next, () => {
                    if (m_CurrentScene) m_CurrentScene.OnChangeDisableAfter();
                    next.OnChangeEnableAfter();

                    // シーン切替後の処理
                    OnChangeSceneAfter(m_CurrentScene, next, () => {
                        m_CurrentScene = next;

                        EventUtility.SafeInvokeAction(onComplete);
                    });
                });
            });
        }

        public U GetScene<U>() where U : BaseScene {
            if (m_Scenes == null) return null;

            for (int i = 0; i < m_Scenes.Length; i++) {
                if (m_Scenes[i].GetType() == typeof(U)) return (U)m_Scenes[i];
            }

            return null;
        }

#endregion

    }
}