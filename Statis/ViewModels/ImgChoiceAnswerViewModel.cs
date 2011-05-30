using System.Collections.ObjectModel;
using Statis.StatisServices;

namespace Statis.ViewModels
{
    public class ImgChoiceAnswerViewModel : ImgChoiceQuestionViewModel
    {
        private readonly ObservableCollection<TextChoiceViewModelForAnswer> _choices = new ObservableCollection<TextChoiceViewModelForAnswer>();
        
        public ImgChoiceAnswerViewModel(ImgChoiceQuestion question, SingleChoiceAnswer answer): base(question)
        {
            foreach (var choice in question.ChoiceList)
            {
                _choices.Add(new TextChoiceViewModelForAnswer((TextChoice) choice, answer, question.QuestionId));
            }
        }

        public ObservableCollection<TextChoiceViewModelForAnswer> AnswerChoices
        {
            get { return _choices; }
        }
    }
}
