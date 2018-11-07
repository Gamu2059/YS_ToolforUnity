using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YS_Tool.Common.Singleton;
using GoogleMobileAds.Api;
using System;

namespace YS_Tool.Ads {
    public class AdmobManager<T> : GlobalSingletonMonoBehavior<T> where T : MonoBehaviour {

        #region Field Inspector

        /// <summary>
        /// テスト広告を使用するかどうか。
        /// true : テスト広告を使用する false : 本番用広告を使用する
        /// </summary>
        [SerializeField]
        private bool m_isUseTestAds;

        [Space()]
        [Header("Admob ID")]

        [SerializeField]
        private string _androidAppID;

        [SerializeField]
        private string _iOSAppID;

        [SerializeField]
        private string _androidBannerID;

        [SerializeField]
        private string _iOSBannerID;

        [SerializeField]
        private string _androidInterstitialID;

        [SerializeField]
        private string _iOSInterstitialID;

        [SerializeField]
        private string _androidRewardID;

        [SerializeField]
        private string _iOSRewardID;

        private readonly string _testBannerID = "ca-app-pub-3940256099942544/6300978111";

        private readonly string _testInterstitialID = "ca-app-pub-3940256099942544/1033173712";

        private readonly string _testRewardID = "ca-app-pub-3940256099942544/5224354917";

        private readonly string _unexpectedID = "unexpected_platform";

        #endregion



        #region Property Public

        public bool IsUseTestAds {
            get { return m_isUseTestAds; }
        }

        #endregion



        #region Method Unity Call

        protected virtual void Start() {
            Initialize();
        }

        private void OnApplicationPause(bool pause) {
            if (!pause) Initialize();
        }

        private void OnApplicationFocus(bool focus) {
            if (focus) Initialize();
        }

        #endregion



        #region Method GetID

        public string GetAppID() {
#if DEBUG_ON
            return _unexpectedID;
#endif

#if UNITY_ANDROID
            return _androidAppID;
#elif UNITY_IOS
            return _iOSAppID;
#else
            return _unexpectedID;
#endif
        }

        public string GetBannerID() {
#if DEBUG_ON
            return _testBannerID;
#endif

            if (IsUseTestAds)
                return _testBannerID;

#if UNITY_ANDROID
            return _androidBannerID;
#elif UNITY_IOS
            return _iOSBannerID;
#else
            return _unexpectedID;
#endif
        }

        public string GetInterstitialID() {
#if DEBUG_ON
            return _testInterstitialID;
#endif

            if (IsUseTestAds)
                return _testInterstitialID;

#if UNITY_ANDROID
            return _androidInterstitialID;
#elif UNITY_IOS
            return _iOSInterstitialID;
#else
            return _unexpectedID;
#endif
        }

        public string GetRewardID() {
#if DEBUG_ON
            return _testRewardID;
#endif

            if (IsUseTestAds)
                return _testRewardID;

#if UNITY_ANDROID
            return _androidRewardID;
#elif UNITY_IOS
            return _iOSRewardID;
#else
            return _unexpectedID;
#endif
        }

        #endregion



        #region Method EventHandler

        /// <summary>
        /// 広告の読込が完了した時に呼び出されます。
        /// </summary>
        protected virtual void OnLoadedBanner(object sender, EventArgs eventArgs) { }

        /// <summary>
        /// 広告の読込が失敗した時に呼び出されます。
        /// </summary>
        protected virtual void OnFailedToLoadBanner(object sender, AdFailedToLoadEventArgs eventArgs) { }

        /// <summary>
        /// 広告をタップした時に呼び出されます。
        /// インタースティシャルやリワード広告とは呼び出されるタイミングが異なります。
        /// </summary>
        protected virtual void OnOpeningBanner(object sender, EventArgs eventArgs) { }

        /// <summary>
        /// 広告のリンク先からアプリに戻ってきた時に呼び出されます。
        /// インタースティシャルやリワード広告とは呼び出されるタイミングが異なります。
        /// </summary>
        protected virtual void OnClosedBanner(object sender, EventArgs eventArgs) { }

