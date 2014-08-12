using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Windows.Controls.Samples
{
    internal class RandomPairs : INotifyPropertyChanged
    {
        private readonly Random _rng = new Random();

        private Tuple<object, object>[] _pairs;

        public Tuple<object, object>[] Pairs
        {
            get { return _pairs; }
            set
            {
                _pairs = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Start()
        {
            Pairs = Enumerable.Range(0, _rng.Next(2, 10))
                .Select(
                    x => new Tuple<object, object>(x.ToString(CultureInfo.InvariantCulture), _rng.NextDouble()))
                .ToArray();
        }

        public override string ToString()
        {
            return "Item";
        }
    }
}