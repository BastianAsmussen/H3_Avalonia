using Avalonia.Controls;
using H3_Avalonia.Models;
using H3_Avalonia.Utilities;
using H3_Avalonia.ViewModels;

namespace H3_Avalonia.Views.UserControls;

public partial class GroupView : UserControl
{
    public GroupView()
    {
        InitializeComponent();
    }

    private void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count == 0) return;

        var group = (Group) e.AddedItems[0]!;
        MainWindowViewModel.SelectedGroup = group;

        ContentArea.Navigate(new GroupDetailsView());
    }
}
