using Management.Data.File;
using Management.Data.Info;
using System;
using System.Collections.ObjectModel;

namespace Management.Pages.Model.Quotation
{

    /// <summary>
    /// 見積書.Model
    /// </summary>
    public class Quotation : IDisposable, ICloneable
    {

        #region File

        /// <summary>
        /// 見積書ファイル
        /// </summary>
        public QuotationFile File;

        /// <summary>
        /// 見積情報
        /// </summary>
        public DataFileInfo DataFile { get; private set; }

        #endregion

        /// <summary>
        /// 見積書.Model
        /// </summary>
        /// <param name="dataFile">見積情報</param>
        public Quotation(DataFileInfo dataFile)
        {

            DataFile = dataFile;
            File = new QuotationFile(DataFile);

        }

        /// <summary>
        /// クローン作成
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new Quotation(DataFile);
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            File.Dispose();
            File = null;

        }

        /// <summary>
        /// 見積書ファイルを保存
        /// </summary>
        public void Save()
        {
            File.Save();
        }

        /// <summary>
        /// 見積書ファイルを削除
        /// </summary>
        public void Delete()
        {
            File.Delete();
        }

        /// <summary>
        /// 取引先担当者の登録
        /// </summary>
        /// <param name="staffs">取引先担当者一覧</param>
        public void SetClientStaffs(ObservableCollection<Staff> staffs)
        {

            File.ClientStaffs.Clear();

            foreach (var staff in staffs)
            {
                File.ClientStaffs.Add((Staff)staff.Clone());
            }

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

            File.DeliveryDate.No = no;
            File.DeliveryDate.Unit = unit;

        }

    }
}
