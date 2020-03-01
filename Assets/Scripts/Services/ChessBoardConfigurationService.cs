namespace Services
{
    public class ChessBoardConfigurationService
    {
        public int Height { get; private set; }
        public int Width { get; private set; }

        public void Initialize()
        {
            // TODO: add receiving this data from json config file
            Height = 6;
            Width = 8;
        }
    }
}
