using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using H3_Avalonia.Models;
using ReactiveUI;

namespace H3_Avalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public static Group? SelectedGroup { get; set; }
    public static User? SelectedUser { get; set; }
}
