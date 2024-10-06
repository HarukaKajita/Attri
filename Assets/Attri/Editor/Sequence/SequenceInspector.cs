using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Attri.Runtime;
using UnityEditor;
using UnityEngine;

namespace Attri.Editor
{
    
    [CustomEditor(typeof(Sequence), true) /*, CanEditMultipleObjects*/]
    public class SequenceInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // 通常のInspectorを表示
            base.OnInspectorGUI();
            
            var sequence = target as Sequence;
            if (sequence == null) return;
            
            // セットアップボタン
            if (GUILayout.Button("Gather Containers"))
                sequence.GatherContainer();
        }
    }
}
