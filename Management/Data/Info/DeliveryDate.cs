using Property = Management.Properties;

namespace Management.Data.Info
{

    /// <summary>
    /// 納期単位
    /// </summary>
    public enum DeliveryUnit
    {
        /// <summary>
        /// 日間
        /// </summary>
        Days = 0,
        /// <summary>
        /// 週間
        /// </summary>
        Weeks,
        /// <summary>
        /// ヶ月間
        /// </summary>
        Months,
    }

    /// <summary>
    /// 見積納期
    /// </summary>
    public class DeliveryDate
    {

        #region Property

        /// <summary>
        /// 日数、週数、月数
        /// </summary>
        public double No = 1d;

        /// <summary>
        /// 納期単位
        /// </summary>
        public DeliveryUnit Unit = DeliveryUnit.Months;

        /// <summary>
        /// 納期
        /// </summary>
        public string Word
        {
            get
            {

                const string wild = "*";

                switch (Unit)
                {

                    case DeliveryUnit.Months:
                        return Property::Quotation.AfterReceiptOfOrder + Property::Quotation.Months.Replace(wild, No.ToString());

                    case DeliveryUnit.Weeks:
                        return Property::Quotation.AfterReceiptOfOrder + Property::Quotation.Weeks.Replace(wild, No.ToString());

                    case DeliveryUnit.Days:
                    default:
                        return Property::Quotation.AfterReceiptOfOrder + Property::Quotation.Days.Replace(wild, No.ToString());


                }

            }
        }

        #endregion

    }

}
