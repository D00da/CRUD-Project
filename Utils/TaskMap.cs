using CRUD_Project.Enums;
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
            builder.Property(x => x.Id)
                .HasColumnType("serial")
                .ValueGeneratedOnAdd();
            builder.Property(x => x.title).IsRequired();
            builder.Property(x => x.status)
                .HasColumnType("int")
                .HasConversion(
                    v => (int)v,
                    v => (State)Enum.Parse(typeof(State), v.ToString()))
                .IsRequired();
            builder.Property(x => x.dateCreated).IsRequired();
            builder.Property(x => x.dateLimit).IsRequired();
            builder.Property(x => x.userId);
            builder.HasOne(x => x.user);
        }
    }
}
