using CommonLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Services
{
    public class LabelRepository :ILabelRepository
    {
        public DBContext dBContext;

        private readonly IConfiguration config;

        public LabelRepository(DBContext dBContext, IConfiguration config)
        {
            this.dBContext = dBContext;
            this.config = config;
        }

        public List<LabelEntity> GetAllLabels(long userId)
        {
            try
            {
                var result = this.dBContext.LabelTable.Where(e => e.UserID == userId).ToList();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
        public LabelEntity CreateLabel(LabelModel lable,long noteId,long userId)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity();             
                    labelEntity.LableName = lable.LableName;
                    labelEntity.UserID = userId;
                    labelEntity.Note_ID = noteId;
                    dBContext.LabelTable.Add(labelEntity);
                    dBContext.SaveChanges();
                    return labelEntity;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RenameLable(string OldLableName,string NewLableName,long userId)
        {
            try
            {
                var result = this.dBContext.LabelTable.FirstOrDefault(e => e.UserID == userId && e.LableName == OldLableName);
                if (result != null)
                {
                  result.LableName=NewLableName;
                    dBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;   
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool RemoveLable(string lableName,long userId)
        {
            try
            {
                var result = dBContext.LabelTable.FirstOrDefault(e => e.LableName==lableName && e.UserID == userId);
                if (result != null)
                {
                    dBContext.LabelTable.Remove(result);
                    dBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }catch(Exception ex)
            {
                throw ex;
            }
        }
        //public async Task<List<LabelEntity>> GetAllLabelByRedisCache()
        //{
        //    try
        //    {
        //        return await this.dBContext.LabelTable.ToListAsync();
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
