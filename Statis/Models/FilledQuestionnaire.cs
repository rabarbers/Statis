using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Db4objects.Db4o;

namespace Statis.Models
{
    public class FilledQuestionnaire
    {
        private Questionnaire _questionnaire;
        private List<Answer> _aList;
        public FilledQuestionnaire(Questionnaire q)
        {
            this._questionnaire = q;
            _aList = new List<Answer>();
        }
        public void AddAnswer(Answer a)
        {
            _aList.Add(a);
        }
        public void RemoveLastAnswer()
        {
            _aList.RemoveAt(_aList.Count - 1);
        }
        public void SaveFilledQuestionnaire(IObjectContainer db)
        {
            db.Store(this);

        }
        public void DeleteFilledQuestionnaire(IObjectContainer db)
        {
            // db query
            //	    FilledQuestionnaire found = from FilledQuestionnaire fq in db
            //					where fq.descript
            db.Delete(this);
        }
    }
}