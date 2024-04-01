namespace ConsoleApp2.Models;

public class Vacancy
{
    public int VacancyId { get; set; }
    public string? VacancyName { get; set; }
    public string? VacancyInfo { get; set; }
    public double VacancySalary { get; set; }

    // Constructor
    public Vacancy(int vacancyId, string vacancyName, string vacancyInfo, double vacancySalary)
    {
        VacancyId = vacancyId;
        VacancyName = vacancyName;
        VacancyInfo = vacancyInfo;
        VacancySalary = vacancySalary;
    }

    // Override ToString() method to provide custom string representation
    public override string ToString()
    {
        return $"VacancyId: {VacancyId}, VacancyName: {VacancyName}, VacancyInfo: {VacancyInfo}, VacancySalary: {VacancySalary}";
    }
}

