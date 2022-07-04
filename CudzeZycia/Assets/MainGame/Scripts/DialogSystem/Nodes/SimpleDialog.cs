using System;
using System.Collections.Generic;
using XNode;
using UnityEngine;


namespace Scripts.DialogSystem
{
    [NodeWidth(250), NodeTint("#40603e")]
    public class SimpleDialog : DialogNode
    {
        // prosty dialog (wszystko jest w zasadzie w DialogNode)

        [Output]
        public Connection Output;

        public override DialogNode GetNextDialog(int answerId)
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
    }
}