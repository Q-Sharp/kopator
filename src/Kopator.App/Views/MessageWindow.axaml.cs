using Avalonia.Controls;

namespace Kopator.App.Views;

/// <summary>
/// A minimal replacement for WinForms' MessageBox, which Avalonia has no equivalent for.
/// </summary>
public partial class MessageWindow : Window
{
    public MessageWindow()
    {
        InitializeComponent();
        OkButton.Click += (_, _) => Close();
    }

    public MessageWindow(string title, string message)
        : this()
    {
        Title = title;
        MessageText.Text = message;
    }
}
