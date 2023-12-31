﻿using API_Client.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Client.Database;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }


    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserRoleEntity> UserRoles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();

        // modelBuilder.Entity<User>() // ?
        // .HasMany(e => e.Roles)
        // .WithMany(e => e.Users).UsingEntity(
        //     "UserRole",
        //     l => l.HasOne(typeof(Role)).WithMany().HasForeignKey("RoleId").HasPrincipalKey(nameof(Role.Id)),
        //     r => r.HasOne(typeof(User)).WithMany().HasForeignKey("UserId").HasPrincipalKey(nameof(User.Id)),
        //     j => j.HasKey("UserId", "RoleId"));

        modelBuilder.Entity<UserEntity>() // Many-to-many with class for join entity
        .HasMany(e => e.Roles)
        .WithMany(e => e.Users)
        .UsingEntity<UserRoleEntity>(
            l => l.HasOne<RoleEntity>().WithMany().HasForeignKey(e => e.RoleId),
            r => r.HasOne<UserEntity>().WithMany().HasForeignKey(e => e.UserId));
    }


    public RoleEntity? GetRoleById(int id)
    {
        return Roles.FirstOrDefault(r => r.Id == id);
    }


    public List<RoleEntity> GetRolesWithUserId(int id)
    {
        return Roles.Where(r => r.Users.Any(u => u.Id == id)).ToList();
    }
}
