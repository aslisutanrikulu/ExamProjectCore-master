using System;
using System.Collections.Generic;
using System.Text;

namespace ExamProjectCore.Entities
{
    public class Option
    {
        public int OptionId { get; set; }
        public string OptionName { get; set; }
        public string OptionContent { get; set; }
        public bool CorrectOption { get; set; }
        public int QuestionId { get; set; }
    }
}
