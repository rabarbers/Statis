using System;
using Statis.StatisServices;

namespace Statis.ViewModels
{
    public class TextChoiceViewModelForAnswer: ChoiceViewModel
    {
        private readonly TextChoice _choiceModel;
        private readonly SingleChoiceAnswer _answerModel;
        private readonly Guid _questionId;
        private bool _answer;

        public TextChoiceViewModelForAnswer(TextChoice choiceModel, SingleChoiceAnswer answerModelModel, Guid questionId)
            : base(choiceModel)
        {
            _choiceModel = choiceModel;
            _answerModel = answerModelModel;
            _questionId = questionId;
            _answer = choiceModel.Index == answerModelModel.Choice;
        }

        public string Text
        {
            get { return _choiceModel.Option; }
            set
            {
                if (_choiceModel.Option != value)
                {
                    _choiceModel.Option = value;
                    OnNotifyPropertyChanged("Text");
                }
            }
        }

        public int Index
        {
            get { return _choiceModel.Index; }
        }

        public bool Answer
        {
            get { return _answer; }
            set
            {
                if (_answer != value)
                {
                    _answer = value;
                    if (value)
                    {
                        _answerModel.Choice = _choiceModel.Index;
                    }
                    OnNotifyPropertyChanged("Answer");
                }
            }
        }

        public string Group
        {
            get { return _questionId.ToString(); }
        }
    }
}
