using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;
using WpfProjectTemplate.Primitives.Abstract;

namespace WpfProjectTemplate.Windows;

public partial class OkDialogWindow : Window
{
    public OkDialogWindow()
    {
        InitializeComponent();
    }

    private void Ok_Click(object sender, RoutedEventArgs e)
        => Close();
}

public class OkDialog : IOkDialog
{
    private readonly OkDialogWindow _window;
    private readonly OkDialogViewModel _viewModel;

    public OkDialog(OkDialogWindow window, OkDialogViewModel viewModel)
    {
        _window = window;
        _viewModel = viewModel;
    }

    public void ShowMessage(string title, string message)
    {
        _viewModel.Title = title;
        _viewModel.Message = message;
        _window.DataContext = _viewModel;
        _window.ShowDialog();
    }
}

[ObservableObject]
public partial class OkDialogViewModel
{
    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string _message;
}