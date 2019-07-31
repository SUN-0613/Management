using System;

namespace Management.Data.Info
{

    /// <summary>
    /// 作成書類の種類
    /// </summary>
    public enum DataKind
    {
        /// <summary>
        /// 見積書
        /// </summary>
        Quotation = 0,
        /// <summary>
        /// 納品書
        /// </summary>
        Delivery,
        /// <summary>
        /// 請求書
        /// </summary>
        Invoice,
        /// <summary>
        /// 封筒・送付状
        /// </summary>
        CoverLetter
    }

    /// <summary>
    /// 作成書類情報
    /// </summary>
    public class DataFileInfo
    {

        /// <summary>
        /// 書類の種類
        /// </summary>
        public DataKind Kind { get; set; }

        /// <summary>
        /// 改訂
        /// </summary>
        public int Revision { get; set; }

        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// シーケンス
        /// </summary>
        public int Seaquence { get; set; }

        /// <summary>
        /// 作成済FLG
        /// </summary>
        public bool CreatedFlg { get; set; }

        /// <summary>
        /// 作成書類情報
        /// </summary>
        /// <param name="kind">書類の種類</param>
        /// <param name="revision">改訂</param>
        /// <param name="createDate">作成日時</param>
        /// <param name="seq">シーケンス</param>
        /// <param name="createdFlg">作成済FLG</param>
        public DataFileInfo(DataKind kind, int revision, DateTime createDate, int seq, bool createdFlg)
        {

            Kind = kind;
            Revision = revision;
            CreateDate = createDate;
            Seaquence = seq;
            CreatedFlg = createdFlg;

        }

        /// <summary>
        /// ファイル名のワイルド部分を取得
        /// </summary>
        /// <returns>作成日時_改訂No</returns>
        public string GetFileNameParts()
        {
            return CreateDate.ToString("yyyyMMdd") + "_" + Revision.ToString();
        }

    }

}
