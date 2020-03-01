namespace Services
{
    public class UnitConfigurationService
    {
        public int Health { get; private set; }

        public void Initialize()
        {
            // TODO: add receiving this data from json config file
            Health = 100;
        }
    }
}
