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

        public static T ChildOfType<T>(this DependencyObject dependencyObject)
            where T : DependencyObject
        {
            if (VisualTreeHelper.GetChildrenCount(dependencyObject) > 0)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
                {
                    var child = VisualTreeHelper.GetChild(dependencyObject, i);
                    if (child is T target)
                    {
                        return target;
                    }

                    target = child.ChildOfType<T>();
                    if (target != null)
                    {
                        return target;
                    }
                }
            }

            return default;
        }
    }
}
