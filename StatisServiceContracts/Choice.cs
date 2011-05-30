using System.Runtime.Serialization;

namespace StatisServiceContracts
{
    [DataContract]
    [KnownType(typeof(TextChoice))]
    [KnownType(typeof(NumberChoice))]
    [KnownType(typeof(ImgChoice))]
    public class Choice
    {
        [DataMember]
        public int Index { get; set; }
    }

    [DataContract]
    public class TextChoice : Choice
    {
        [DataMember]
        public string Option { get; set; }
        
        public TextChoice(string option, int index)
        {
            Option = option;
            Index = index;
        }
    }

    [DataContract]
    public class NumberChoice : Choice
    {
        [DataMember]
        public double Option { get; set; }
        
        public NumberChoice(double option, int index)
        {
            Option = option;
            Index = index;
        }
    }

    [DataContract]
    public class ImgChoice : Choice
    {
        public object Option { get; set; }
        [DataMember]
        public string Description { get; set; }
        public ImgChoice(string desc, object img, int index)
        {
            Option = img;
            Description = desc;
            Index = index;
        }
    }
}