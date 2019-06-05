
namespace Management.Data.Info
{

    /// <summary>
    /// 取引先情報
    /// </summary>
    public class Client
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ファイル名の"*"部分の名称
        /// </summary>
        public string FileWildName { get; set; }

        /// <summary>
        /// 取引先情報
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="wild">ファイル名の"*"部分の名称</param>
        public Client(string name, string wild)
        {
            Name = name;
            FileWildName = wild;
        }

    }

}
