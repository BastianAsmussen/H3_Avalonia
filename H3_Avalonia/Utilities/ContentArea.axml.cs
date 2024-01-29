using Avalonia.Controls;
using H3_Avalonia.Views.UserControls;

namespace H3_Avalonia.Utilities;

public partial class ContentArea : UserControl
{
    private static ContentArea? _instance;

    public ContentArea()
    {
        InitializeComponent();
        _instance ??= this;

        Navigate(new GroupView());
    }

    public static void Navigate(UserControl? userControl)
    {
        if (_instance == null) return;
        if (userControl == null) return;

        _instance.Content = userControl;
    }
}
