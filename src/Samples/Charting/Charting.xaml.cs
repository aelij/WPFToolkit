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
                    .Select(t => new Item(t, t.GetCustomAttribute<SampleAttribute>()))
                    .Where(t => t.Attribute != null);
        }
    }

    class Item
    {
        public Type Type { get; private set; }

        public SampleAttribute Attribute { get; private set; }

        public string Name { get { return Attribute.Name; } }

        public object Object { get { return Activator.CreateInstance(Type); } }

        public Item(Type type, SampleAttribute attribute)
        {
            Type = type;
            Attribute = attribute;
        }
    }
}
