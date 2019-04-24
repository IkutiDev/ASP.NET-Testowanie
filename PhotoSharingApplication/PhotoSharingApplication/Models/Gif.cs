using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoSharingApplication.Models
{
    public class Gif
    {
        public int GifID { get; set; }
        [Required]
        public string Title { get; set; }
        [DisplayName("Gif")]
        [MaxLength]
        public byte[] GifFile { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [MaxLength]
        public TimeSpan Length;
        [DataType(DataType.DateTime)]
        [DisplayName("Created Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        public string UserName { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}