﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace Entities.Concrete
{
    public class UsersConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> entity)
        {
            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created")
                .HasDefaultValueSql("(getdate())")
                .HasAnnotation("Relational:ColumnType", "datetime");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("firstName");

            entity.Property(e => e.GroupId)
                .HasColumnName("groupId")
                .HasDefaultValueSql("((0))");

            entity.Property(e => e.IsActive)
                .HasColumnName("isActive")
                .HasDefaultValueSql("((0))");

            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("lastName");

            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasAnnotation("Relational:ColumnType", "datetime");

            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");

            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");

            entity.Property(e => e.Picture)
                .HasMaxLength(50)
                .HasColumnName("picture");

            entity.Property(e => e.UserTypeId)
                .HasColumnName("userTypeId")
                .HasDefaultValueSql("((0))");
        }
    }
}