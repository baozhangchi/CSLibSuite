using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFUtils.UserControls
{
    public class DataGridIndexColumn : DataGridColumn
    {
        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            var textBlock = new TextBlock();
            if (cell.DataContext is IList list)
            {
                textBlock.Text = $"{list.IndexOf(dataItem) + 1}";
            }
            throw new NotImplementedException();
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            throw new NotImplementedException();
        }
    }
}
