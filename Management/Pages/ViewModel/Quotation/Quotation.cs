using Management.Data.Info;
using Model = Management.Pages.Model.Quotation;
using Management.Pages.ViewModel.Base;
using System;
using System.Collections.ObjectModel;

namespace Management.Pages.ViewModel.Quotation
{

    /// <summary>
    /// 見積書.ViewModel
    /// </summary>
    public class Quotation : TabPrintViewModelBase, IDisposable, ICloneable
    {

        #region Model

        /// <summary>
        /// 見積書.Model
        /// </summary>
        private Model::Quotation _Model;

        #endregion

        #region ViewModel

        /// <summary>
        /// 取引先社名
        /// </summary>
        public string ClientName
        {
            get { return _Model?.File.ClientName ?? ""; }
            set
            {
                if (_Model != null)
                {
                    _Model.File.ClientName = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 取引先担当者名
        /// </summary>
        public string ClientStaff
        {
            get { return _Model?.File.ClientStaff ?? ""; }
            set
            {
                if (_Model != null)
                {
                    _Model.File.ClientStaff = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 見積No
        /// </summary>
        public string QuoteNo
        {
            get { return _Model?.File.QuoteNo ?? ""; }
            set
            {
                if (_Model != null)
                {
                    _Model.File.QuoteNo = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 見積日
        /// </summary>
        public DateTime QuoteDate
        {
            get { return _Model?.File.QuoteDate ?? DateTime.Now; }
            set
            {
                if (_Model != null)
                {
                    _Model.File.QuoteDate = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 件名
        /// </summary>
        public string JobName
        {
            get { return _Model?.File.JobName ?? ""; }
            set
            {
                if (_Model != null)
                {
                    _Model.File.JobName = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// マスタ情報.郵便番号
        /// </summary>
        public string MasterPostCode { get { return _Model?.File.Master.PostalCode ?? ""; } }

        /// <summary>
        /// マスタ情報.住所
        /// </summary>
        public string MasterAddress { get { return _Model?.File.Master.Address ?? ""; } }

        /// <summary>
        /// マスタ情報.電話番号
        /// </summary>
        public string MasterTelephone { get { return _Model?.File.Master.PhoneNo ?? ""; } }

        /// <summary>
        /// マスタ情報.メールアドレス
        /// </summary>
        public string MasterEMail { get { return _Model?.File.Master.EMailAddress ?? ""; } }

        /// <summary>
        /// マスタ情報.氏名
        /// </summary>
        public string MasterName { get { return _Model?.File.Master.GetFullName() ?? ""; } }

        /// <summary>
        /// 納期
        /// </summary>
        public string DeliveryDate
        {
            get { return _Model?.File.DeliveryDate ?? ""; }
            set
            {
                if (_Model != null)
                {
                    _Model.File.DeliveryDate = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 見積内容一覧
        /// </summary>
        public ObservableCollection<QuoteSummary> Summaries
        {
            get { return _Model?.File.Summaries; }
            set
            {
                if (_Model != null)
                {
                    _Model.File.Summaries = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks
        {
            get { return _Model?.File.Remarks ?? ""; }
            set
            {
                if (_Model != null)
                {
                    _Model.File.Remarks = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 合計金額
        /// 税抜
        /// </summary>
        private decimal _TotalPrice;

        /// <summary>
        /// 合計金額
        /// 税抜
        /// </summary>
        public decimal TotalPrice
        {
            get { return _TotalPrice; }
            set
            {
                _TotalPrice = value;
                CallPropertyChanged();
            }
        }

        #endregion

        /// <summary>
        /// 見積書.ViewModel
        /// </summary>
        public Quotation()
        { }

        /// <summary>
        /// 見積書.ViewModel
        /// </summary>
        /// <param name="dataFile">見積情報</param>
        public Quotation(DataFileInfo dataFile)
        {

            _Model = new Model::Quotation(dataFile);

            foreach (var summary in _Model.File.Summaries)
            {
                summary.SetCalcTotalPrice(new Action(() => CalcTotalPrice()));
            }

        }

        /// <summary>
        /// 見積書.ViewModel
        /// Clone()実行用
        /// </summary>
        /// <param name="quote">コピー元ViewModel</param>
        private Quotation(Quotation quote)
        {

            _Model = quote._Model.Clone() as Model::Quotation;

            foreach (var summary in _Model.File.Summaries)
            {
                summary.SetCalcTotalPrice(new Action(() => CalcTotalPrice()));
            }

        }

        /// <summary>
        /// クローン作成
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new Quotation(this);
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (_Model != null)
            {
                _Model.Dispose();
                _Model = null;
            }

        }

        /// <summary>
        /// 合計金額を計算
        /// </summary>
        private void CalcTotalPrice()
        {

            decimal totalPrice = 0;

            if (_Model != null)
            {

                foreach (var summary in _Model.File.Summaries)
                {

                    totalPrice += summary.Price;

                }

            }

            TotalPrice = totalPrice;

        }

    }

}
