using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StackUndertow.Models
{
    public class ImageUploadViewModel
    {
        [Required]
        public string Caption { get; set; }

        [Required]
        public HttpPostedFile File { get; set; }
    }
    public class ImageUpload
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string File { get; set; }
        public string OwnerId { get; set; }
        public bool ProfilePic { get; set; }

        [ForeignKey("OwnerId")]
        public virtual ApplicationUser Owner { get; set; }

        public string FilePath
        {
            get
            {
                return $"/Uploads/{File}";
            }

        }
    }
}