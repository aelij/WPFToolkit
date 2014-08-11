// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993] for details.
// All other rights reserved.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Markup;

namespace System.Windows.Controls.Samples
{
    /// <summary>
    ///     Represents a department in an organization.
    /// </summary>
    [ContentProperty("Divisions")]
    public class Department
    {
        private readonly Collection<Employee> _employees;
        private Collection<Department> _divisions;

        /// <summary>
        ///     Initializes a new instance of the Department class.
        /// </summary>
        public Department()
        {
            _divisions = new Collection<Department>();
            _employees = new Collection<Employee>();
        }

        /// <summary>
        ///     Gets or sets the title of the department.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Gets a collection of employees in the department.
        /// </summary>
        public Collection<Employee> Employees
        {
            get { return _employees; }
        }

        /// <summary>
        ///     Gets a collection of divisions inside the department.
        /// </summary>
        public Collection<Department> Divisions
        {
            get { return _divisions; }
        }

        /// <summary>
        ///     Gets a sample hierarchy of departments and employees.
        /// </summary>
        public static IEnumerable<Department> AllDepartments
        {
            get
            {
                var data = Application.Current.Resources["DepartmentOrganization"] as IEnumerable<object>;
                return (data != null)
                    ? data.Cast<Department>()
                    : Enumerable.Empty<Department>();
            }
        }
    }
}