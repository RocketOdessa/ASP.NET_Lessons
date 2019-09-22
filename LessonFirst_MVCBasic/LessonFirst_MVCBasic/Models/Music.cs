using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LessonFirst_MVCBasic.Models
{
    public class Music
    {
        public int id { get; set; }

        public string AlbumName { get; set; }

        public string Singer { get; set; }

        public List<string> Songs { get; set; }
    }
}