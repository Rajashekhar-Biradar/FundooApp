using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Services
{
    public class CollabRepository : ICollabRepository
    {
        DBContext dBContext;
        private readonly IConfiguration config;
        public CollabRepository(DBContext dBContext, IConfiguration config)
        {
            this.dBContext = dBContext;
            this.config = config;
        }

        public CollabEntity CreateCollab(long noteId, String email, long userID)
        {
            try
            {
                //    var resultEmail = this.dBContext.userTable.Where(e => e.EmailID == email).FirstOrDefault();
                //    var resultNoteId = this.dBContext.noteTable.Where(e => e.Note_ID == noteId).FirstOrDefault();
                //    if (resultEmail != null && resultNoteId != null)
                //    {
                //        CollabEntity collab = new CollabEntity();
                //        collab.CollaEmail = resultEmail.EmailID;
                //        collab.Note_ID = resultNoteId.Note_ID;
                //        collab.UserID = resultEmail.UserID;
                //        this.dBContext.CollabTable.Add(collab);
                //        dBContext.SaveChanges();
                //        return collab;

                //    }
                var result = this.dBContext.CollabTable.Where(e => e.Note_ID == noteId && e.CollaEmail == email).FirstOrDefault();
                if (result == null )
                {
                    CollabEntity collab = new CollabEntity();
                    collab.Note_ID = noteId;
                    collab.CollaEmail = email;
                    collab.UserID= userID;
                    this.dBContext.CollabTable.Add(collab);
                    dBContext.SaveChanges();
                    return collab;
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
        public bool RemoveCollab(long collabId)
        {
            try
            {
                var result = this.dBContext.CollabTable.FirstOrDefault(e => e.CollabID == collabId);

                if (result != null)
                {
                    this.dBContext.CollabTable.Remove(result);
                    this.dBContext.SaveChanges();
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
        public List<CollabEntity> GetAllCollab(long noteId)
        {
            try
            {
                var result = this.dBContext.CollabTable.Where(e => e.Note_ID == noteId).ToList();
                if (result != null)
                {
                    return result;
                }else
                {
                    return null;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }
        //public async Task<List<CollabEntity>> GetAllCollabByRedisCache()
        //{
        //    try
        //    {
        //        return await this.dBContext.CollabTable.ToListAsync();
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        
    }
}
