namespace Services
{
    public class UnitConfigurationService
    {
        public float InitialHealth { get; private set; }
        public float AttackRange { get; private set; }
        public float AttackDamage { get; private set; }

        public void Initialize()
        {
            // TODO: add receiving this data from json config file
            InitialHealth = 100f;
            AttackRange = 0f;
            AttackDamage = 10f;
        }
    }
}
