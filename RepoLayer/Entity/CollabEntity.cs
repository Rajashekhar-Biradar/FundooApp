using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Entity
{
    public class CollabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabID {  get; set; }      
        public string CollaEmail { get; set; }
        public DateTime? Updatedata { get; set; }

        [ForeignKey("User")]
        public long UserID { get; set; }

        [ForeignKey("Note")]
        public long Note_ID { get; set; }

        public virtual UserEntity? User { get; set; }

        public virtual NoteEntity? Note { get; set; }


    }
}
