namespace Services
{
    public class ChessBoardConfigurationService
    {
        public int Height { get; private set; } = 6;
        public int Width { get; private set; } = 8;

        public void Initialize()
        {
            // TODO: add receiving this data from json config file
            Height = 6;
            Width = 8;
        }
    }
}
