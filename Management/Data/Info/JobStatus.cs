using System;

namespace Management.Data.Info
{

    /// <summary>
    /// 状態
    /// </summary>
    public enum StatusEnum
    {
        /// <summary>
        /// 無
        /// </summary>
        None,
        /// <summary>
        /// 未注
        /// </summary>
        NotOrdered,
        /// <summary>
        /// 受注
        /// </summary>
        Ordered,
        /// <summary>
        /// 納品
        /// </summary>
        Delivery,
        /// <summary>
        /// 完了
        /// </summary>
        Finished
    }

    /// <summary>
    /// ジョブの状態
    /// </summary>
    public class JobStatus
    {

        /// <summary>
        /// 状態
        /// </summary>
        public StatusEnum Status { get; set; } = StatusEnum.None;

        /// <summary>
        /// 納期
        /// </summary>
        public string Deadline { get; set; } = "";

        /// <summary>
        /// 受注日
        /// </summary>
        public DateTime OrderedDate { get; set; } = DateTime.MinValue;

        /// <summary>
        /// 納品日
        /// </summary>
        public DateTime DeliveryDate { get; set; } = DateTime.MinValue;

        /// <summary>
        /// 入金予定日
        /// </summary>
        public DateTime DepositDate { get; set; } = DateTime.MinValue;

        /// <summary>
        /// ジョブの状態
        /// </summary>
        public JobStatus()
        { }

    }

}
