using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using H3_Avalonia.Models;
using H3_Avalonia.Utilities;
using H3_Avalonia.Views.UserControls;
using ReactiveUI;

namespace H3_Avalonia.ViewModels;

public class GroupDetailsViewModel : ViewModelBase
{
    private Group _group = MainWindowViewModel.SelectedGroup ?? new Group();
    public Group Group
    {
        get => _group;
        set => this.RaiseAndSetIfChanged(ref _group, value);
    }

    public string FormattedId => $"- ID: {Group.GroupId}";
    public string FormattedName => $"- Name: {Group.Name}";
    public string FormattedAccessLevel => $"- Access Level: {Group.AccessLevel}";

    public ICommand AddUserCommand { get; }
    public ICommand DeleteGroupCommand { get; }
    public ICommand BackCommand { get; }

    public GroupDetailsViewModel()
    {
        AddUserCommand = ReactiveCommand.Create(() => ContentArea.Navigate(new CreateUserView()));
        DeleteGroupCommand = ReactiveCommand.Create(DeleteGroup);
        BackCommand = ReactiveCommand.Create(() => ContentArea.Navigate(new GroupView()));
    }

    private void DeleteGroup()
    {
        using var db = new GroupManager();

        db.Groups.Remove(_group);

        try
        {
            db.SaveChanges();

            ContentArea.Navigate(new GroupView());
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to delete group: {e.Message}");
        }
    }
}
