using BossAz;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ConsoleApp2.Models;

public class Employer
{
    public int EmployerId { get; set; }

   
    public Employer(int employerId, string employerName, string employerSurname, string employerPhone, int employerAge, string employerUserName, string employerEmail, string employerPassword)
    {
        
        EmployerName = employerName;
        EmployerSurname = employerSurname;
        EmployerPhone = employerPhone;
        EmployerAge = employerAge;
        EmployerUserName = employerUserName;
        EmployerEmail = employerEmail;
        EmployerPassword = employerPassword;
        EmployerId = employerId;

        EmployerVacancies = new();
        PostedCv = new();
    }
    public string EmployerName
    {
        get => _employerName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(EmployerName), "EmployerName is required");
            _employerName = value;
        }
    }
    private string _employerName;

    public string EmployerSurname
    {
        get => _employerSurname;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(EmployerSurname), "EmployerSurname is required");
            _employerSurname = value;
        }
    }
    private string _employerSurname;

    public string EmployerPhone
    {
        get => _employerPhone;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(EmployerPhone), "EmployerPhone is required");
            if (!Regex.IsMatch(value, @"^[0-9]{10}$"))
                throw new ArgumentException("EmployerPhone must be a 10-digit number", nameof(EmployerPhone));
            _employerPhone = value;
        }
    }
    private string _employerPhone;

    public int EmployerAge
    {
        get => _employerAge;
        set
        {
            if (value < 18 || value > 99)
                throw new ArgumentOutOfRangeException(nameof(EmployerAge), "EmployerAge must be between 18 and 99");
            _employerAge = value;
        }
    }
    private int _employerAge;

    public List<Vacancy> EmployerVacancies { get; set; }

    public string EmployerUserName
    {
        get => _employerUserName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(EmployerUserName), "EmployerUserName is required");
            _employerUserName = value;
        }
    }
    private string _employerUserName;

    public string EmployerEmail
    {
        get => _employerEmail;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(EmployerEmail), "EmployerEmail is required");
            if (!new EmailAddressAttribute().IsValid(value))
                throw new ArgumentException("Invalid email format", nameof(EmployerEmail));
            _employerEmail = value;
        }
    }
    private string _employerEmail;

    public string EmployerPassword
    {
        get => _employerPassword;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(EmployerPassword), "EmployerPassword is required");
            _employerPassword = value;
        }
    }
    private string _employerPassword;

    public List<CV> PostedCv { get; set; }
}
