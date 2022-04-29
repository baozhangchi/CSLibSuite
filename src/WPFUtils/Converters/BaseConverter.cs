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

        protected bool Invert { get; private set; }
        private static T _invertInstance;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Invert ? InvertConvert(value, targetType, parameter, culture) : NormalConvert(value, targetType, parameter, culture);
        }

        protected virtual object NormalConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        protected virtual object InvertConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Invert ? InvertConvertBack(value, targetType, parameter, culture) : NormalConvertBack(value, targetType, parameter, culture);
        }

        protected virtual object NormalConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        protected virtual object InvertConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
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

        public static T InvertInstance
        {
            get
            {
                if (_invertInstance == null)
                {
                    lock (_lock)
                    {
                        if (_invertInstance == null)
                        {
                            _invertInstance = new T();
                            _invertInstance.Invert = true;
                        }
                    }
                }

                return _invertInstance;
            }
        }
    }
}
