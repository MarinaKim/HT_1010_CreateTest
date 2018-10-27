namespace CreateTest.Lib.model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Answers
    {
        [Key]
        public int AnswerId { get; set; }

        [Required]
        [StringLength(255)]
        public string ContentAnswer { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }

        public double? AnswerBal { get; set; }

        public virtual Questions Questions { get; set; }
    }
}
