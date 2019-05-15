﻿using AYam.Common.Method;
using System;

namespace Management.Class
{

    /// <summary>
    /// パス情報
    /// </summary>
    public class PathInfo
    {

        #region Class

        /// <summary>
        /// フォルダ情報
        /// </summary>
        public class DirectoryInfo
        {

            /// <summary>
            /// ルートフォルダ
            /// </summary>
            public readonly string RootPath;

            /// <summary>
            /// マスタ情報フォルダ
            /// </summary>
            public readonly string MasterPath;

            /// <summary>
            /// フォルダ情報
            /// </summary>
            public DirectoryInfo()
            {

                RootPath = Path.MakeDirectories(Environment.CurrentDirectory);
                MasterPath = Path.MakeDirectories(RootPath + @"\" + "MasterData");

            }

        }

        /// <summary>
        /// ファイル情報
        /// </summary>
        public class FileInfo
        {

            /// <summary>
            /// マスタ情報
            /// </summary>
            public readonly string Master;

            /// <summary>
            /// 取引先情報
            /// </summary>
            public readonly string Clients;

            /// <summary>
            /// フォルダ一覧
            /// </summary>
            private readonly DirectoryInfo Directories;

            /// <summary>
            /// ファイル情報
            /// </summary>
            public FileInfo()
            {

                Directories = new DirectoryInfo();

                Master = Path.GetFullPath(Directories.MasterPath, "Master.Dat");

            }

        }

        #endregion

        /// <summary>
        /// フォルダ一覧
        /// </summary>
        public readonly DirectoryInfo Directories;

        /// <summary>
        /// ファイル一覧
        /// </summary>
        public readonly FileInfo Files;

        /// <summary>
        /// パス情報
        /// </summary>
        public PathInfo()
        {

            Directories = new DirectoryInfo();
            Files = new FileInfo();

        }

    }

}