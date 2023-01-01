using BusinessLayer.Interface;
using CommonLayer;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class LabelBusiness : ILabelBusiness
    {
        private readonly ILabelRepository labelRepository;

        public LabelBusiness(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }

        public LabelEntity CreateLabel(LabelModel lable, long noteId, long userId)
        {
            try
            {
                return labelRepository.CreateLabel(lable, noteId,userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public bool RenameLable(string OldLableName, string NewLableName, long userId)
        {
            try
            {
                return labelRepository.RenameLable(OldLableName, NewLableName, userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool RemoveLable(string lableName, long userId)
        {
            try
            {
                return labelRepository.RemoveLable(lableName, userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<LabelEntity> GetAllLabels(long userId)
        {
            try
            {
                return labelRepository.GetAllLabels(userId);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        //public async Task<List<LabelEntity>> GetAllLabelByRedisCache()
        //{
        //    try
        //    {
        //        return await labelRepository.GetAllLabelByRedisCache();
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
