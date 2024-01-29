using System.Collections.ObjectModel;
using System.Windows.Input;
using H3_Avalonia.Models;
using H3_Avalonia.Utilities;
using H3_Avalonia.Views.UserControls;
using ReactiveUI;

namespace H3_Avalonia.ViewModels;

public class GroupViewModel : ViewModelBase
{
    private ObservableCollection<Group> _groups = new(new GroupManager().GetGroups());
    public ObservableCollection<Group> Groups
    {
        get => _groups;
        set => this.RaiseAndSetIfChanged(ref _groups, value);
    }

    private Group? _selectedGroup;
    public Group? SelectedGroup {
        get => _selectedGroup;
        set => this.RaiseAndSetIfChanged(ref _selectedGroup, value);
    }

    public ICommand CreateGroupCommand { get; }

    public GroupViewModel()
    {
        CreateGroupCommand = ReactiveCommand.Create(() => ContentArea.Navigate(new CreateGroupView()));
    }
}
