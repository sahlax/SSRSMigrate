﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ninject.Extensions.Logging;
using SSRSMigrate.Wrappers;

namespace SSRSMigrate.Bundler
{
    public class ZipBundleReader : IBundleReader
    {
        private readonly IZipFileReaderWrapper mZipFileReaderWrapper = null;
        private readonly ICheckSumGenerator mCheckSumGenerator = null;
        private Dictionary<string, List<BundleSummaryEntry>> mEntries = null;
        private readonly ILogger mLogger = null;
        private readonly string mFileName = null;
        private readonly string mUnpackDirectory = null;

        private string mExportSummaryFilename = "ExportSummary.json";

        #region Public Properties
        public string ExportSummaryFilename
        {
            get { return this.mExportSummaryFilename; }
        }
        #endregion

        #region Events
        public event FolderReadEventHandler OnFolderRead;
        public event DataSourceReadEventHandler OnDataSourceRead;
        public event ReportReadEventHandler OnReportRead;
        #endregion

        public Dictionary<string, List<BundleSummaryEntry>> Entries
        {
            get { return this.mEntries; }
        }

        public ZipBundleReader(
            string fileName,
            string unpackDirectory,
            IZipFileReaderWrapper zipFileReaderWrapper, 
            ICheckSumGenerator checkSumGenerator, 
            ILogger logger)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("fileName");

            if (string.IsNullOrEmpty(unpackDirectory))
                throw new ArgumentException("unpackDirectory");

            if (zipFileReaderWrapper == null)
                throw new ArgumentNullException("zipFileReaderWrapper");

            if (checkSumGenerator == null)
                throw new ArgumentNullException("checkSumGenerator");

            if (logger == null)
                throw new ArgumentNullException("logger");

            this.mFileName = fileName;
            this.mUnpackDirectory = unpackDirectory;
            this.mZipFileReaderWrapper = zipFileReaderWrapper;
            this.mCheckSumGenerator = checkSumGenerator;
            this.mLogger = logger;

            // Register event for when entries are extracted using the IZipFileReaderWrapper
            this.mZipFileReaderWrapper.OnEntryExtracted += EntryExtractedEventHandler;

            // Create entries Dictionary with default keys
            this.mEntries = new Dictionary<string, List<BundleSummaryEntry>>()
		    {
			    { "DataSources", new List<BundleSummaryEntry>() },
			    { "Reports", new List<BundleSummaryEntry>() },
			    { "Folders", new List<BundleSummaryEntry>() }
		    };
        }

        ~ZipBundleReader()
        {
            if (this.mZipFileReaderWrapper != null)
            {
                this.mZipFileReaderWrapper.OnEntryExtracted -= EntryExtractedEventHandler;
                this.mZipFileReaderWrapper.Dispose();
            }
        }

        public string Extract()
        {
            return this.mZipFileReaderWrapper.UnPack(this.mFileName, this.mUnpackDirectory);
        }

        public void ReadExportSummary()
        {
            //TODO Deserialize <UnpackDirectory>\ExportSummory.json to Dictionary<string, List<BundleSummaryEntry>>
            throw new NotImplementedException();
        }

        public void Read()
        {
            //TODO Iterate through mEntries, verify the checksum for the extracted
            //  entry matches the checksum of the current entry,
            //  and call the respective OnXRead event with this information.
            throw new NotImplementedException();
        }

        private void EntryExtractedEventHandler(IZipFileReaderWrapper sender, ZipEntryReadEvent e)
        {

        }
    }
}