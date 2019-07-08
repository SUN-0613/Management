using Management.Data.Info;
using Property = Management.Properties;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Management.Converter
{

    /// <summary>
    /// ジョブ状態をリソースファイル内容に変換
    /// </summary>
    [ValueConversion(typeof(JobStatus.StatusEnum), typeof(string))]
    public class JobStatusConverter : IValueConverter
    {

        /// <summary>
        /// ジョブ状態 -> リソースファイル内容
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value is JobStatus.StatusEnum status)
            {

                switch (status)
                {

                    case JobStatus.StatusEnum.None:
                        return Property::JobList.Status_None;

                    case JobStatus.StatusEnum.NotOrdered:
                        return Property::JobList.Status_NotOrdered;

                    case JobStatus.StatusEnum.Ordered:
                        return Property::JobList.Status_Ordered;

                    case JobStatus.StatusEnum.Delivery:
                        return Property::JobList.Status_Delivery;

                    case JobStatus.StatusEnum.Finished:
                        return Property::JobList.Status_Finished;

                    default:
                        break;

                }

            }

            return Property::JobList.Status_None;

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
