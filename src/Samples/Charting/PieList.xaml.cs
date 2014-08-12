using System.Linq;

namespace System.Windows.Controls.Samples
{
    /// <summary>
    /// Interaction logic for PieList.xaml
    /// </summary>
    [Sample("Pie List", DifficultyLevel.Advanced, "Pie List")]
    public partial class PieList
    {
        public PieList()
        {
            InitializeComponent();
            List.ItemsSource = Enumerable.Range(0, 10).Select(t => new RandomPairs()).ToArray();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (RandomPairs)List.SelectedItem;
            Chart.DataContext = selectedItem;
            selectedItem.Start();
        }
    }
}
