using MedicalRecordsUI.Services;

namespace MedicalRecordsUI.ViewModels;

public sealed class MainViewModel
{
    private readonly INavigationService _navigation;

    public ICommand NavigatePerson { get; }
    public ICommand NavigateDoctor { get; }
    public ICommand NavigateMedicalRecord { get; }

    public MainViewModel(INavigationService navigation)
    {
        _navigation = navigation;

        NavigatePerson = new RelayCommand(
            () => _navigation.Navigate(Routes.Person));

        NavigateDoctor = new RelayCommand(
            () => _navigation.Navigate(Routes.Doctor));

        NavigateMedicalRecord = new RelayCommand(
            () => _navigation.Navigate(Routes.MedicalRecord));
    }
}
