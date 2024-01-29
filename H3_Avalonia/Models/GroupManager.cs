using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace H3_Avalonia.Models;

public class GroupManager : DbContext
{
    public DbSet<Group> Groups { get; set; }
    public DbSet<User> Users { get; set; }

    public string DbPath { get; }

    public GroupManager()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;

        var path = Environment.GetFolderPath(folder);

        DbPath = System.IO.Path.Join(path, "group_manager.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    public List<Group> GetGroups()
    {
        Database.EnsureCreated();

        if (Groups.Any())
            return Groups.ToList();

        // If there are no groups, create a default group.
        Groups.Add(new Group { Name = "Default", AccessLevel = AccessLevel.User });
        SaveChanges();

        return Groups.ToList();
    }
}

public enum AccessLevel
{
    User,
    Admin
}

public class Group
{
    public int GroupId { get; set; }
    public string Name { get; set; }
    public AccessLevel AccessLevel { get; set; }

    public List<User> Users { get; set; } = new();
}

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; }
}
