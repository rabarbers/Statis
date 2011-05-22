using System.Runtime.Serialization;

namespace StatisServiceContracts
{
    [DataContract]
    [KnownType(typeof(TextChoice))]
    [KnownType(typeof(NumberChoice))]
    [KnownType(typeof(ImgChoice))]
    public abstract class Choice
    {
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
        public double Option { get; set; }
        public NumberChoice(double option)
        {
            Option = option;
        }
    }

    [DataContract]
    public class ImgChoice : Choice
    {
        public object Option { get; set; }
        public string Description { get; set; }
        public ImgChoice(string desc, object img)
        {
            Option = img;
            Description = desc;
        }
    }
}