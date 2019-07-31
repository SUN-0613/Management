using Management.Data.File;
using Management.Data.Info;
using System;

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
        private DataFileInfo _DataFile;

        #endregion

        /// <summary>
        /// 見積書.Model
        /// </summary>
        /// <param name="dataFile">見積情報</param>
        public Quotation(DataFileInfo dataFile)
        {

            File = new QuotationFile(dataFile);

        }

        /// <summary>
        /// クローン作成
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new Quotation(_DataFile);
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

    }
}
