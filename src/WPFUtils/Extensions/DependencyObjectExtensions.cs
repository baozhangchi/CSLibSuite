using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

// ReSharper disable once CheckNamespace
namespace System.Windows
{
    public static class DependencyObjectExtensions
    {
        public static T ParentOfType<T>(this DependencyObject dependencyObject)
        where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);
            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as T;
        }
    }
}
