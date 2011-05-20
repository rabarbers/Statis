using System.Collections.Generic;

namespace Statis.Models
{
    public class ChoiceQuestion : Question
    {
// ReSharper disable FieldCanBeMadeReadOnly.Local
        List<Choice> _choiceList;
// ReSharper restore FieldCanBeMadeReadOnly.Local

        public ChoiceQuestion(List<Choice> choiceList)
        {
            _choiceList = choiceList;
        }

        void AddChoice(Choice choice)
        {
            _choiceList.Add(choice);
        }
        void MoveChoice(int fromI, int toI)
        {
            _choiceList.Insert(toI, _choiceList[fromI]);
            _choiceList.RemoveAt(fromI);
        }
        void DeleteChoice(int fromIndex)
        {
            _choiceList.RemoveAt(fromIndex);
        }
    }
}