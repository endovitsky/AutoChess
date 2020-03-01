namespace Services
{
    public class UnitsSpawningConfigurationService
    {
        public int StepFromLeft { get; private set; }
        public int StepFromRight { get; private set; }

        public void Initialize()
        {
            // TODO: add receiving this data from json config file
            StepFromLeft = 3;
            StepFromRight = 3;
        }
    }
}
