using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StatisServiceContracts
{
    [DataContract]
    public class Answer
    {
        string answerText { get; set; }
        bool choiceList = false;
    }
}