using Microsoft.EntityFrameworkCore;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) :base(options)
        { }
        public DbSet<UserEntity> userTable { get; set; }

        public DbSet<NoteEntity> noteTable { get; set; }

        public DbSet<LabelEntity> LabelTable { get; set; }
        public DbSet<CollabEntity> CollabTable { get; set; }
    }
}
