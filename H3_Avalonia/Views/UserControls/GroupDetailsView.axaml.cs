using Avalonia.Controls;
using H3_Avalonia.Models;
using H3_Avalonia.ViewModels;

namespace H3_Avalonia.Views.UserControls;

public partial class GroupDetailsView : UserControl
{
    private Group _group = MainWindowViewModel.SelectedGroup ?? new Group();

    public GroupDetailsView()
    {
        InitializeComponent();
    }
}