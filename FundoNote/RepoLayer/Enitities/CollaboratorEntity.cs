using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace RepoLayer.Enitities
{
    public class CollaboratorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollaboratorId { get; set; }
        public string CollaboratorEmail { get; set; }
        [ForeignKey("Users")]
        public long UserId { get; set; }
        [JsonIgnore]
        public virtual UserEntity user { get; set; }
        [ForeignKey("Notes")]
        public long NoteId { get; set; }
        [JsonIgnore]
        public virtual NoteEntitiy Note { get; set; }
    }
}
