using System;
using System.Collections.Generic;
using XNode;
using UnityEngine;

/*
S³u¿y do tworzenia Nodów (bloków) w grafie z dialogami 
*/

namespace Scripts.DialogSystem
{
    [NodeWidth(250), NodeTint("#5e603e")]
    public class DialogQuestion : DialogNode
    {

        // wyjscia z bloku (i opcje które s¹ w Answers)
        [Output(dynamicPortList = true, connectionType = ConnectionType.Override)]
        public List<DialogAnswer> Answers;


        public override DialogNode GetNextDialog(int answerId)
        {         
            var port = GetPort("Answers " + answerId);
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
    }
}