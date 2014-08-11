// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System.Reflection;
using System.Security;
using System.Windows.Markup;

[assembly: AssemblyTitle("WPF Toolkit Data Visualization")]
[assembly: AssemblyDescription("WPF Toolkit Data Visualization")]

// WPF Toolkit settings

[assembly: XmlnsDefinition("http://schemas.microsoft.com/wpf/2008/toolkit", "System.Windows.Controls.DataVisualization")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/wpf/2008/toolkit", "System.Windows.Controls.DataVisualization.Charting")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/wpf/2008/toolkit", "System.Windows.Controls.DataVisualization.Charting.Primitives")]

[assembly: AllowPartiallyTrustedCallers]