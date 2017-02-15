using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StackUndertow.Models
{
    public class Vote
    {
        public int Id { get; set; }

        public int AnswerId{ get; set; }
        [ForeignKey("AnswerId")]
        public virtual Answer Answer { get; set; }

        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual ApplicationUser Owner { get; set; }

    }
}