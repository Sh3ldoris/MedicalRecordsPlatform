using MedicalRecordsUI.Services;
using MedicalRecordsUI.ViewModels;

namespace MedicalRecordsUI;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
        
        // Get services from the application host
        var services = App.Services;
        if (services is null)
        {
            throw new InvalidOperationException("Application services are not available");
        }
        
        // Create NavigationService with the ContentFrame
        var navigation = new NavigationService(ContentFrame);
        
        // Create MainViewModel with NavigationService
        var mainViewModel = new MainViewModel(navigation);
        DataContext = mainViewModel;

        navigation.Navigate(Routes.Person);
    }
}
