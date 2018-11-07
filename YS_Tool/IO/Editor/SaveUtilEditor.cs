using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace YS_Tool.IO {
    //[CustomEditor(typeof(SaveUtil))]
    public class SaveUtilEditor : MonoBehaviour {

        public const string CLEAR = "Tools/SaveUtil/Clear";

        [MenuItem(CLEAR)]
        public static void Clear() {
            SaveUtil.Clear();
            SaveUtil.Save();
            Debug.Log("SaveUtilで保持されている全てのデータをクリアしました。");
        }

        [MenuItem("Tools/SaveUtil/Change Save FileName")]
        public static void ChangeSaveFileName() {
            //GetWindow<SaveUtilEditor>();
        }
    }
}

