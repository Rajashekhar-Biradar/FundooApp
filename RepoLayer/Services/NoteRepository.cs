using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Services
{
    public class NoteRepository : INoteRepository
    {
        DBContext dBContext;
        private readonly IConfiguration config;

        public NoteRepository(DBContext dBContext, IConfiguration config)
        {
            this.dBContext = dBContext;
            this.config = config;
        }

        public List<NoteEntity> GetAllNotes(long userId)
        {
            try
            {
                var result = dBContext.noteTable.Where(e => e.UserID == userId).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NoteEntity CreateNote(NoteModel note, long UserID)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.Note_Title = note.Note_Title;
                noteEntity.Description = note.Description;
                noteEntity.Reminder = note.Reminder;
                noteEntity.Background_Color = note.Background_Color;
                noteEntity.Image = note.Image;
                noteEntity.Archive = note.Archive;
                noteEntity.Pin = note.Pin;
                noteEntity.Trash = note.Trash;
                noteEntity.Created = note.Created;
                noteEntity.Updated = note.Updated;
                noteEntity.UserID = UserID;
                dBContext.noteTable.Add(noteEntity);
                int result = dBContext.SaveChanges();
                if (result > 0)
                {
                    return noteEntity;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateNotes(long userId, long NoteId, NoteModel note)
        {
            try
            {
                var result = dBContext.noteTable.FirstOrDefault(e => e.UserID == userId && e.Note_ID == NoteId);

                if (result != null)
                {
                    if (note.Note_Title != null)
                    {
                        result.Note_Title = note.Note_Title;
                    }
                    if (note.Description != null)
                    {
                        result.Description = note.Description;
                    }
                    result.Updated = DateTime.Now;
                    dBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RemoveNotes(long userId, long NoteId)
        {
            try
            {
                var result = dBContext.noteTable.FirstOrDefault(e => e.UserID == userId && e.Note_ID == NoteId);
                if (result != null)
                {
                    dBContext.noteTable.Remove(result);
                    dBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NoteEntity ChangeColor(long noteId, string color)
        {
            try
            {
                NoteEntity note = this.dBContext.noteTable.FirstOrDefault(e => e.Note_ID == noteId);
                if (note != null)
                {
                    note.Background_Color = color;
                    dBContext.SaveChanges();
                    return note;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsPinOrNot(long noteId)
        {
            try
            {
                var note = this.dBContext.noteTable.FirstOrDefault(e => e.Note_ID == noteId);
                if (note.Pin == true)
                {
                    note.Pin = false;
                    this.dBContext.SaveChanges();
                    return false;
                }
                else
                {
                    note.Pin = true;
                    this.dBContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool IsTrashOrNot(long noteId)
        {
            try
            {
                var note = this.dBContext.noteTable.FirstOrDefault(e => e.Note_ID == noteId);
                if (note.Trash == true)
                {
                    note.Trash = false;
                    this.dBContext.SaveChanges();
                    return false;
                }
                else
                {
                    note.Trash = true;
                    this.dBContext.SaveChanges();
                    return true;
                }
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
                var note = this.dBContext.noteTable.FirstOrDefault(e => e.Note_ID == noteId);
                if (note.Archive == true)
                {
                    note.Archive = false;
                    this.dBContext.SaveChanges();
                    return false;
                }
                else
                {
                    note.Archive = true;
                    this.dBContext.SaveChanges();
                    return true;
                }
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
                var result = this.dBContext.noteTable.FirstOrDefault(e => e.Note_ID == NoteId && e.UserID == userId);
                if (result != null)
                {
                    Account account = new Account(
                        this.config["CloudinarySettings:CloudName"],
                        this.config["CloudinarySettings:ApiKey"],
                        this.config["CloudinarySettings:ApiSecret"]);

                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(img.FileName, img.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    string imgagePath = uploadResult.Url.ToString();
                    result.Image = imgagePath;
                    dBContext.SaveChanges();
                    return "Image Uploaded Successfully";
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RemoveTrashForever(long NoteId)
        {
            try
            {
                var result = this.dBContext.noteTable.FirstOrDefault(e => e.Note_ID == NoteId);
                if (result.Trash == true)
                {
                    dBContext.noteTable.Remove(result);
                    this.dBContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Trash = true;
                    dBContext.SaveChanges();
                    return true;
                }
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
                return await this.dBContext.noteTable.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

    }
}
