using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StatisServiceContracts
{
    [DataContract]
    [KnownType(typeof(ChoiceAnswer))]
    [KnownType(typeof(TextAnswer))]
    public class Answer
    {
        
    }

    [DataContract]
    public class ChoiceAnswer : Answer
    {
        [DataMember]
        public List<int> AnswerList = new List<int>();

        public ChoiceAnswer(int indexNo)
        {
            AnswerList.Add(indexNo);
        }
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