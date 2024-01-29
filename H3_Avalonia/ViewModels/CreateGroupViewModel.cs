using System;
using System.Linq;
using System.Windows.Input;
using H3_Avalonia.Models;
using H3_Avalonia.Utilities;
using H3_Avalonia.Views.UserControls;
using ReactiveUI;

namespace H3_Avalonia.ViewModels;

public class CreateGroupViewModel : ViewModelBase
{
    private string _groupName = string.Empty;
    public string GroupName {
        get => _groupName;
        set => this.RaiseAndSetIfChanged(ref _groupName, value);
    }

    private AccessLevel _selectedAccessLevel = AccessLevel.User;
    public AccessLevel SelectedAccessLevel {
        get => _selectedAccessLevel;
        set => this.RaiseAndSetIfChanged(ref _selectedAccessLevel, value);
    }

    public AccessLevel[] AccessLevels { get; } = Enum.GetValues(typeof(AccessLevel)).Cast<AccessLevel>().ToArray();

    public ICommand CreateGroupCommand { get; }

    public CreateGroupViewModel()
    {
        CreateGroupCommand = ReactiveCommand.Create(CreateGroup);
    }

    private void CreateGroup()
    {
        if (string.IsNullOrWhiteSpace(GroupName))
        {
            Console.WriteLine("Group name cannot be empty!");

            return;
        }

        using var db = new GroupManager();

        db.Database.EnsureCreated();
        db.Groups.Add(new Group { Name = GroupName, AccessLevel = SelectedAccessLevel });

        try
        {
            db.SaveChanges();

            Console.WriteLine("Group saved successfully.");

            ContentArea.Navigate(new GroupView());
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while saving the group: {e.Message}");
        }
    }
}
