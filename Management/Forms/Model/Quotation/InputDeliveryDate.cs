using Management.Data.Info;
using System;

namespace Management.Forms.Model.Quotation
{

    /// <summary>
    /// 納期入力.Model
    /// </summary>
    public class InputDeliveryDate : IDisposable
    {

        #region File

        /// <summary>
        /// 見積納期
        /// </summary>
        public DeliveryDate DeliveryDate;

        #endregion

        /// <summary>
        /// 納期入力.Model
        /// </summary>
        /// <param name="deliveryDate">見積納期</param>
        public InputDeliveryDate(DeliveryDate deliveryDate)
        {

            DeliveryDate = deliveryDate;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        { }

    }

}
