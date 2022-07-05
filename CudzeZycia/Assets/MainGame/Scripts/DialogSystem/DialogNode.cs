using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using XNode;

namespace Scripts.DialogSystem
{
    public enum AvatarPosition
    {
        Left = 1,
        Right = 2,
    }

    [Serializable]
    public struct Connection { }

    [NodeWidth(250)]
    public abstract class DialogNode : Node
    {
        [Input]
        public Connection input; // wejscie do bloku

        public DialogAvatarType AvatarName = DialogAvatarType.FoxyDebug;
        
        public AvatarPosition AvatarPosition = AvatarPosition.Right;

        [TextArea(4, 500)]
        public string DialogText;


        public override object GetValue(NodePort port)
        {
            return null;
        }

        public virtual DialogNode GetNextDialog(int answerId)
        {
            var port = GetPort("Output");
            if (port.IsConnected)
            {
                var node = port.Connection.node;
                return node as DialogNode;
            }
            else
            {
                return null;
            }
        }

        public virtual DialogNode GetPreviousDialog()
        {
            var input = GetInputPort("input");
            if (input.IsConnected)
            {
                var node = input.Connection.node;
                return node as DialogNode;
            }
            else
            {
                return null;
            }
        }
    }

}

