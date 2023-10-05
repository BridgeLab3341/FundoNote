using Microsoft.EntityFrameworkCore;
using RepoLayer.Enitities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Context
{
    public class NewFundoContext : DbContext
    {
        public NewFundoContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<NoteEntitiy> Notes { get; set; }
        public DbSet<CollaboratorEntity> Collaborator { get; set; }
        public DbSet<LabelEntity> Labels { get; set; }
    }
}
