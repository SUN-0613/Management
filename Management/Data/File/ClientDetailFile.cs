using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Management.Data.File
{

    /// <summary>
    /// 取引先情報ファイル
    /// </summary>
    public class ClientDetailFile : Base.DataFile
    {

        /// <summary>
        /// 登録日のテキスト保存用フォーマット
        /// </summary>
        private readonly string _DateTimeFormat = "yyyyMMddHHmmssfff";

        /// <summary>
        /// 会社名
        /// </summary>
        public string CompanyName;

        /// <summary>
        /// 郵便番号
        /// </summary>
        public string PostalCode;

        /// <summary>
        /// 住所
        /// </summary>
        public string Address;

        /// <summary>
        /// 電話番号
        /// </summary>
        public string PhoneNo;

        /// <summary>
        /// 銀行口座
        /// </summary>
        public string BankAccount;

        /// <summary>
        /// 担当差一覧
        /// </summary>
        public ObservableCollection<Staff> Staffs { get; set; }

        /// <summary>
        /// 取引先情報ファイル
        /// </summary>
        /// <param name="wildName">ファイル名のワイルド部分</param>
        public ClientDetailFile(string wildName) : base(new PathInfo().Files.ClientDetail.Replace(PathInfo.Wild, wildName))
        {
            Staffs = new ObservableCollection<Staff>();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public override void Dispose()
        {

            if (Staffs != null)
            {
                Staffs.Clear();
                Staffs = null;
            }

            base.Dispose();

        }

        /// <summary>
        /// ファイル読込
        /// </summary>
        public override void Read()
        {

            CompanyName = GetValue(nameof(CompanyName));
            PostalCode = GetValue(nameof(PostalCode), "-");
            Address = GetValue(nameof(Address));
            PhoneNo = GetValue(nameof(PhoneNo), "--");
            BankAccount = GetValue(nameof(BankAccount));

            var names = GetValues(nameof(Staff) + nameof(Staff.Name));
            var phonetics = GetValues(nameof(Staff) + nameof(Staff.Phonetic));
            var mails = GetValues(nameof(Staff) + nameof(Staff.EMailAddress));
            var mobiles = GetValues(nameof(Staff) + nameof(Staff.MobilePhone));
            var creates = GetValues(nameof(Staff) + nameof(Staff.CreateDate));

            for (int iLoop = 0; iLoop < names.Count; iLoop++)
            {
                Staffs.Add(new Staff(names[iLoop], phonetics[iLoop]
                                    , mails[iLoop], mobiles[iLoop]
                                    , DateTime.ParseExact(creates[iLoop], _DateTimeFormat, null)));
            }

        }

        /// <summary>
        /// ファイル保存
        /// </summary>
        public override void Save()
        {

            Update(nameof(CompanyName), CompanyName);
            Update(nameof(PostalCode), PostalCode);
            Update(nameof(Address), Address);
            Update(nameof(PhoneNo), PhoneNo);
            Update(nameof(BankAccount), BankAccount);

            var names = new List<string>();
            var phonetics = new List<string>();
            var mails = new List<string>();
            var mobiles = new List<string>();
            var creates = new List<string>();

            for (int iLoop = 0; iLoop < Staffs.Count; iLoop++)
            {

                names.Add(Staffs[iLoop].Name);
                phonetics.Add(Staffs[iLoop].Phonetic);
                mails.Add(Staffs[iLoop].EMailAddress);
                mobiles.Add(Staffs[iLoop].MobilePhone);
                creates.Add(Staffs[iLoop].CreateDate.ToString(_DateTimeFormat));

            }

            Update(nameof(Staff) + nameof(Staff.Name), names);
            Update(nameof(Staff) + nameof(Staff.Phonetic), phonetics);
            Update(nameof(Staff) + nameof(Staff.EMailAddress), mails);
            Update(nameof(Staff) + nameof(Staff.MobilePhone), mobiles);
            Update(nameof(Staff) + nameof(Staff.CreateDate), creates);

            WriteFile();

        }

    }

    /// <summary>
    /// 担当者情報
    /// </summary>
    public class Staff
    {

        /// <summary>
        /// 氏名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 振り仮名
        /// </summary>
        public string Phonetic { get; set; }

        /// <summary>
        /// メールアドレス
        /// </summary>
        public string EMailAddress { get; set; }

        /// <summary>
        /// 携帯電話番号
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 登録日
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 担当者情報
        /// </summary>
        /// <param name="name">氏名</param>
        /// <param name="phonetic">振り仮名</param>
        /// <param name="eMailAddress">メールアドレス</param>
        /// <param name="mobilePhone">携帯電話番号</param>
        /// <param name="createDate">登録日</param>
        public Staff(string name, string phonetic, string eMailAddress, string mobilePhone, DateTime createDate)
        {

            Name = name;
            Phonetic = phonetic;
            EMailAddress = eMailAddress;
            MobilePhone = mobilePhone;
            CreateDate = createDate;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {

            if (obj is Staff staff)
            {

                return staff.Name.Equals(Name)
                        && staff.Phonetic.Equals(Phonetic)
                        && staff.EMailAddress.Equals(EMailAddress)
                        && staff.MobilePhone.Equals(MobilePhone)
                        && staff.CreateDate.Equals(CreateDate);
                
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

    }

}
