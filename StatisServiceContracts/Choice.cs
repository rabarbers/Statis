using System.Runtime.Serialization;

namespace StatisServiceContracts
{
    [DataContract]
    [KnownType(typeof(TextChoice))]
    [KnownType(typeof(NumberChoice))]
    [KnownType(typeof(ImgChoice))]
    public abstract class Choice
    {
<<<<<<< HEAD

=======
>>>>>>> 7ebdf2b357af77d07fd0082d61335da987c196db
    }

    [DataContract]
    public class TextChoice : Choice
    {
        public string Option { get; set; }
        public TextChoice(string option)
        {
            Option = option;
        }
    }

    [DataContract]
    public class NumberChoice : Choice
    {
<<<<<<< HEAD
        public int Option { get; set; }
=======
        public double Option { get; set; }
        public NumberChoice(double option)
        {
            Option = option;
        }
>>>>>>> 7ebdf2b357af77d07fd0082d61335da987c196db
    }

    [DataContract]
    public class ImgChoice : Choice
    {
        public object Option { get; set; }
        public string Description { get; set; }
<<<<<<< HEAD
=======
        public ImgChoice(string desc, object img)
        {
            Option = img;
            Description = desc;
        }
>>>>>>> 7ebdf2b357af77d07fd0082d61335da987c196db
    }
}