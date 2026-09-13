using Avalonia.Controls;
using Kopator.App.ViewModels;

namespace Kopator.App.Views;

public partial class MainWindow : Window
{
    private bool _closeApproved;

    public MainWindow()
    {
        InitializeComponent();

        DataContextChanged += OnDataContextChanged;
        Closing += OnClosing;
    }

    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel)
            viewModel.CloseRequested += OnCloseRequested;
    }

    private void OnCloseRequested(object? sender, EventArgs e)
    {
        _closeApproved = true;
        Close();
    }

    private void OnClosing(object? sender, WindowClosingEventArgs e)
    {
        if (DataContext is not MainWindowViewModel viewModel)
            return;

        // A running operation keeps the window open, exactly as the WinForms version did.
        if (!_closeApproved && !viewModel.CanClose())
        {
            e.Cancel = true;
            return;
        }

        viewModel.SaveSettings();
    }
}
