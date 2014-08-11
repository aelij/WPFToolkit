using System.Collections;
using System.Collections.ObjectModel;

namespace System.Windows.Controls.Samples
{
    public class ObjectCollection : Collection<Object>
    {
        public ObjectCollection()
        {
        }

        public ObjectCollection(IEnumerable enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }

            foreach (Object item in enumerable)
            {
                Add(item);
            }
        }
    }
}