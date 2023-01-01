using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepoLayer.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LableID { get; set; }

        public string LableName { get; set; }

        [ForeignKey("User")]
        public long UserID { get; set; }

        [ForeignKey("Note")]
        public long Note_ID { get; set; }
        [JsonIgnore]

        public virtual UserEntity User { get; set; }
        [JsonIgnore]
        public virtual NoteEntity Note { get; set;}


    }
}
