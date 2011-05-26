using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StatisServiceContracts
{
    [DataContract]
    [KnownType(typeof(Answer))]
    [KnownType(typeof(Guid))]
    public class FilledQuestionnaire
    {
        [DataMember]
        public string QuestionnaireName { get; set; }
        [DataMember]
        public List<Answer> Answers { get; set; }
        [DataMember]
        public Guid Id { get; set; }

        public FilledQuestionnaire(Questionnaire q): this(q.Name) { }
        public FilledQuestionnaire(string questionnaireName)
        {
            QuestionnaireName = questionnaireName;
            Answers = new List<Answer>();
            Id = Guid.NewGuid();
        }
        
    }
}