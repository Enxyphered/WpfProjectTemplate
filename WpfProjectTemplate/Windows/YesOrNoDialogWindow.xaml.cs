using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;
using WpfProjectTemplate.Primitives.Abstract;

namespace WpfProjectTemplate.Windows;

public partial class YesOrNoDialogWindow : Window
{
    public bool Result { get; private set; } = false;

    public YesOrNoDialogWindow()
    {
        InitializeComponent();
    }

    private void Yes_Click(object sender, RoutedEventArgs e)
    {
        Result = true;
        Close();
    }

    private void No_Click(object sender, RoutedEventArgs e)
    {
        Result = false;
        Close();
    }
}

public class YesOrNoDialog : IYesOrNoDialog
{
    private readonly YesOrNoDialogWindow _window;
    private readonly YesOrNoDialogViewModel _viewModel;

    public YesOrNoDialog(YesOrNoDialogWindow window, YesOrNoDialogViewModel viewModel)
    {
        _window = window;
        _viewModel = viewModel;
    }

    public bool AskQuestion(string title, string message)
    {
        _viewModel.Title = title;
        _viewModel.Message = message;
        _window.DataContext = _viewModel;
        _window.ShowDialog();
        return _window.Result;
    }
}

[ObservableObject]
public partial class YesOrNoDialogViewModel
{
    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string _message;
}