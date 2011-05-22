using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StatisServiceContracts
{
    [DataContract]
<<<<<<< HEAD
    public class Answer
    {
        string answerText { get; set; }
        bool choiceList = false;
=======
    [KnownType(typeof(Answer))]

    public abstract class Answer
    {
        [DataMember]
        public Guid answerId;
    }
    public class ChoiceAnswer : Answer
    {
        [DataMember]
        public List<int> answerList;
        public ChoiceAnswer(int indexNo)
        {
            answerList.Add(indexNo);
        }
    }
    public class TextAnswer : Answer
    {
        [DataMember]
        public string answerText;
        public TextAnswer(string text)
        {
            answerText = text;
        }
>>>>>>> 7ebdf2b357af77d07fd0082d61335da987c196db
    }
}