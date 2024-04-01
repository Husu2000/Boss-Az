
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ConsoleApp2.Models;

public class Worker
{
    public int WorkerId { get; set; }

    public Worker(int workerId, string workerName, string workerSurname, string workerPhone, int workerAge, string workerUserName, string workerEmail, string workerPassword)
    {
        WorkerName = workerName;
        WorkerSurname = workerSurname;
        WorkerPhone = workerPhone;
        WorkerAge = workerAge;
        WorkerUserName = workerUserName;
        WorkerEmail = workerEmail;
        WorkerPassword = workerPassword;
        WorkerId = workerId;
    }
    public string WorkerName
    {
        get => _workerName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(WorkerName), "WorkerName is required");
            if (value.Length < 2 || value.Length > 50)
                throw new ArgumentException("WorkerName must be between 2 and 50 characters", nameof(WorkerName));
            _workerName = value;
        }
    }
    private string _workerName;

    public string WorkerSurname
    {
        get => _workerSurname;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(WorkerSurname), "WorkerSurname is required");
            if (value.Length < 2 || value.Length > 50)
                throw new ArgumentException("WorkerSurname must be between 2 and 50 characters", nameof(WorkerSurname));
            _workerSurname = value;
        }
    }
    private string _workerSurname;


    public string WorkerPhone
    {
        get => _workerPhone;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(WorkerPhone), "WorkerPhone is required");
            if (!Regex.IsMatch(value, @"^[0-9]{10}$"))
                throw new ArgumentException("WorkerPhone must be a 10-digit number", nameof(WorkerPhone));
            _workerPhone = value;
        }
    }
    private string _workerPhone;

    public int WorkerAge
    {
        get => _workerAge;
        set
        {
            if (value < 18 || value > 99)
                throw new ArgumentOutOfRangeException(nameof(WorkerAge), "WorkerAge must be between 18 and 99");
            _workerAge = value;
        }
    }
    private int _workerAge;

    public List<CV> WorkerCVs { get; set; }

    public string WorkerUserName
    {
        get => _workerUserName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(WorkerUserName), "WorkerUserName is required");
            _workerUserName = value;
        }
    }
    private string _workerUserName;

    public string WorkerEmail
    {
        get => _workerEmail;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(WorkerEmail), "WorkerEmail is required");
            if (!new EmailAddressAttribute().IsValid(value))
                throw new ArgumentException("Invalid email format", nameof(WorkerEmail));
            _workerEmail = value;
        }
    }
    private string _workerEmail;

    public string WorkerPassword
    {
        get => _workerPassword;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(WorkerPassword), "WorkerPassword is required");
            _workerPassword = value;
        }
    }
    private string _workerPassword;

}

