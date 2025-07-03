namespace Grad.DTO
{
    public class EmbassiesWithCity
    {

        public int Id { get; set; }
        public string EmbassiesName { get; set; }
        public string EmbassiesCountryName { get; set; }
        public string CityName { get; set; }
        public EmbassiesWithCity() { }
        public EmbassiesWithCity(int id,string embassiesName,string cityName,string EmbassiesCountryName)
        {
            Id = id;
            EmbassiesName = embassiesName;
            this.EmbassiesCountryName = EmbassiesCountryName;
            CityName = cityName;
        }

    }
}
