﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

// ReSharper disable once CheckNamespace
namespace System.Windows.Controls
{
    public class DataGridHelper
    {
        public static bool GetSupportDisplay(DependencyObject obj)
        {
            return (bool)obj.GetValue(SupportDisplayProperty);
        }

        public static void SetSupportDisplay(DependencyObject obj, bool value)
        {
            obj.SetValue(SupportDisplayProperty, value);
        }

        // Using a DependencyProperty as the backing store for ColumnDisplay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SupportDisplayProperty =
            DependencyProperty.RegisterAttached("SupportDisplay", typeof(bool), typeof(DataGridHelper), new PropertyMetadata(default(bool), OnColumnDisplayChanged));

        private static void OnColumnDisplayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGrid grid)
            {
                grid.AutoGeneratingColumn -= DataGridAutoGeneratingColumn;
                if (e.NewValue != null && string.IsNullOrWhiteSpace((string)e.NewValue))
                {
                    grid.AutoGeneratingColumn += DataGridAutoGeneratingColumn;
                }
            }
        }

        private static void DataGridAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var result = e.PropertyName;
            var p = (e.PropertyDescriptor as PropertyDescriptor).ComponentType.GetProperty(e.PropertyName);

            if (p != null)
            {
                var found = p.GetCustomAttribute<DisplayNameAttribute>();
                if (found != null)
                {
                    result = found.DisplayName;

                    //if (found.get() == false)
                    //{
                    //    e.Cancel = true;
                    //    return;
                    //}
                    if (e.Column.Header != e.PropertyName)
                    {
                        return;
                    }

                }


            }

            e.Column.Header = result;

        }
    }
}