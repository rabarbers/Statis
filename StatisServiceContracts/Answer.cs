using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StatisServiceContracts
{
    [DataContract]
    [KnownType(typeof(SingleChoiceAnswer))]
    [KnownType(typeof(TextAnswer))]
    public class Answer
    {
        
    }

    [DataContract]
    public class SingleChoiceAnswer : Answer
    {
        [DataMember]
        public int Choice { get; set; }
    }

    [DataContract]
    public class TextAnswer : Answer
    {
        [DataMember]
        public string AnswerText;
        
        public TextAnswer(string text)
        {
            AnswerText = text;
        }
    }
}