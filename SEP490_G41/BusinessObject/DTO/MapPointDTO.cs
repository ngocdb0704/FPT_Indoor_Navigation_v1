using Microsoft.AspNetCore.Http;
using NetTopologySuite.Geometries;

public class MapPointDTO
{
    public int MapPointId { get; set; }
    public int MapId { get; set; }
    public string MappointName { get; set; } = null!;
    public string LocationWeb { get; set; } = null!;
    public string LocationApp { get; set; } = null!;
    public string LocationGps { get; set; } = null!;
    public int FloorId { get; set; }
    public int BuildingId { get; set; }
    public string Image { get; set; } = null!;
    public bool Destination { get; set; }
    public string BuildingName { get; set; }
    public string FloorName { get; set; }



}

public class MapPointAddDTO
{
    public int MapId { get; set; }
    public string MappointName { get; set; } = null!;
    public string LocationWeb { get; set; } = null!;
    public string LocationApp { get; set; } = null!;
    public string LocationGps { get; set; }
    public int FloorId { get; set; }
    public int BuildingId { get; set; }     

}

public class MapPointUpdateDTO
{
    public int MapPointId { get; set; }
    public int MapId { get; set; }
    public string MappointName { get; set; } = null!;
    public string LocationWeb { get; set; } = null!;
    public string LocationApp { get; set; } = null!;
    public string LocationGps { get; set; }
    public int FloorId { get; set; }
    public int BuildingId { get; set; }
    public IFormFile Image { get; set; } = null!;

}
