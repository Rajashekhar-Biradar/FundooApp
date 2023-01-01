using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICollabBusiness
    {
        public CollabEntity CreateCollab(long noteId, String email, long userID);
        public bool RemoveCollab(long collabId);
        public List<CollabEntity> GetAllCollab(long noteId);
       // public Task<List<CollabEntity>> GetAllCollabByRedisCache();
    }
}
