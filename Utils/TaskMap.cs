using CRUD_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUD_Project.Utils
{
    public class TaskMap : IEntityTypeConfiguration<TaskModel>
    {
        public void Configure(EntityTypeBuilder<TaskModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.title).IsRequired();
            builder.Property(x => x.status).IsRequired();
            builder.Property(x => x.dateCreated).IsRequired();
            builder.Property(x => x.dateLimit).IsRequired();
        }
    }
}
