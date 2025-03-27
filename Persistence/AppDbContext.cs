using System;
using Domain;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistence;

//App DbContext is used to query and save data to the SQLite database
//This Class extends DbContext which is the main class in EF Core for working with a database
//DbContextOptions allows configuration of the database connection and settings
public class AppDbContext(DbContextOptions options): IdentityDbContext<User>(options)
{
    //DbSet represents a row in the database where each row corresponds to an Activity entity (object) from the Domain namespace
    public required DbSet<Domain.Activity> Activities { get; set; }

    public DbSet<ActivityAttendee> ActivityAttendees { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ActivityAttendee>(x => x.HasKey(a => new {a.ActivityId, a.UserId}));

        builder.Entity<ActivityAttendee>()
            .HasOne(x => x.User)
            .WithMany(x => x.Activities)
            .HasForeignKey(x => x.UserId);

        builder.Entity<ActivityAttendee>()
        .HasOne(x => x.Activity)
        .WithMany(x => x.Attendees)
        .HasForeignKey(x => x.ActivityId);
    }
}
