using System;

namespace Management.Data.Info
{

    /// <summary>
    /// 作成書類情報
    /// </summary>
    public class DataFileInfo
    {

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
        /// <param name="Revision">改訂</param>
        /// <param name="createDate">作成日時</param>
        /// <param name="seq">シーケンス</param>
        /// <param name="createdFlg">作成済FLG</param>
        public DataFileInfo(int revision, DateTime createDate, int seq, bool createdFlg)
        {

            Revision = revision;
            CreateDate = createDate;
            Seaquence = seq;
            CreatedFlg = createdFlg;

        }

    }

}
