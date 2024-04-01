namespace ConsoleApp2.Models;

public class CV
{



    public string Specialty
    {
        get => _specialty;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(Specialty), "Specialty is required");
            _specialty = value;
        }
    }
    private string _specialty;

    public string School
    {
        get => _school;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(School), "School is required");
            _school = value;
        }
    }
    private string _school;

    public string University
    {
        get => _university;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(University), "University is required");
            _university = value;
        }
    }
    private string _university;

    public List<string> Skills { get; set; }

    public List<string> Companies { get; set; }

    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            if (value > DateTime.Now)
                throw new ArgumentException("StartDate cannot be in the future", nameof(StartDate));
            _startDate = value;
        }
    }
    private DateTime _startDate;

    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            if (value > DateTime.Now)
                throw new ArgumentException("EndDate cannot be in the future", nameof(EndDate));
            _endDate = value;
        }
    }
    private DateTime _endDate;


	public Dictionary<string, string> Languages { get; set; }

    public bool HasDegree { get; set; }
	public CV(string specialty, string school, string university,DateTime startDate, DateTime endDate, bool hasDegree)
	{
		Specialty=specialty;
		School=school;
		University=university;
		Skills=new();
        Companies=new() ;
		StartDate=startDate;
		EndDate=endDate;
		Languages=new();
		HasDegree=hasDegree;
	}
}

