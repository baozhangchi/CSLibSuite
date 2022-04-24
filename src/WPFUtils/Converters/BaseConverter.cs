using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFUtils.Converters
{
    public abstract class BaseConverter<T> : IValueConverter
        where T : BaseConverter<T>, new()
    {
        // ReSharper disable once InconsistentNaming
        protected static T _instance;
        // ReSharper disable once StaticMemberInGenericType
        // ReSharper disable once InconsistentNaming
        protected static readonly object _lock = new object();

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new T();
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
