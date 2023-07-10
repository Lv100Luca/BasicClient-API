using API_Client.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Client.Database;

public class ApiDbContext : DbContext // todo what is this class doing ?
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }


    public virtual DbSet<UserEntity> Users { get; set; }
    public virtual DbSet<UserRoleEntity> UserRoles { get; set; }
    public virtual DbSet<RoleEntity> Roles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRoleEntity>()
        .HasKey(ur => new
        {
            ur.UserId,
            ur.RoleId
        });

        modelBuilder.Entity<UserRoleEntity>()
        .HasOne(ur => ur.User)
        .WithMany(u => u.UserRole)
        .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRoleEntity>()
        .HasOne(ur => ur.Role)
        .WithMany(r => r.UserRole)
        .HasForeignKey(ur => ur.RoleId);

        // Rest of the method code...
    }
    // {
    //     modelBuilder.Entity<UserEntity>(entity =>
    //     {
    //         entity.HasKey(e => e.Id);
    //     });
    //
    //     modelBuilder.Entity<RoleEntity>(entity =>
    //     {
    //         entity.HasKey(e => e.Id);
    //
    //     });
    //
    //     modelBuilder.Entity<UserRoleEntity>(entity => // todo what does this do?
    //     {
    //         entity
    //         .HasNoKey()
    //         .HasOne(one => one.User)
    //         .WithMany(many => many.UserRole)
    //         .HasForeignKey(fk => fk.fk_user_id)
    //         // .OnDelete(DeleteBehavior.Restrict) // todo what does this do?
    //         // .HasConstraintName("fk_user_userrole") // todo what does this do?
    //         ;
    //     });
    //
    //     modelBuilder.Entity<UserRoleEntity>(entity => // todo what does this do?
    //     {
    //         entity
    //         .HasNoKey()
    //         .HasOne(one => one.Role)
    //         .WithMany(many => many.UserRole)
    //         .HasForeignKey(fk => fk.fk_role_id)
    //         // .OnDelete(DeleteBehavior.Restrict) // todo what does this do?
    //         // .HasConstraintName("fk_role_userrole") // todo what does this do?
    //         ;
    //     });
    //
    //     // todo do i need two of these?
    //     base.OnModelCreating(modelBuilder);
    // }
}
