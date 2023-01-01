using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer
{
    public class NoteModel
    {
        public string Note_Title { get; set; }
        public string Description { get; set; }
        public DateTime? Reminder { get; set; }
        public string? Background_Color { get; set; }
        public string? Image { get; set; }
        public bool Archive { get; set; }
        public bool Pin { get; set; }
        public bool Trash { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
