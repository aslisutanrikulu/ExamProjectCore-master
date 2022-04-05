using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExamProjectCore.Entities
{
    public class CorrectAnswer
    {
        [Key]
        public int CorrectId { get; set; }
        public string Correct { get; set; }
        public int OptionId { get; set; }
    }
}
