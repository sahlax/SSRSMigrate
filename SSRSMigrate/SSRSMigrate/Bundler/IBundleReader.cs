﻿using System.Collections.Generic;
using SSRSMigrate.Bundler.Events;

namespace SSRSMigrate.Bundler
{
    // Event handlers
    public delegate void FolderReadEventHandler(IBundleReader sender, ItemReadEvent e);
    public delegate void DataSourceReadEventHandler(IBundleReader sender, ItemReadEvent e);
    public delegate void ReportReadEventHandler(IBundleReader sender, ItemReadEvent e);

    public interface IBundleReader 
    {
        // Properties
        string ExportSummaryFilename { get; }
        //Dictionary<string, List<BundleSummaryEntry>> Entries { get; }
        BundleSummary Summary { get;  }
        string ArchiveFileName { get; set; }
        string UnPackDirectory { get; set; }

        // Events
        event FolderReadEventHandler OnFolderRead;
        event DataSourceReadEventHandler OnDataSourceRead;
        event ReportReadEventHandler OnReportRead;

        // Methods
        string Extract();
        void ReadExportSummary();
        void Read();
    }
}
