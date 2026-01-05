using MedicalRecordsUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalRecordsUI.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class PersonPage : Page
{
    public PersonPage()
    {
        this.InitializeComponent();
        
        // Get services from the application host
        var services = App.Services;
        if (services is not null)
        {
            var viewModel = services.GetRequiredService<PersonViewModel>();
            DataContext = viewModel;
            
            // Load persons when page is loaded
            Loaded += async (s, e) => await viewModel.LoadPersonsCommand.ExecuteAsync(null);
        }
    }
}

