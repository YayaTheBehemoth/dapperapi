using System.ComponentModel.DataAnnotations;

namespace festivalbooking.Models
{
    public class Character
    {
        [Required, Range(1, int.MaxValue, ErrorMessage = "Give a goddamn ID stupid")]
        public int Id {get; set;}
        [Required(ErrorMessage = "This aint no nothing, give fella a callsign, dumbass")]
        public string Name {get; set;} = string.Empty;
        public string Bio {get; set;} = string.Empty;
        public int TeamId {get; set;} = 1;
        public int DifficultyId {get; set;} = 1;
        public bool IsReadyToFight {get; set;} = true;
    }

}