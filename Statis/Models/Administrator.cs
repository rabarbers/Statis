namespace Statis.Models
{
    class Administrator : RegisteredUser
    {
        public Administrator(string name, string surname, string mail, string user)
        {
            FirstName = name;
            LastName = surname;
            Email = mail;
            UserName = user;
        }
    }
}