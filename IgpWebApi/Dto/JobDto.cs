using NetTopologySuite.Geometries;
public class JobDto
{
    public string JobDescription { get; set; } = string.Empty;

    public int SkillTypeId { get; set; }
    public int JobinitialBudget { get; set; } 
    public string Image { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public GeolocationDto Location { get; set; }

}




