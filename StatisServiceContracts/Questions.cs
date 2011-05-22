using System.Collections.Generic;
using System.Runtime.Serialization;

namespace StatisServiceContracts
{
    [DataContract]
    [KnownType(typeof(TextQuestion))]
    [KnownType(typeof(ImgQuestion))]
    [KnownType(typeof(ChoiceQuestion))]
    public class Question
    {
        [DataMember]
        public string Text { get; set; }
    }

    [DataContract]
    public class TextQuestion : Question
    {
        public TextQuestion(string text)
        {
            Text = text;
        }
    }

    [DataContract]
    public class ImgQuestion : Question
    {
        [DataMember]
        public object Image { get; set; }
    }

    [DataContract]
    [KnownType(typeof(Choice))]
    public class ChoiceQuestion : Question
    {
        [DataMember]
        public List<Choice> _choiceList;
        
        public ChoiceQuestion(List<Choice> choiceList)
        {
            _choiceList = choiceList;
        }

        
    }
}