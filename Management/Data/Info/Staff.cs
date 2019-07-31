using System;

namespace Management.Data.Info
{

    /// <summary>
    /// 担当者情報
    /// </summary>
    public class Staff : ICloneable
    {

        /// <summary>
        /// 該当担当者を書類表記するか
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// 名前
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 名前：よみがな
        /// </summary>
        public string FirstKana { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 名字：よみがな
        /// </summary>
        public string LastKana { get; set; }

        /// <summary>
        /// フルネーム
        /// </summary>
        public string FullName
        {
            get
            {
                if (IsFullNameJapaneseStyle)
                {
                    return LastName + " " + FirstName;
                }
                else
                {
                    return FirstName + " " + LastName;
                }
            }
        }

        /// <summary>
        /// フルネームを姓名表記とするか
        /// </summary>
        public bool IsFullNameJapaneseStyle { private get; set; } = true;

        /// <summary>
        /// 書類表記時、フルネーム表示するか
        /// </summary>
        public bool IsNotationFullName { get; set; } = false;

        /// <summary>
        /// 部署
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 役職
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// メールアドレス
        /// </summary>
        public string EMailAddress { get; set; }

        /// <summary>
        /// 携帯電話番号
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// メモ
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 登録日
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 担当者情報
        /// </summary>
        /// <param name="firstName">名前</param>
        /// <param name="firstKana">名前：よみがな</param>
        /// <param name="lastName">名字</param>
        /// <param name="lastKana">名字：よみがな</param>
        /// <param name="department">部署</param>
        /// <param name="position">役職</param>
        /// <param name="eMailAddress">メールアドレス</param>
        /// <param name="mobilePhone">携帯電話番号</param>
        /// <param name="remarks">メモ</param>
        /// <param name="createDate">登録日</param>
        public Staff(string firstName, string firstKana, string lastName, string lastKana, string department, string position, string eMailAddress, string mobilePhone, string remarks, DateTime createDate, bool IsNotationFullName)
        {

            FirstName = firstName;
            FirstKana = firstKana;
            LastName = lastName;
            LastKana = lastKana;
            Department = department;
            Position = position;
            EMailAddress = eMailAddress;
            MobilePhone = mobilePhone;
            Remarks = remarks;
            CreateDate = createDate;

        }

        /// <summary>
        /// 値が一致するかチェック
        /// </summary>
        /// <param name="obj">チェックしたい担当者情報</param>
        /// <returns>
        /// True:一致
        /// False:不一致
        /// </returns>
        public override bool Equals(object obj)
        {

            if (obj is Staff staff)
            {

                return staff.FirstName.Equals(FirstName)
                        && staff.FirstKana.Equals(FirstKana)
                        && staff.LastName.Equals(LastName)
                        && staff.LastKana.Equals(LastKana)
                        && staff.Department.Equals(Department)
                        && staff.Position.Equals(Position)
                        && staff.EMailAddress.Equals(EMailAddress)
                        && staff.MobilePhone.Equals(MobilePhone)
                        && staff.Remarks.Equals(Remarks)
                        && staff.CreateDate.Equals(CreateDate)
                        && staff.IsNotationFullName.Equals(IsNotationFullName);

            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// ハッシュコードの取得
        /// </summary>
        /// <returns>ハッシュコード</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// クローン作成
        /// </summary>
        /// <returns>クローン</returns>
        public object Clone()
        {
            return new Staff(FirstName, FirstKana, LastName, LastKana, Department, Position, EMailAddress, MobilePhone, Remarks, CreateDate, IsNotationFullName);
        }

    }

}
