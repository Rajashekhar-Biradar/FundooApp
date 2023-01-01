using BusinessLayer.Interface;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class CollabBusiness : ICollabBusiness
    {
        private readonly ICollabRepository collabRepository;

        public CollabBusiness(ICollabRepository collabRepository)
        {
            this.collabRepository = collabRepository;
        }

        public CollabEntity CreateCollab(long noteId, String email, long userID)
        {
            try
            {
                return collabRepository.CreateCollab(noteId, email,userID);

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
                return collabRepository.RemoveCollab(collabId);

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
                return collabRepository.GetAllCollab(noteId);
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }
        //public async Task<List<CollabEntity>> GetAllCollabByRedisCache()
        //{
        //    try
        //    {
        //        return await collabRepository.GetAllCollabByRedisCache();
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
