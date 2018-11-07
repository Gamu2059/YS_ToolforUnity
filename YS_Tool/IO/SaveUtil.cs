using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YS_Tool.Common.Singleton;
using YS_Tool.Common.Utility;

namespace YS_Tool.IO {
    /// <summary>
    /// 内部に SaveDataSerializer を保持し、その機能をラップするクラスです。
    /// </summary>
    public class SaveUtil : Singleton<SaveUtil> {

        private const string DEFAULT_FILE_NAME = "save.json";

        private const string DEFAULT_ENC_FILE_NAME = "save.dat";


        #region Field Private

        /// <summary>
        /// 通常のセーブファイルの名前
        /// </summary>
        [SerializeField]
        private string m_FileName = null;

        /// <summary>
        /// 暗号化された実用データのセーブファイルの名前
        /// </summary>
        [SerializeField]
        private string m_EncryptFileName = null;

        /// <summary>
        /// データを保持する本体。
        /// 初期化を走らせるため、意図的に static を付けていない。
        /// </summary>
        private SaveDataSerializer m_DataBase = null;

        private static UnityEvent m_OnCompleteLoad = new UnityEvent();

        private static UnityEvent m_OnFailureLoad = new UnityEvent();

        private static UnityEvent m_OnCompleteSave = new UnityEvent();

        private static UnityEvent m_OnFailureSave = new UnityEvent();

        #endregion



        #region Property Public

        public static UnityEvent OnCompleteLoad {
            get { return m_OnCompleteLoad; }
        }

        public static UnityEvent OnFailureLoad {
            get { return m_OnFailureLoad; }
        }

        public static UnityEvent OnCompleteSave {
            get { return m_OnCompleteSave; }
        }

        public static UnityEvent OnFailureSave {
            get { return m_OnFailureSave; }
        }

        #endregion



        #region Method Override

        protected override void OnConstructor() {
            base.OnConstructor();


#if UNITY_IOS
            // TODO : iOSでキャッシュされないように設定する
#endif
            CheckFileName();
            InitDataBase();
        }

        #endregion



        #region Method Private

        private SaveDataSerializer GetDataBase() {
            InitDataBase();
            return m_DataBase;
        }

        /// <summary>
        /// セーブファイルの名前を確認し、利用できない名前であればデフォルト名にする。
        /// </summary>
        private void CheckFileName() {
            if (string.IsNullOrEmpty(m_FileName)) {
                m_FileName = DEFAULT_FILE_NAME;
            }
            if (string.IsNullOrEmpty(m_EncryptFileName)) {
                m_EncryptFileName = DEFAULT_ENC_FILE_NAME;
            }
        }

        private void InitDataBase() {
            if (m_DataBase == null) {
                CheckFileName();

                string fileName;
#if UNITY_EDITOR
                fileName = m_FileName;
#else
                fileName = m_EncryptFileName;
#endif

                m_DataBase = new SaveDataSerializer(Application.persistentDataPath, fileName);
                PrivateLoad();
            }
        }

        private void CallBackEvent(Action a, UnityEvent e) {
            EventUtility.SafeInvokeAction(a);
            EventUtility.SafeInvokeUnityEvent(e);
        }

        private void PrivateLoad(Action onComplete = null, Action onFailure = null) {
            // GetDataBase() を呼び出すと InitDataBase() で PrivateLoad() を呼び出すのでコールループになる
            if (m_DataBase == null) {
                CallBackEvent(onFailure, m_OnFailureLoad);
                return;
            }

#if UNITY_EDITOR
            m_DataBase.Load(
                () => CallBackEvent(onComplete, m_OnCompleteLoad),
                () => CallBackEvent(onFailure, m_OnFailureLoad)
            );
#else
            m_DataBase.EncryptLoad(
                () => CallBackEvent(onComplete, m_OnCompleteLoad),
                () => CallBackEvent(onFailure, m_OnFailureLoad)
            );
#endif
        }

        private void PrivateSave(Action onComplete = null, Action onFailure = null) {
            if (m_DataBase == null) {
                CallBackEvent(onFailure, m_OnFailureSave);
                return;
            }

#if UNITY_EDITOR
            m_DataBase.Save(
                () => CallBackEvent(onComplete, m_OnCompleteSave),
                () => CallBackEvent(onFailure, m_OnFailureSave)
            );
#else
            m_DataBase.EncryptSave(
                () => CallBackEvent(onComplete, m_OnCompleteSave),
                () => CallBackEvent(onFailure, m_OnFailureSave)
            );
#endif
        }

        #endregion



#region Method Public

        public static void Save(Action onComplete = null, Action onFailure = null) {
            GetInstance().PrivateSave(onComplete, onFailure);
        }

        public static void Load(Action onComplete = null, Action onFailure = null) {
            GetInstance().PrivateLoad(onComplete, onFailure);
        }

        public static void SetList<T>(string key, List<T> list) {
            GetInstance().GetDataBase().SetList<T>(key, list);
        }

        public static List<T> GetList<T>(string key, List<T> _default) {
            return GetInstance().GetDataBase().GetList<T>(key, _default);
        }

        public static void SetStruct<T>(string key, T obj) where T : struct {
            GetInstance().GetDataBase().SetStruct<T>(key, obj);
        }

        public static T GetStruct<T>(string key, T _default) where T : struct {
            return GetInstance().GetDataBase().GetStruct(key, _default);
        }

        public static void SetClass<T>(string key, T obj) where T : class, new() {
            GetInstance().GetDataBase().SetClass<T>(key, obj);
        }

        public static T GetClass<T>(string key, T _default) where T : class, new() {
            return GetInstance().GetDataBase().GetClass(key, _default);

        }

        public static void SetString(string key, string value) {
            GetInstance().GetDataBase().SetString(key, value);
        }

        public static string GetString(string key, string _default = "") {
            return GetInstance().GetDataBase().GetString(key, _default);
        }

        public static void SetInt(string key, int value) {
            GetInstance().GetDataBase().SetInt(key, value);
        }

        public static int GetInt(string key, int _default = 0) {
            return GetInstance().GetDataBase().GetInt(key, _default);
        }

        public static void SetFloat(string key, float value) {
            GetInstance().GetDataBase().SetFloat(key, value);
        }

        public static float GetFloat(string key, float _default = 0.0f) {
            return GetInstance().GetDataBase().GetFloat(key, _default);
        }

        public static void SetBool(string key, bool value) {
            GetInstance().GetDataBase().SetBool(key, value);
        }

        public static bool GetBool(string key, bool _default = false) {
            return GetInstance().GetDataBase().GetBool(key, _default);
        }

        public static void Clear() {
            GetInstance().GetDataBase().Clear();
        }

        public static void Remove(string key) {
            GetInstance().GetDataBase().Remove(key);
        }

        public static bool ContainsKey(string _key) {
            return GetInstance().GetDataBase().ContainsKey(_key);
        }

        public static List<string> GetKeys() {
            return GetInstance().GetDataBase().GetKeys();
        }

#endregion

    }
}
