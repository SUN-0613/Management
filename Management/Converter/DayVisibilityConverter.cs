using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Management.Converter
{

    /// <summary>
    /// 日付未割り当て(value = 0)の場合非表示にする
    /// </summary>
    public class DayVisibilityConverter : IValueConverter
    {

        /// <summary>
        /// 日付未割り当て(value = 0)の場合非表示にする
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is int numValue)
            {

                return numValue.Equals(0) ? Visibility.Collapsed : Visibility.Visible;

            }
            else
            {
                throw new NotImplementedException();
            }

        }

        /// <summary>
        /// 未使用
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}
