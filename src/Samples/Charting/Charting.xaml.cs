using System.Linq;
using System.Reflection;

namespace System.Windows.Controls.Samples
{
    /// <summary>
    /// Interaction logic for Charting.xaml
    /// </summary>
    public partial class Charting
    {
        public Charting()
        {
            InitializeComponent();

            SampleList.ItemsSource =
                typeof (Charting).Assembly.GetTypes()
                    .Select(t => new Item(t))
                    .Where(t => t.Attribute != null)
                    .ToArray();
        }
    }

    class Item
    {
        public Type Type { get; private set; }

        public SampleAttribute Attribute { get; private set; }

        public string Name { get { return Attribute.Name; } }

        public object Object { get { return Activator.CreateInstance(Type); } }

        public Item(Type type)
        {
            Type = type;
            Attribute = type.GetCustomAttribute<SampleAttribute>();
        }
    }
}
