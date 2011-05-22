using System;

namespace Statis.Models
{
    /// <summary>Represents anonymous user</summary>
    public class User
    {
        /// <summary></summary>
        public string UserName { get; private set; }

        /// <summary></summary>
        public string Email { get; set; }

        /// <summary>Creates anonymous user model</summary>
        public User()
        {
            UserName = Guid.NewGuid().ToString();
        }
        /// <summary>Creates anonymous user model</summary>
        /// <param name="userName">User Name</param>
        public User(string userName)
        {
            UserName = userName;
        }
    }

    /// <summary>Represents registered user</summary>
    public class RegisteredUser : User
    {
        /// <summary></summary>
        public string FirstName { get; set; }
        /// <summary></summary>
        public string LastName { get; set; }

        /// <summary></summary>
        /// <param name="userName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        public RegisteredUser(string userName, string firstName, string lastName, string email): base(userName)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }

    /// <summary>Represents administrator</summary>
    public class Administrator : RegisteredUser
    {
        /// <summary></summary>
        /// <param name="userName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        public Administrator(string userName, string firstName, string lastName, string email): base(userName, firstName, lastName, email)
        {
           
        }
    }

    /// <summary></summary>
    public class Analyst : RegisteredUser
    {
        /// <summary></summary>
        /// <param name="userName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        public Analyst(string userName, string firstName, string lastName, string email): base (userName, firstName, lastName, email)
        {
            
        }
    }
}