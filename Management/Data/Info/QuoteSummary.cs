using AYam.Common.MVVM;
using System;

namespace Management.Data.Info
{

    /// <summary>
    /// 数量単位
    /// </summary>
    public enum VolumeUnit
    {
        /// <summary>
        /// 個
        /// </summary>
        Pieces = 0,
        /// <summary>
        /// 式
        /// </summary>
        Sets,
        /// <summary>
        /// 時間
        /// </summary>
        Hours,
        /// <summary>
        /// 台
        /// </summary>
        Units
    }

#pragma warning disable 0659

    /// <summary>
    /// 見積摘要
    /// </summary>
    public class QuoteSummary : ViewModelBase, IDisposable
    {

        #region Property

        /// <summary>
        /// 合計金額計算
        /// </summary>
        private Action _CalcTotalPrice = null;

        /// <summary>
        /// No.
        /// </summary>
        public int No
        {
            get { return _No; }
            set
            {
                _No = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Volume
        {
            get { return _Volume; }
            set
            {
                _Volume = value;
                CalcPrice();
            }
        }

        /// <summary>
        /// 数量単位
        /// </summary>
        public VolumeUnit Unit { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        public decimal UnitPrice
        {
            get { return _UnitPrice; }
            set
            {
                _UnitPrice = value;
                CalcPrice();
            }
        }

        /// <summary>
        /// 金額
        /// </summary>
        public decimal Price { get; set; }

        #endregion

        /// <summary>
        /// No.
        /// </summary>
        private int _No;

        /// <summary>
        /// 数量
        /// </summary>
        private int _Volume = 0;

        /// <summary>
        /// 単価
        /// </summary>
        private decimal _UnitPrice = 0;

        /// <summary>
        /// 見積摘要
        /// </summary>
        public QuoteSummary()
        { }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {
            _CalcTotalPrice = null;
        }

        /// <summary>
        /// 金額計算
        /// </summary>
        public void CalcPrice()
        {

            Price = _Volume * _UnitPrice;
            CallPropertyChanged(nameof(Price));

            _CalcTotalPrice?.Invoke();

        }

        /// <summary>
        /// 合計金額計算アクションをセット
        /// </summary>
        /// <param name="action">Management.Pages.ViewModel.Quotation.Quotation.CalcTotalPrice()</param>
        public void SetCalcTotalPrice(Action action)
        {
            _CalcTotalPrice = action;
        }

        /// <summary>
        /// 合計金額計算アクションをリセット
        /// </summary>
        public void InitializeTotalPrice()
        {
            _CalcTotalPrice = null;
        }

        /// <summary>
        /// 同データかチェック
        /// </summary>
        public override bool Equals(object obj)
        {

            if (obj is QuoteSummary quote)
            {
                return No.Equals(quote.No);
            }
            else
            {
                return false;
            }
            
        }

    }

}
