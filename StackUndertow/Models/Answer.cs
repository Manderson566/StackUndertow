using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StackUndertow.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }

        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }

        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual  ApplicationUser Owner { get; set; }
    }
}
