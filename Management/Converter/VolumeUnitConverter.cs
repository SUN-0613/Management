using Management.Data.Info;
using Property = Management.Properties;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Management.Converter
{

    /// <summary>
    /// 数量単位をリソースファイル内容に変換
    /// </summary>
    [ValueConversion(typeof(VolumeUnit), typeof(string))]
    public class VolumeUnitConverter : IValueConverter
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

            if (value is VolumeUnit unit)
            {

                switch (unit)
                {

                    case VolumeUnit.Pieces:
                        return Property::Quotation.Pieces;

                    case VolumeUnit.Sets:
                        return Property::Quotation.Sets;

                    case VolumeUnit.Hours:
                        return Property::Quotation.Hours;

                    case VolumeUnit.Units:
                        return Property::Quotation.Units;

                }

            }

            return Property::Quotation.Sets;

        }

        /// <summary>
        /// 今回は使用しない
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}
