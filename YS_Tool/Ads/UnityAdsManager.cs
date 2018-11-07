using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YS_Tool.Common.Singleton;

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
using UnityEngine.Advertisements;
#endif

using System;

namespace YS_Tool.Ads {
    public class UnityAdsManager<T> : GlobalSingletonMonoBehavior<T> where T : MonoBehaviour {

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR

#region Field Inspector

        [SerializeField]
        private string m_PlayStoreGameID;

        [SerializeField]
        private string m_AppStoreGameID;

#endregion



#region Method Unity Call

        protected virtual void Start() {
            Init();
        }

#endregion



#region Method Public

        /// <summary>
        /// このデバイスでUnityAdsが対応しているかどうかを返す。
        /// </summary>
        /// <returns></returns>
        public bool IsEnableAdsThisDevice() {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            return true;
#else
            return false;
#endif
        }

        public void Init() {
            Advertisement.Initialize(GetGameID());
        }

        public bool IsInit() {
            return Advertisement.isInitialized;
        }


        /// <summary>
        /// プラットフォームに合わせたGameIDを返す。
        /// </summary>
        public string GetGameID() {
#if UNITY_ANDROID
            return m_PlayStoreGameID;
#elif UNITY_IOS
            return m_AppStoreGameID;
#else
            return null;
#endif
        }

        
        public virtual void ShowAds() {
            Advertisement.Show();
        }



        public virtual void ShowRewardAds(Action<ShowResult> onResult) {
            ShowOptions options = new ShowOptions();
            options.resultCallback = onResult;

            Advertisement.Show("rewardedVideo", options);
        }



        public virtual bool IsReadyAds() {
            return Advertisement.IsReady();
        }



        public virtual bool IsReadyRewardAds() {
            return Advertisement.IsReady("rewardedVideo");
        }

#endregion

#endif
    }
}
