using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Entity
{
    public class NoteEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long Note_ID { get; set; }
        public string Note_Title { get; set; }
        public string Description { get; set;}
        public DateTime? Reminder { get; set; }
        public string? Background_Color { get; set; }
        public string? Image { get; set; }
        public bool Archive { get; set; }
        public bool Pin { get; set; }
        public bool Trash { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set;}

        [ForeignKey("NotesTable")]

        public long UserID { get; set; }

        public virtual UserEntity User { get; set; }

        


            

       
    }
}
