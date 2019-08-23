using AYam.Common.MVVM;
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
    public class Quotation : TabPrintViewModelBase, IDisposable
    {

        #region Model

        /// <summary>
        /// 見積書.Model
        /// </summary>
        private Model::Quotation _Model;

        #endregion

        #region ViewModel

        /// <summary>
        /// 印刷モード
        /// </summary>
        public bool IsPrintMode { get { return _Model?.IsPrintMode ?? false; } }

        /// <summary>
        /// 入力モード
        /// </summary>
        public bool IsInputMode { get { return !_Model?.IsPrintMode ?? true; } }

        /// <summary>
        /// 現在ページ
        /// </summary>
        public int NowPage { get { return _Model?.NowPage ?? 0; } }

        /// <summary>
        /// 最大ページ数
        /// </summary>
        public int MaxPage { get { return _Model?.MaxPage ?? 0; } }

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
            get { return _Model?.File.GetStaffNames() ?? ""; }
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
        public string MasterPostCode { get { return Properties.Resources.PostCodeMark + _Model?.File.Master.PostalCode ?? ""; } }

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
        public string DeliveryDate { get { return _Model?.File.DeliveryDate.Word ?? ""; } }

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
        /// 印刷用摘要一覧
        /// </summary>
        public ObservableCollection<QuoteSummary> PrintSummaries { get { return _Model?.PrintSummaries; } }

        /// <summary>
        /// 選択している見積内容
        /// </summary>
        public QuoteSummary SelectedSummary { get; set; } = null;

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
        /// 小計金額
        /// 税抜
        /// </summary>
        private decimal _SubTotalPrice;

        /// <summary>
        /// 小計金額
        /// 税抜
        /// </summary>
        public decimal SubTotalPrice
        {
            get { return _SubTotalPrice; }
            set
            {
                _SubTotalPrice = value;
                CallPropertyChanged();
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

        /// <summary>
        /// 客先担当選択コマンド
        /// </summary>
        public DelegateCommand SelectedStaffCommand
        {
            get
            {
                return new DelegateCommand(() => { CallPropertyChanged("CallSelectedStaff"); });
            }
        }

        /// <summary>
        /// 納期入力コマンド
        /// </summary>
        public DelegateCommand InputDeliveryDateCommand
        {
            get
            {
                return new DelegateCommand(() => { CallPropertyChanged("CallInputDeliveryDate"); });
            }
        }

        /// <summary>
        /// 摘要コマンド
        /// </summary>
        public DelegateCommand<string> SummaryCommand
        {
            get
            {

                return new DelegateCommand<string>(
                    (parameter) => 
                    {

                        switch (parameter)
                        {

                            case "add":     // 行追加

                                Summaries?.Add(new QuoteSummary()
                                {
                                    No = Summaries?.Count + 1 ?? 1,
                                    Summary = "",
                                    Volume = 0,
                                    Unit = VolumeUnit.Sets,
                                    UnitPrice = 0
                                });

                                break;

                            case "insert":  // 行挿入

                                if (SelectedSummary != null)
                                {

                                    var index = Summaries?.IndexOf(SelectedSummary) ?? -1;

                                    if (!index.Equals(-1))
                                    {

                                        for (int iLoop = index; iLoop < Summaries.Count; iLoop++)
                                        {
                                            Summaries[iLoop].No += 1;
                                        }

                                        var quote = new QuoteSummary()
                                        {
                                            No = index + 1,
                                            Summary = "",
                                            Volume = 0,
                                            Unit = VolumeUnit.Sets,
                                            UnitPrice = 0
                                        };

                                        Summaries?.Insert(index, quote);

                                    }

                                }

                                break;

                            case "remove":  // 行削除

                                if (SelectedSummary != null)
                                {

                                    var index = Summaries?.IndexOf(SelectedSummary) ?? -1;

                                    if (!index.Equals(-1))
                                    {

                                        for (int iLoop = Summaries.Count - 1; iLoop > index; iLoop--)
                                        {
                                            Summaries[iLoop].No -= 1;
                                        }

                                        Summaries.RemoveAt(index);

                                    }

                                }

                                break;

                        }

                        Save();
                        CalcSubTotalPrice();
                        CalcTotalPrice();

                    });

            }
        }

        /// <summary>
        /// 印刷コマンド
        /// </summary>
        public DelegateCommand PrintCommand
        {
            get
            {
                return new DelegateCommand(() => { CallPropertyChanged("CallExecutePrint"); });
            }
        }

        public DelegateCommand SaveCommand
        {
            get
            {
                return new DelegateCommand(() => { CallPropertyChanged("CallSave"); });
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
                summary.SetCalcTotalPrice(new Action(() => 
                {
                    CalcSubTotalPrice();
                    CalcTotalPrice();
                }));
            }

            CalcSubTotalPrice();
            CalcTotalPrice();

        }

        /// <summary>
        /// 見積書.ViewModel
        /// </summary>
        /// <param name="dataFile">見積情報</param>
        /// <param name="nowPage">現在ページ数</param>
        /// <param name="maxPage">最大ページ数</param>
        public Quotation(DataFileInfo dataFile, int nowPage, int maxPage)
        {

            _Model = new Model::Quotation(dataFile)
            {
                IsPrintMode = true,
                NowPage = nowPage,
                MaxPage = maxPage
            };

            foreach (var summary in _Model.File.Summaries)
            {
                summary.SetCalcTotalPrice(new Action(() =>
                {
                    CalcSubTotalPrice();
                    CalcTotalPrice();
                }));
            }

            _Model.SetPrintSummaries();

            CalcSubTotalPrice();
            CalcTotalPrice();

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
        /// 小計金額を計算
        /// </summary>
        private void CalcSubTotalPrice()
        {

            decimal subTotalPrice = 0;

            if (_Model != null)
            {

                if (IsPrintMode)
                {

                    foreach (var summary in PrintSummaries)
                    {

                        subTotalPrice += summary.Price;

                    }

                }
                else
                {

                    foreach (var summary in _Model.File.Summaries)
                    {

                        subTotalPrice += summary.Price;

                    }

                }

            }

            SubTotalPrice = subTotalPrice;

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

        /// <summary>
        /// 取引先担当者の登録
        /// </summary>
        /// <param name="staffs">取引先担当者一覧</param>
        public void SetClientStaffs(ObservableCollection<Staff> staffs)
        {

            _Model?.SetClientStaffs(staffs);
            CallPropertyChanged(nameof(ClientStaff));

            Save();

        }

        /// <summary>
        /// 納期情報の取得
        /// </summary>
        public DeliveryDate GetDeliveryDate()
        {
            return _Model?.File.DeliveryDate;
        }

        /// <summary>
        /// 納期情報を設定
        /// </summary>
        /// <param name="no">数日、週数、月数</param>
        /// <param name="unit">
        /// 単位指定
        /// 日、週、月
        /// </param>
        public void SetDeliveryDate(double no, DeliveryUnit unit)
        {
            _Model?.SetDeliveryDate(no, unit);
            CallPropertyChanged(nameof(DeliveryDate));
        }

        /// <summary>
        /// 見積情報の取得
        /// </summary>
        /// <returns>見積情報</returns>
        public override DataFileInfo GetDataFileInfo()
        {
            return _Model?.DataFile;
        }

        /// <summary>
        /// 印刷実行
        /// </summary>
        public override string ExecutePrint()
        {
            
            if (_Model != null)
            {

                _Model.Save();
                return _Model.ExecutePrint();

            }
            else
            {
                return string.Empty;
            }

        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            _Model?.Save();
        }

    }

}
