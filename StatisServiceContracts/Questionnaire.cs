using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace StatisServiceContracts
{
    /// <summary></summary>
    [DataContract]
    [KnownType(typeof(Question))]
    public class Questionnaire
    {
        /// <summary></summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary></summary>
        [DataMember]
        public string Description { get; set; }
        /// <summary></summary>
        [DataMember]
        public List<Question> Questions { get; private set; }

        public Questionnaire()
        {
        }

        /// <summary></summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public Questionnaire(string name, string description)
        {
            Name = name;
            Description = description;
            Questions = new List<Question>();
        }

        
    }
}