using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Kopator.App.Services;
using Kopator.App.ViewModels;
using Kopator.App.Views;
using Kopator.Core.Settings;

namespace Kopator.App;

public partial class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var window = new MainWindow();

            // The dialog service needs the window as its owner, so the view model is
            // built here rather than in XAML.
            window.DataContext = new MainWindowViewModel(
                new JsonSettingsStore(),
                new StorageDialogService(window));

            desktop.MainWindow = window;
        }

        base.OnFrameworkInitializationCompleted();
    }
}
