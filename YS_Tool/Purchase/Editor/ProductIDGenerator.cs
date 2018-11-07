using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Purchasing;
using System;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace YS_Tool.Purchase {
    public class IAPProductIDConstantizer {

        /// <summary>
        /// 保存場所のパス
        /// </summary>
        private static readonly string _path = "Assets/Constant/IAPProductID.cs";

        /// <summary>
        /// 無効な文字の配列
        /// </summary>
        private static readonly string[] INVALUD_CHARS = {
                " ", "!", "\"", "#", "$",
                "%", "&", "\'", "(", ")",
                "-", "=", "^", "~", "\\",
                "|", "[", "{", "@", "`",
                "]", "}", ":", "*", ";",
                "+", "/", "?", ".", ">",
                ",", "<"
            };

        [MenuItem("Tools/IAP/Constantize ProductID")]
        public static void ConstantizeProductID() {
            if (CanConstantize()) {
                Constantize();
                EditorUtility.DisplayDialog("Success", "IAP ProductIDs are constantized!", "OK");
            } else {
                EditorUtility.DisplayDialog("Error", "IAP ProductIDs cannot be constantized.\nBecause of Unity is working.", "OK");
            }
        }

        private static bool CanConstantize() {
            return !EditorApplication.isPlaying && !Application.isPlaying && !EditorApplication.isCompiling;
        }

        private static void Constantize() {
            try {
                var builder = new StringBuilder();
                builder.AppendLine("/// <summary>");
                builder.AppendLine("/// IAPのProductIDを定数として保持するクラス");
                builder.AppendLine("/// </summary>");
                builder.AppendLine("public static class IAPProductID {");

                var catalog = ProductCatalog.LoadDefaultCatalog();
                foreach (var product in catalog.allProducts) {
                    var name = RemoveInvalidChars(product.id.Replace(".", "_")).ToUpper();
                    builder.Append("\t").AppendFormat(@"public const string {0} = ""{1}"";", name, product.id).AppendLine();
                }

                builder.AppendLine("}");

                var directoryName = Path.GetDirectoryName(_path);
                if (!Directory.Exists(directoryName)) {
                    Directory.CreateDirectory(directoryName);
                }

                File.WriteAllText(_path, builder.ToString(), Encoding.UTF8);
                AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
            } catch (Exception e) {
                Debug.LogException(e);
            }
        }


        /// <summary>
        /// 無効な文字を削除します。
        /// </summary>
        private static string RemoveInvalidChars(string str) {
            Array.ForEach(INVALUD_CHARS, c => str = str.Replace(c, string.Empty));
            return str;
        }
    }
}
