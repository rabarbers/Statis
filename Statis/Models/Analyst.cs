namespace Statis.Models
{
    class Analyst : RegisteredUser
    {
        public Analyst(string name, string surname, string mail, string user)
        {
            FirstName = name;
            LastName = surname;
            Email = mail;
            UserName = user;
        }
    }
}