using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YS_Tool.Common.Singleton;
using YS_Tool.Common.Utility;
using System;

#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif

namespace YS_Tool.GPS {
    /// <summary>
    /// GooglePlayServicesのログインや実績の処理を管理するクラス。
    /// </summary>
    public class GPSManager<T> : LocalSingletonMonoBehavior<T> where T : MonoBehaviour {

        protected virtual void Start() {
            Init();
            SignIn();
        }

        protected virtual void OnApplicationQuit() {
            SingOut();
        }

        /// <summary>
        /// インスタンスの初期化を行う。
        /// </summary>
        protected void Init() {
#if UNITY_ANDROID
            var config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.Activate();
#endif
        }

        /// <summary>
        /// サインインしているかどうかを返す。
        /// </summary>
        /// <returns></returns>
        public bool IsAuthenticated() {
#if UNITY_ANDROID
            return ((PlayGamesPlatform)Social.Active).IsAuthenticated();
#else
            return false;
#endif
        }

        /// <summary>
        /// GooglePlayServiceにサインインする。
        /// </summary>
        public virtual void SignIn(Action<bool> result = null) {
#if UNITY_ANDROID
            Social.localUser.Authenticate((success) => EventUtility.SafeInvokeAction(result, success));
#endif
        }

        /// <summary>
        /// GooglePlayServiceからサインアウトする。
        /// </summary>
        public void SingOut() {
#if UNITY_ANDROID
            ((PlayGamesPlatform)Social.Active).SignOut();
#endif
        }

        /// <summary>
        /// 実績ボードを開く。
        /// サインインしていない場合は、サインイン処理を行ってからボードを開く。
        /// </summary>
        public void ShowAchieveMent() {
#if UNITY_ANDROID
            if (IsAuthenticated()) {
                Social.ShowAchievementsUI();
            } else {
                SignIn((success) => {
                    Social.ShowAchievementsUI();
                });
            }
#endif
        }

        /// <summary>
        /// 実績を解除する。
        /// </summary>
        public virtual void Achieve(string achieveID, Action<bool> result = null) {
#if UNITY_ANDROID
            ((PlayGamesPlatform)Social.Active).ReportProgress(achieveID, 100.0f, (success) => EventUtility.SafeInvokeAction(result, success));
#endif
        }
    }
}
