﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSRSMigrate.SSRS
{
    public interface IReportServerRepository
    {
        // Folders
        List<FolderItem> GetFolders(string path);
        void CreateFolder(string folderPath);

        // Reports
        byte[] GetReportDefinition(string reportPath);
        ReportItem GetReport(string reportPath);
        List<ReportItem> GetReports(string path);
        string[] WriteReport(string reportPath, ReportItem reportItem);

        // Data Source
        DataSourceItem GetDataSource(string dataSourcePath);
        List<DataSourceItem> GetDataSources(string path);
        string[] WriteDataSource(string dataSourcePath, DataSourceItem dataSource);

        // Items
        bool ItemExists(string itemPath, string itemType);
    }
}
