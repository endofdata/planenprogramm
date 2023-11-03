namespace Tarps.Datalayer
{
    public class TarpsSelection
    {
        private readonly HashSet<int> _selectedNumbers = new();

        public string SelectedNumbers => string.Join(", ", _selectedNumbers.Select(n => n.ToString()));

        public IDictionary<string, string> Selectors
        {
            get;
        } = new Dictionary<string, string>();

        public IDictionary<string, bool> Sequence
        {
            get;
        } = new Dictionary<string, bool>();
    }
}
