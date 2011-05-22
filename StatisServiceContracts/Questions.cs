using System.Collections.Generic;
using System.Runtime.Serialization;
<<<<<<< HEAD
=======
using System;
>>>>>>> 7ebdf2b357af77d07fd0082d61335da987c196db

namespace StatisServiceContracts
{
    [DataContract]
    [KnownType(typeof(TextQuestion))]
<<<<<<< HEAD
    [KnownType(typeof(ImgQuestion))]
=======
    [KnownType(typeof(ImgChoiceQuestion))]
    [KnownType(typeof(ImgTextQuestion))]
>>>>>>> 7ebdf2b357af77d07fd0082d61335da987c196db
    [KnownType(typeof(ChoiceQuestion))]
    public class Question
    {
        [DataMember]
<<<<<<< HEAD
=======
        public Guid questionId;
        [DataMember]
>>>>>>> 7ebdf2b357af77d07fd0082d61335da987c196db
        public string Text { get; set; }
    }

    [DataContract]
    public class TextQuestion : Question
    {
<<<<<<< HEAD
=======
        public TextQuestion()
        { 
        }
>>>>>>> 7ebdf2b357af77d07fd0082d61335da987c196db
        public TextQuestion(string text)
        {
            Text = text;
        }
    }

<<<<<<< HEAD
    [DataContract]
    public class ImgQuestion : Question
    {
        [DataMember]
        public object Image { get; set; }
    }
=======
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
>>>>>>> 7ebdf2b357af77d07fd0082d61335da987c196db

    [DataContract]
    [KnownType(typeof(Choice))]
    public class ChoiceQuestion : Question
    {
        [DataMember]
        public List<Choice> _choiceList;
<<<<<<< HEAD
        
        public ChoiceQuestion(List<Choice> choiceList)
        {
            _choiceList = choiceList;
        }

        
=======

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
>>>>>>> 7ebdf2b357af77d07fd0082d61335da987c196db
    }
}