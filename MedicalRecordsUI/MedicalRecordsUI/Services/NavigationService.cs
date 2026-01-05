using MedicalRecordsUI.Views;

namespace MedicalRecordsUI.Services;

public static class Routes
{
    public const string Person = nameof(PersonPage);
    public const string Doctor = nameof(DoctorPage);
    public const string MedicalRecord = nameof(MedicalRecordPage);
}


public sealed class NavigationService : INavigationService
{
    private readonly Frame _frame;

    public NavigationService(Frame frame)
    {
        _frame = frame;
    }

    public void Navigate(string route)
    {
        var pageType = route switch
        {
            Routes.Person => typeof(PersonPage),
            Routes.Doctor => typeof(DoctorPage),
            Routes.MedicalRecord => typeof(MedicalRecordPage),
            _ => throw new InvalidOperationException($"Unknown route: {route}")
        };

        _frame.Navigate(pageType);
    }
}

