using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace StatisServiceContracts
{
    /// <summary>Represents anonymous user</summary>
    [DataContract]
    [KnownType(typeof(Analyst))]
    [KnownType(typeof(Administrator))]
    public class User
    {
        /// <summary></summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>Creates anonymous user model</summary>
        /// <param name="email">User email</param>
        public User(string email)
        {
            Email = email;
        }
    }

    /// <summary>Represents registered user</summary>
    [DataContract]
    [KnownType(typeof(Questionnaire))]
    [KnownType(typeof(Analyst))]
    [KnownType(typeof(User))]
    public class Analyst : User
    {
        /// <summary></summary>
        [DataMember]
        public string UserName { get; set; }
        /// <summary></summary>
        [DataMember]
        public string FirstName { get; set; }
        /// <summary></summary>
        [DataMember]
        public string LastName { get; set; }
        /// <summary></summary>
        [DataMember]
        public string Password { get; set; }

        /// <summary></summary>
        [DataMember]
        public List<Questionnaire> Questionnaires { get; set; }

        /// <summary></summary>
        [DataMember]
        public List<Analyst> TrustedAnalysts{ get; set; }
        /// <summary></summary>
        [DataMember]
        public List<User> Respondents { get; set; }

        /// <summary></summary>
        /// <param name="userName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public Analyst(string userName, string firstName, string lastName, string email, string password): base(email)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Questionnaires = new List<Questionnaire>();
            TrustedAnalysts = new List<Analyst>();
            Respondents = new List<User>();
        }

        //public string GetMd5Hash(string input)
        //{
        //    var x = new System.Security.Cryptography.MD5CryptoServiceProvider();
        //    var bs = System.Text.Encoding.UTF8.GetBytes(input);
        //    bs = x.ComputeHash(bs);
        //    var s = new System.Text.StringBuilder();
        //    foreach (var b in bs)
        //    {
        //        s.Append(b.ToString("x2").ToLower());
        //    }
        //    var password = s.ToString();
        //    return password;
        //}
    }

    /// <summary>Represents administrator</summary>
    [DataContract]
    public class Administrator : Analyst
    {
        /// <summary></summary>
        /// <param name="userName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public Administrator(string userName, string firstName, string lastName, string email, string password): base(userName, firstName, lastName, email, password)
        {
        }
    }   
}