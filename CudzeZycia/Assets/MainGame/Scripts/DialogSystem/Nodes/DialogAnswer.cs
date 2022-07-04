using System;
using System.Collections.Generic;
using XNode;
using UnityEngine;


namespace Scripts.DialogSystem
{

    [Serializable]
    public class DialogAnswer
    {
        public string Message;        

        public enum AnswerType
        {
            MainQuest = 1,
            Secondary = 2,
            Gray = 3,
        }
        public AnswerType Type = AnswerType.Secondary;

        
        public Color GetTextColor()
        {
            Color myColor;
            if (Type == AnswerType.MainQuest)
            {
                ColorUtility.TryParseHtmlString("#fca548", out myColor);
            }
            else if (Type == AnswerType.Secondary)
            {
                ColorUtility.TryParseHtmlString("#61ccff", out myColor);
            }
            else
            {
                ColorUtility.TryParseHtmlString("#989898", out myColor);
            }
            return myColor;
        }


    }

}