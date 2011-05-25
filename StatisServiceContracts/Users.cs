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
        public User()
        {
            
        }
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
        public string UserName { get; private set; }
        /// <summary></summary>
        [DataMember]
        public string FirstName { get; set; }
        /// <summary></summary>
        [DataMember]
        public string LastName { get; set; }
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
        public Analyst()
        {
            
        }
        /// <summary></summary>
        /// <param name="userName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        public Analyst(string userName, string firstName, string lastName, string email): base(email)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Questionnaires = new List<Questionnaire>();
            TrustedAnalysts = new List<Analyst>();
            Respondents = new List<User>();
        }
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
        public Administrator(string userName, string firstName, string lastName, string email): base(userName, firstName, lastName, email)
        {
           
        }
    }
}