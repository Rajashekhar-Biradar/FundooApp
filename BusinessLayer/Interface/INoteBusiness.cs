using CommonLayer;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INoteBusiness
    {
        public NoteEntity CreateNote(NoteModel note, long UserID);
        public List<NoteEntity> GetAllNotes(long userId);
        public bool UpdateNotes(long userId, long NoteId, NoteModel note);
        public bool RemoveNotes(long userId, long NoteId);
        public NoteEntity ChangeColor(long noteId, string color);
        public bool IsPinOrNot(long noteId);
        public bool IsTrashOrNot(long noteId);
        public bool IsArchiveOrNot(long noteId);
        public string UploadImage(long NoteId, long userId, IFormFile img);
        public bool RemoveTrashForever(long NoteId);
        public Task<List<NoteEntity>> GetAllNoteByRedisCache();
    }
}
