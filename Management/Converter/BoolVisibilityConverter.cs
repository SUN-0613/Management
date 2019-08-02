using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Management.Converter
{

    /// <summary>
    /// bool値からVisibility値へ変換
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolVisibilityConverter : IValueConverter
    {

        /// <summary>
        /// bool -> Visibility
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value is bool boolValue)
            {

                return boolValue ? Visibility.Visible : Visibility.Hidden;

            }

            throw new NotImplementedException();

        }

        /// <summary>
        /// Visibility -> bool
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value is Visibility visible)
            {

                switch (visible)
                {

                    case Visibility.Visible:
                        return true;

                    default:
                        return false;

                }

            }

            throw new NotImplementedException();

        }

    }

}
