namespace Statis.Models
{
    public class IndivRespondent : AnonymousUser
    {
        string Email { get; set; }
        IndivRespondent(string mail)
        {
            Email = mail;
        }
    }
}