using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace WPFUtils.UserControls
{
    public class DataGridIndexColumn : DataGridColumn
    {
        public DataGridIndexColumn()
        {
            IsReadOnly = true;
            Header = "序号";
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            return GenerateContent(cell, dataItem);
        }

        private FrameworkElement GenerateContent(DataGridCell cell, object dataItem)
        {
            var textBlock = new TextBlock();
            if (cell.DataContext is IList list)
            {
                textBlock.Text = $"{list.IndexOf(dataItem) + 1}";
            }

            return textBlock;
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return GenerateContent(cell, dataItem);
        }
    }
}
