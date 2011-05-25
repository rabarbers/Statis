using System.Runtime.Serialization;

namespace StatisServiceContracts
{
    [DataContract]
    [KnownType(typeof(TextChoice))]
    [KnownType(typeof(NumberChoice))]
    [KnownType(typeof(ImgChoice))]
    public class Choice
    {
    }

    [DataContract]
    public class TextChoice : Choice
    {
        [DataMember]
        public string Option { get; set; }
        
        public TextChoice(string option)
        {
            Option = option;
        }
    }

    [DataContract]
    public class NumberChoice : Choice
    {
        [DataMember]
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
        [DataMember]
        public string Description { get; set; }
        public ImgChoice(string desc, object img)
        {
            Option = img;
            Description = desc;
        }
    }
}