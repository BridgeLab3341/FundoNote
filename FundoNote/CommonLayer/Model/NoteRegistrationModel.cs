using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class NoteRegistrationModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string BGColor { get; set; }
        public bool Archive { get; set; }
        public DateTime Remainder { get; set; }
        public bool Pin { get; set; }
        public bool Trash { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
