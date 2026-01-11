using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using MedicalRecordsUI.Services;

namespace MedicalRecordsUI.ViewModels;

public partial class PersonViewModel : ObservableValidator
{
    public XamlRoot? XamlRoot { get; set; }
    
    private readonly IMedicalRecordsApi _api;
    private readonly ILogger<PersonViewModel> _logger;

    [ObservableProperty] private ObservableCollection<Person> _persons = new();

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "First name is required")]
    private string _firstName = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Last name is required")]
    private string _lastName = string.Empty;

    [ObservableProperty] private DateTimeOffset _dateOfBirth = DateTimeOffset.Now.Date.AddYears(-30);

    [ObservableProperty] private string _address = string.Empty;

    [ObservableProperty] private string _phoneNumber = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    private string _email = string.Empty;

    [ObservableProperty] private bool _isLoading;

    [ObservableProperty] private string _errorMessage = string.Empty;

    [ObservableProperty] private Person? _selectedPerson;
    
    [NotifyPropertyChangedFor(nameof(FormTitle))]
    [NotifyPropertyChangedFor(nameof(SaveButtonText))]
    [ObservableProperty]
    private bool _isEditing;
    [ObservableProperty] private bool _hasErrorMessage;
    [ObservableProperty] private bool _showPersonsList;
    [ObservableProperty] private bool _showEmptyState;
    [ObservableProperty] private bool _isFormVisible;
    
    public string FormTitle => IsEditing ? "Edit Person" : "Create Person";
    public string SaveButtonText => IsEditing ? "Update" : "Create";

    [RequiresUnreferencedCode("ObservableValidator uses reflection for DataAnnotations validation.")]
    public PersonViewModel(IMedicalRecordsApi api, ILogger<PersonViewModel> logger)
    {
        _api = api;
        _logger = logger;
        UpdateVisibilityStates();
    }

    partial void OnErrorMessageChanged(string value)
    {
        HasErrorMessage = !string.IsNullOrWhiteSpace(value);
        UpdateVisibilityStates();
    }

    partial void OnIsLoadingChanged(bool value)
    {
        UpdateVisibilityStates();
    }

    partial void OnPersonsChanged(ObservableCollection<Person> value)
    {
        UpdateVisibilityStates();
    }

    private void UpdateVisibilityStates()
    {
        ShowPersonsList = !IsLoading && Persons.Count > 0;
        ShowEmptyState = !IsLoading && Persons.Count == 0;
    }

    [RelayCommand]
    private async Task LoadPersonsAsync()
    {
        _logger.LogWarning("Loading persons!");
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;
            var persons = await _api.GetPersonsAsync();
            Persons = new ObservableCollection<Person>(persons);
            
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error loading persons: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task AddPersonAsync()
    {
        if (!ValidateInput())
            return;

        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            var person = new Person
            {
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth.UtcDateTime,
                Address = Address,
                PhoneNumber = PhoneNumber,
                Email = Email
            };

            if (IsEditing && SelectedPerson != null)
            {
                person.Id = SelectedPerson.Id;
                await _api.UpdatePersonAsync(person.Id, person);
            }
            else
            {
                await _api.CreatePersonAsync(person);
            }

            await LoadPersonsAsync();
            ClearForm();
            IsFormVisible = false;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error saving person: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void EditPerson(Person person)
    {
        SelectedPerson = person;
        FirstName = person.FirstName;
        LastName = person.LastName;
        DateOfBirth = new DateTimeOffset(person.DateOfBirth);
        Address = person.Address ?? string.Empty;
        PhoneNumber = person.PhoneNumber ?? string.Empty;
        Email = person.Email ?? string.Empty;
        IsEditing = true;
        IsFormVisible = true;
    }

    [RelayCommand]
    private async Task DeletePersonAsync(Person person)
    {
        _logger.LogWarning("Clicking to delete person!");
        // Confirm the deletion
        if (!await this.DisplayConfirmation("Delete person",
                $"Are you sure you want to delete {person.FirstName} {person.LastName}?"))
        {
            return;
        }
        
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;
            await _api.DeletePersonAsync(person.Id);
            await LoadPersonsAsync();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error deleting person: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void ShowForm()
    {
        ClearForm();
        IsFormVisible = true;
    }

    [RelayCommand]
    private void CancelEdit()
    {
        ClearForm();
    }

    private void ClearForm()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        DateOfBirth = DateTimeOffset.Now.Date.AddYears(-30);
        Address = string.Empty;
        PhoneNumber = string.Empty;
        Email = string.Empty;
        SelectedPerson = null;
        IsEditing = false;
        ErrorMessage = string.Empty;
        IsFormVisible = false;
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(FirstName))
        {
            ErrorMessage = "First name is required";
            return false;
        }

        if (string.IsNullOrWhiteSpace(LastName))
        {
            ErrorMessage = "Last name is required";
            return false;
        }

        if (!string.IsNullOrWhiteSpace(Email) && !Email.Contains("@"))
        {
            ErrorMessage = "Invalid email address";
            return false;
        }

        return true;
    }
    
    private async Task<bool> DisplayConfirmation(
        string title,
        string message,
        string confirmText = "Confirm",
        string cancelText = "Cancel"
    ) {
        var dialog = new ContentDialog
        {
            Title = title,
            Content = message,
            PrimaryButtonText = confirmText,
            SecondaryButtonText = cancelText,
            DefaultButton = ContentDialogButton.Secondary,
            XamlRoot = XamlRoot
        };

        var result = await dialog.ShowAsync();
        return  result == ContentDialogResult.Primary;
    }
}
