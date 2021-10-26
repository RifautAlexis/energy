namespace Energy.Contracts.DTOs
{
    public class PowerPlantDTO
    {
        public string Name { get; set; }
        public PowerPlantType Type { get; set; }
        public double Efficiency { get; set; }
        public int Pmin { get; set; }
        public int Pmax { get; set; }
    }

    public enum PowerPlantType
    {
        GasFired,
        TurboJet,
        WindTurbine
    }
}
