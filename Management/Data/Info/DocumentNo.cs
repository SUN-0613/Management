using System;

namespace Management.Data.Info
{

    /// <summary>
    /// 各書類No情報
    /// </summary>
    public class DocumentNo
    {

        /// <summary>
        /// 作成日
        /// </summary>
        public DateTime CreateDate { get; private set; } = DateTime.Today;

        /// <summary>
        /// No
        /// </summary>
        public int No { get; private set; } = 0;

        /// <summary>
        /// 各書類No情報
        /// </summary>
        /// <param name="createDate">作成日</param>
        /// <param name="no">No</param>
        public DocumentNo(DateTime createDate, int no)
        {
            CreateDate = createDate;
            No = no;
        }

        /// <summary>
        /// 書類Noの更新
        /// </summary>
        public void UpdateDocumentNo()
        {

            if (CreateDate.Equals(DateTime.Today))
            {
                No++;
            }
            else
            {
                CreateDate = DateTime.Today;
                No = 1;
            }

        }

        /// <summary>
        /// 書類Noの取得
        /// </summary>
        /// <returns>書類No</returns>
        public string GetDocumentNo()
        {
            return CreateDate.ToString("yyyyMMdd") + "-" + No.ToString("D2");
        }

    }

}
