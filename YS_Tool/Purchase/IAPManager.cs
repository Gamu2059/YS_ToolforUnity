using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YS_Tool.Common.Singleton;

using UnityEngine.Purchasing;

namespace YS_Tool.IAP {
    /// <summary>
    /// IAPの処理を管理するクラス。
    /// </summary>
    public abstract class IAPManager<T> : LocalSingletonMonoBehavior<T> where T : MonoBehaviour {

        private static bool m_IsRestoredBeginning;

        private static CodelessIAPStoreListener m_iapsl;

        protected static CodelessIAPStoreListener IAPSL {
            get {
                if (m_iapsl == null) m_iapsl = CodelessIAPStoreListener.Instance;
                return m_iapsl;
            }
        }

        protected virtual void Update() {
            RestoreProcess();
        }

        protected void RestoreProcess() {
            if (m_IsRestoredBeginning) return;

            if (CodelessIAPStoreListener.initializationComplete) {
                var ctrl = IAPSL.StoreController;
                foreach (var product in ctrl.products.all) {
                    if (product.hasReceipt) {
                        PurchaseProcess(product);
                    }
                }
                m_IsRestoredBeginning = true;
            }
        }

        protected abstract void PurchaseProcess(Product product);

        /// <summary>
        /// プロダクトの名前についているアプリ名を取り除いた文字列を返す。
        /// </summary>
        public string GetProductTitle(string productTitle) {
            return productTitle.Replace(string.Format("({0})", Application.productName), "").Trim();
        }
    }
}
