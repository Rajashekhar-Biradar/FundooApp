using CommonLayer;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    public interface ILabelRepository
    {
        public LabelEntity CreateLabel(LabelModel lable, long noteId, long userId);
        public bool RenameLable(string OldLableName, string NewLableName, long userId);
        public bool RemoveLable(string lableName, long userId);
        public List<LabelEntity> GetAllLabels(long userId);
        //public Task<List<LabelEntity>> GetAllLabelByRedisCache();
    }
}
