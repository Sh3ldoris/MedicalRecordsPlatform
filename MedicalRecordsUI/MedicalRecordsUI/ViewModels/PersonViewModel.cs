using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using MedicalRecordsUI.Services;

namespace MedicalRecordsUI.ViewModels;

public partial class PersonViewModel : ObservableValidator
{
    private readonly IMedicalRecordsApi _api;

    [ObservableProperty] private ObservableCollection<Person> _persons = new();

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "First name is required")]
    private string _firstName = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Last name is required")]
    private string _lastName = string.Empty;

    [ObservableProperty] private DateTime _dateOfBirth = DateTime.Today.AddYears(-30);

    [ObservableProperty] private string _address = string.Empty;

    [ObservableProperty] private string _phoneNumber = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    private string _email = string.Empty;

    [ObservableProperty] private bool _isLoading;

    [ObservableProperty] private string _errorMessage = string.Empty;

    [ObservableProperty] private Person? _selectedPerson;

    [ObservableProperty] private bool _isEditing;

    [RequiresUnreferencedCode("ObservableValidator uses reflection for DataAnnotations validation.")]
    public PersonViewModel(IMedicalRecordsApi api)
    {
        _api = api;
    }

    [RelayCommand]
    private async Task LoadPersonsAsync()
    {
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
                DateOfBirth = DateOfBirth,
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
        DateOfBirth = person.DateOfBirth;
        Address = person.Address ?? string.Empty;
        PhoneNumber = person.PhoneNumber ?? string.Empty;
        Email = person.Email ?? string.Empty;
        IsEditing = true;
    }

    [RelayCommand]
    private async Task DeletePersonAsync(Person person)
    {
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
    private void CancelEdit()
    {
        ClearForm();
    }

    private void ClearForm()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        DateOfBirth = DateTime.Today.AddYears(-30);
        Address = string.Empty;
        PhoneNumber = string.Empty;
        Email = string.Empty;
        SelectedPerson = null;
        IsEditing = false;
        ErrorMessage = string.Empty;
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
}
