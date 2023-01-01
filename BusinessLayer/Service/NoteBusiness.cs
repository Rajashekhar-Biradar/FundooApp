using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class NoteBusiness : INoteBusiness
    {
        private readonly INoteRepository noteRepository;

        public NoteBusiness(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public NoteEntity CreateNote(NoteModel note, long UserID)
        {
            try
            {
                return noteRepository.CreateNote(note, UserID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public List<NoteEntity> GetAllNotes(long userId)
        {
            try
            {
                return noteRepository.GetAllNotes(userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateNotes(long userId, long NoteId, NoteModel note)
        {
            try
            {
                return noteRepository.UpdateNotes(userId,NoteId,note);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool RemoveNotes(long userId, long NoteId)
        {
            try
            {
                return noteRepository.RemoveNotes(userId,NoteId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public NoteEntity ChangeColor(long noteId, string color)
        {
            try
            {
                return noteRepository.ChangeColor(noteId,color);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool IsPinOrNot(long noteId)
        {
            try
            {
                return noteRepository.IsPinOrNot(noteId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool IsTrashOrNot(long noteId)
        {
            try
            {
                return noteRepository.IsTrashOrNot(noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsArchiveOrNot(long noteId)
        {
            try
            {
                return noteRepository.IsArchiveOrNot(noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string UploadImage(long NoteId, long userId, IFormFile img)
        {
            try
            {
                return noteRepository.UploadImage(NoteId, userId, img);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool RemoveTrashForever(long NoteId)
        {
            try
            {
                return noteRepository.RemoveTrashForever(NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<NoteEntity>> GetAllNoteByRedisCache()
        {
            try
            {
                return await noteRepository.GetAllNoteByRedisCache();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
