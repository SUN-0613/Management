using Management.Data.Info;
using Property = Management.Properties;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Management.Converter
{

    /// <summary>
    /// 見積納期の単位をリソースファイル内容に変換
    /// </summary>
    [ValueConversion(typeof(DeliveryUnit), typeof(string))]
    public class DeliveryUnitConverter : IValueConverter
    {

        /// <summary>
        /// 数量単位 -> リソースファイル内容
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            const string wild = "*";

            if (value is DeliveryUnit unit)
            {

                switch (unit)
                {

                    case DeliveryUnit.Days:
                        return Property::Quotation.Days.Replace(wild, "");

                    case DeliveryUnit.Weeks:
                        return Property::Quotation.Weeks.Replace(wild, "");

                    case DeliveryUnit.Months:
                    default:
                        return Property::Quotation.Months.Replace(wild, "");

                }

            }

            return Property::Quotation.Months.Replace(wild, "");

        }

        /// <summary>
        /// 今回は使用しない
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
