namespace Statis.Models
{
    public class TextChoice : Choice
    {
        string Option { get; set; }
        public TextChoice(string option)
        {
            this.Option = option;
        }
    }
}