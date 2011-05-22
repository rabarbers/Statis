using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatisServiceContracts
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
    }
}