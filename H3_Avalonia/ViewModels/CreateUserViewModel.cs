using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using H3_Avalonia.Models;
using H3_Avalonia.Utilities;
using H3_Avalonia.Views.UserControls;
using ReactiveUI;

namespace H3_Avalonia.ViewModels;

public class CreateUserViewModel : ViewModelBase
{
    private string _userName = string.Empty;
    public string UserName
    {
        get => _userName;
        set => this.RaiseAndSetIfChanged(ref _userName, value);
    }

    private string _password = string.Empty;
    public string Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }

    private string _confirmPassword = string.Empty;
    public string ConfirmPassword
    {
        get => _confirmPassword;
        set => this.RaiseAndSetIfChanged(ref _confirmPassword, value);
    }

    private ObservableCollection<Group> _groups = new(new GroupManager().GetGroups());
    public ObservableCollection<Group> Groups
    {
        get => _groups;
        set => this.RaiseAndSetIfChanged(ref _groups, value);
    }

    private Group? _selectedGroup = new GroupManager().GetGroups().FirstOrDefault();
    public Group? SelectedGroup
    {
        get => _selectedGroup;
        set => this.RaiseAndSetIfChanged(ref _selectedGroup, value);
    }

    public ICommand CreateUserCommand { get; }
    public ICommand CancelCommand { get; }

    public CreateUserViewModel()
    {
        CreateUserCommand = ReactiveCommand.Create(CreateUser);
        CancelCommand = ReactiveCommand.Create(() => ContentArea.Navigate(new GroupView()));
    }

    private void CreateUser()
    {
        if (string.IsNullOrWhiteSpace(UserName))
        {
            Console.WriteLine("User name cannot be empty!");

            return;
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            Console.WriteLine("Password cannot be empty!");

            return;
        }

        if (string.IsNullOrWhiteSpace(ConfirmPassword))
        {
            Console.WriteLine("Confirm password cannot be empty!");

            return;
        }

        if (Password != ConfirmPassword)
        {
            Console.WriteLine("Passwords do not match!");

            return;
        }

        if (SelectedGroup is null)
        {
            Console.WriteLine("Selected group cannot be null!");

            return;
        }

        using var db = new GroupManager();

        db.Database.EnsureCreated();

        try
        {
            var user = new User { Name = UserName, Password = Password, GroupId = SelectedGroup.GroupId };

            db.Users.Add(user);
            db.SaveChanges();

            var group = db.Groups
                .OrderBy(g => g.GroupId)
                .First();

            group.Users.Add(user);
            db.SaveChanges();

            Console.WriteLine("User saved successfully.");

            ContentArea.Navigate(new GroupView());
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while saving the user: {e}");
        }
    }
}
