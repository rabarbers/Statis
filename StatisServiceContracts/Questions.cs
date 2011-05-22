using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

namespace StatisServiceContracts
{
    [DataContract]
    [KnownType(typeof(TextQuestion))]
    [KnownType(typeof(ImgChoiceQuestion))]
    [KnownType(typeof(ImgTextQuestion))]
    [KnownType(typeof(ChoiceQuestion))]
    public class Question
    {
        [DataMember]
        public Guid questionId;
        [DataMember]
        public string Text { get; set; }
    }

    [DataContract]
    public class TextQuestion : Question
    {
        public TextQuestion()
        { 
        }
        public TextQuestion(string text)
        {
            Text = text;
        }
    }

 /*   [DataContract]
    public class ImgQuestion : Question
    {
        [DataMember]
        public List<Choice> _choiceList;
        [DataMember]
        public object Img { get; set; }
        public ImgQuestion(string text, object img)
        {
            Text = text;
            Img = img;
        }
    }*/

    [DataContract]
    [KnownType(typeof(Choice))]
    public class ChoiceQuestion : Question
    {
        [DataMember]
        public List<Choice> _choiceList;

        public ChoiceQuestion()
        { 
            _choiceList = new List<Choice>();
        }
        public ChoiceQuestion(string text, List<Choice> choiceList)
        {
            Text = text;
            _choiceList = new List<Choice>(choiceList);
        }
    }

    [DataContract]
    public class ImgChoiceQuestion : ChoiceQuestion
    {
        [DataMember]
        public object Img { get; set; }
        public ImgChoiceQuestion(string text, object img, List<Choice> choiceList)
        {
            Text = text;
            Img = img;
            _choiceList = new List<Choice>(choiceList);
        }
    }

    [DataContract]
    public class ImgTextQuestion : TextQuestion
    {
        [DataMember]
        public object Img { get; set; }
        public ImgTextQuestion(string text, object img)
        {
            Text = text;
            Img = img;
        }
    }
}