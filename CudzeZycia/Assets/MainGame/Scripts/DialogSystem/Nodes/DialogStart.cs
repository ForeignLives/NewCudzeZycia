using System;
using System.Collections.Generic;
using XNode;
using UnityEngine;


namespace Scripts.DialogSystem
{
    [NodeTint("#046b30")]
    public class DialogStart : Node
    {
        [Output]
        public Connection Output;


        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}