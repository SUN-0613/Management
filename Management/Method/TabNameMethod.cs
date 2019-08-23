namespace Management.Method
{

    /// <summary>
    /// タブ名
    /// </summary>
    public　static class TabNameMethod
    {

        /// <summary>
        /// タブ名取得
        /// </summary>
        /// <param name="title">主題</param>
        /// <param name="subTitle">副題</param>
        /// <returns>主題 + ":" + 副題の先頭5文字</returns>
        public static string GetTabName(string title, string subTitle)
        {

            if (subTitle.Length > 5)
            {
                subTitle = subTitle.Substring(0, 4) + "...";
            }

            return title + ":" + subTitle;

        }

    }
}