        /// <summary>
        /// アプリがバックグラウンドにまわった時に呼び出されます。
        /// </summary>
        protected virtual void OnLeavingAppBanner(object sender, EventArgs eventArgs) { }



        /// <summary>
        /// 広告の読込が完了した時に呼び出されます。
        /// </summary>
        protected virtual void OnLoadedRect(object sender, EventArgs eventArgs) { }

        /// <summary>
        /// 広告の読込が失敗した時に呼び出されます。
        /// </summary>
        protected virtual void OnFailedToLoadRect(object sender, AdFailedToLoadEventArgs eventArgs) { }

        /// <summary>
        /// 広告をタップした時に呼び出されます。
        /// インタースティシャルやリワード広告とは呼び出されるタイミングが異なります。
        /// </summary>
        protected virtual void OnOpeningRect(object sender, EventArgs eventArgs) { }

        /// <summary>
        /// 広告のリンク先からアプリに戻ってきた時に呼び出されます。
        /// インタースティシャルやリワード広告とは呼び出されるタイミングが異なります。
        /// </summary>
        protected virtual void OnClosedRect(object sender, EventArgs eventArgs) { }

        /// <summary>
        /// アプリがバックグラウンドにまわった時に呼び出されます。
        /// </summary>
        protected virtual void OnLeavingAppRect(object sender, EventArgs eventArgs) { }



        /// <summary>
        /// 広告の読込が完了した時に呼び出されます。
        /// </summary>
        protected virtual void OnLoadedInterstitial(object sender, EventArgs eventArgs) { }

        /// <summary>
        /// 広告の読込が失敗した時に呼び出されます。
        /// </summary>
        protected virtual void OnFailedToLoadInterstitial(object sender, AdFailedToLoadEventArgs eventArgs) { }

        /// <summary>
        /// 広告が画面に表示された時に呼び出されます。
        /// </summary>
        protected virtual void OnOpeningInterstitial(object sender, EventArgs eventArgs) { }

        /// <summary>
        /// 広告が画面から消えた時に呼び出されます。
        /// </summary>
        protected virtual void OnClosedInterstitial(object sender, EventArgs eventArgs) { }

        /// <summary>
        /// アプリがバックグラウンドにまわった時に呼び出されます。
        /// </summary>
        protected virtual void OnLeavingAppInterstitial(object sender, EventArgs eventArgs) { }



        /// <summary>
        /// 広告の読込が完了した時に呼び出されます。
        /// </summary>
        protected virtual void OnLoadedReward(object sender, EventArgs eventArgs) { }

        /// <summary>
        /// 広告の読込が失敗した時に呼び出されます。
        /// </summary>
        protected virtual void OnFailedToLoadReward(object sender, AdFailedToLoadEventArgs eventArgs) { }

        /// <summary>
        /// 広告が画面に表示された時に呼び出されます。
        /// </summary>
        protected virtual void OnOpeningReward(object sender, EventArgs eventArgs) { }

        /// <summary>
        /// 報酬の受け取りが可能になった時に呼び出されます。
        /// </summary>
        protected virtual void OnRewardedReward(object sender, Reward eventArgs) { }

        /// <summary>
        /// 広告が画面から消えた時に呼び出されます。
        /// </summary>
        protected virtual void OnClosedReward(object sender, EventArgs eventArgs) { }

        /// <summary>
        /// アプリがバックグラウンドにまわった時に呼び出されます。
        /// </summary>
        protected virtual void OnLeavingAppReward(object sender, EventArgs eventArgs) { }

        #endregion

        protected void Initialize() {
            MobileAds.Initialize(GetAppID());
        }

        public virtual void ShowBanner() { }

        public virtual void HideBanner() { }

        public virtual void ShowRect() { }

        public virtual void HideRect() { }

        public virtual void ShowInterstitial(Action onOpening = null, Action onFailure = null) { }

        public virtual void ShowRewardVideo(Action<bool> result, Action onOpening = null, Action onFailure = null) { }
    }
}
