using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Db4objects.Db4o;

namespace Statis.Models
{
    public class Questionnaire
    {
        public string Name;
        public string Description { get; set; }
        public List<Question> Questions;
        public Questionnaire(string description, string name)
        {
            this.Name = name;
            this.Description = description;
            Questions = new List<Question>();
        }
        public void SaveQuestionnaire(IObjectContainer db)
        {
            // db query here.
            db.Store(this);
        }
        public void DeleteQuestionnaire(IObjectContainer db)
        {
            // db query
            db.Delete(this);
        }
        public void SendQuestionnaire(int id)
        {
            // db query retrieves questionnaire No.[id]

        }
        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }
        public void RemoveQuestion(Question question)
        {
            Questions.Remove(question);
        }
        public Questionnaire ViewQuestionnaire(int id)
        {
            Questionnaire found = new Questionnaire(Description);
            // connect to db and get the right questionnaire

            return found;
        }
    }
}