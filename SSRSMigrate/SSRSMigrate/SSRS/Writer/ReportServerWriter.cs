﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSRSMigrate.SSRS.Repository;
using SSRSMigrate.SSRS.Errors;
using SSRSMigrate.SSRS.Item;
using SSRSMigrate.Utility;

namespace SSRSMigrate.SSRS.Writer
{
    public class ReportServerWriter : IReportServerWriter
    {
        private IReportServerRepository mReportRepository;

        public ReportServerWriter(IReportServerRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            this.mReportRepository = repository;
        }

        #region Folder Methods
        public string WriteFolder(FolderItem folderItem)
        {
            if (folderItem == null)
                throw new ArgumentNullException("folderItem");

            if (string.IsNullOrEmpty(folderItem.Path))
                throw new ArgumentException("path");

            if (!this.mReportRepository.ValidatePath(folderItem.Path))
                throw new InvalidPathCharsException(folderItem.Path);

            string name = folderItem.Name;
            string parentPath = SSRSUtil.GetParentPath(folderItem);

            return this.mReportRepository.CreateFolder(name, parentPath);
        }

        public string[] WriteFolders(FolderItem[] folderItems)
        {
            if (folderItems == null)
                throw new ArgumentNullException("folderItems");

            throw new NotImplementedException();
        }
        #endregion

        #region Report Methods
        public string[] WriteReport(string reportPath, Item.ReportItem reportItem)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Data Source Methods
        public string[] WriteDataSource(string dataSourcePath, Item.DataSourceItem dataSourceItem)
        {
            throw new NotImplementedException();
        }

        public string[] DeleteItem(string itemPath)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
