namespace Server.Entities.Model;

public class ErrorModel 
{

    public ErrorModel() { }


    public ErrorModel(string? errors)
    {
        if (errors is not null || !String.IsNullOrEmpty(errors))
            this.Errors = errors.Split(";").Where(x => x != string.Empty).ToList();
  
    }

    public List<string> Errors { get; set; }

    public void OnGet(List<string> errors)
    {
        Errors = errors ?? new List<string>();
    }
}