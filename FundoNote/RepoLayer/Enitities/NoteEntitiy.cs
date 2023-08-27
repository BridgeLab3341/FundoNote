using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace RepoLayer.Enitities
{
    public class NoteEntitiy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BGColor { get; set; }
        public bool Archive { get; set; }
        public DateTime Remainder { get; set; }
        public bool Pin { get; set; }
        public bool Trash { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        [ForeignKey("Users")]
        public long UserId { get; set; }
        [JsonIgnore]
        public virtual UserEntity user { get; set; }

    }
}
