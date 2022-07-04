using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;



/// <summary>
/// Edytor nodów (mo¿na tutaj zrobiæ custom wygl¹d)
/// </summary>

namespace Scripts.DialogSystem.Editor
{
    [CustomNodeEditor(typeof(DialogNode))]
    public class DialogNodeEditor : NodeEditor
    {
        DialogAvatarManager dialogAvatarManager;
        public DialogNodeEditor()
        {
            dialogAvatarManager = DialogAvatarManager.GetResourceDialogAvatarManager();
        }

        public override void OnBodyGUI()
        {
            /* Default render all
            base.OnBodyGUI(); return;
            */

            serializedObject.Update(); // leave it here
            NodeEditorWindow.current.titleContent = new GUIContent("Edytor Dialogów");

            // Get data
            var segment = serializedObject.targetObject as DialogNode;
            var avatarTexture = dialogAvatarManager.GetAvatarAsTexture(segment.AvatarName);
            var avatarName = dialogAvatarManager.GetAvatarName(segment.AvatarName);
            
            // render preview
            GUILayout.Label("Preview");
            var styl = new GUIStyle();
            if (segment.AvatarPosition == AvatarPosition.Right) { styl.alignment = TextAnchor.MiddleRight; }
            if (segment.AvatarPosition == AvatarPosition.Left) { styl.alignment = TextAnchor.MiddleLeft; }
            GUILayout.Label(new GUIContent(avatarTexture, avatarName), styl, new GUILayoutOption[] {
                GUILayout.Height(80),
            });

            // render all inputs etc
            string[] excludes = { "m_Script", "graph", "position", "ports", "Answers" };
            SerializedProperty iterator = serializedObject.GetIterator();
            bool enterChildren = true;
            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;
                if (excludes.Contains(iterator.name)) continue;
                NodeEditorGUILayout.PropertyField(iterator, true);
            }

            if (segment.GetType() == typeof(DialogQuestion))
            {
                // render answers Dynamic List with answers            
                NodeEditorGUILayout.DynamicPortList(
                    "Answers",
                    typeof(DialogAnswer),
                    serializedObject,
                    NodePort.IO.Input,
                    Node.ConnectionType.Override,
                    Node.TypeConstraint.None,
                    NewOnCreateReorderableList
                );
            }
            
            /*foreach (NodePort dynamicPort in target.DynamicPorts)
            {
                if (NodeEditorGUILayout.IsDynamicPortListPort(dynamicPort)) continue;
                NodeEditorGUILayout.PortField(dynamicPort);
            }*/

            serializedObject.ApplyModifiedProperties(); // leave it here
        }

        void NewOnCreateReorderableList(ReorderableList list)
        {
            DialogQuestion node = serializedObject.targetObject as DialogQuestion;
            SerializedProperty arrayData = serializedObject.FindProperty("Answers");

            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                XNode.NodePort port = node.GetPort("Answers " + index);
                // get old color and set new
                var oldColor = GUI.backgroundColor;
                GUI.backgroundColor = node.Answers[index].GetTextColor();

                if (arrayData.arraySize <= index)
                {
                    EditorGUI.LabelField(rect, "Array[" + index + "] data out of range");
                    return;
                }
                SerializedProperty itemData = arrayData.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(rect, itemData, true);

                if (port != null)
                {
                    // draw connection
                    Vector2 pos = rect.position + (port.IsOutput ? new Vector2(rect.width + 6, 0) : new Vector2(-36, 0));
                    NodeEditorGUILayout.PortField(pos, port);
                }

                // rollback color changes
                GUI.backgroundColor = oldColor;
            };
        }

    }


}