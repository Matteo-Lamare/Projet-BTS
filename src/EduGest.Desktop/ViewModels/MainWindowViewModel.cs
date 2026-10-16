using CommunityToolkit.Mvvm.ComponentModel;

namespace EduGest.Desktop.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    public string WelcomeMessage => "Bienvenue dans le socle d'administration.";
}
