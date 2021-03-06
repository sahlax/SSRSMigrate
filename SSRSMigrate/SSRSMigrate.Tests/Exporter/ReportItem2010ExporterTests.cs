﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SSRSMigrate.Exporter;
using SSRSMigrate.Exporter.Writer;
using Moq;
using SSRSMigrate.SSRS.Item;
using SSRSMigrate.Status;
using SSRSMigrate.TestHelper;
using System.IO;

namespace SSRSMigrate.Tests.Exporter
{
    [TestFixture]
    [CoverageExcludeAttribute]
    class ReportItem2010ExporterTests
    {
        ReportItemExporter exporter = null;
        Mock<IExportWriter> exportWriterMock = null;
        ReportItem reportItem;
        string testReportPath = "Test AW Reports\\2010\\Company Sales.rdl";

        #region Expected Values
        string expectedReportItemFileName = "C:\\temp\\SSRSMigrate_AW_Tests\\Reports\\Company Sales.rdl";
        #endregion

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            reportItem = new ReportItem()
            {
                Name = "Company Sales",
                Path = "/SSRSMigrate_AW_Tests/Reports/Company Sales",
                Description = null,
                ID = "16d599e6-9c87-4ebc-b45b-5a47e3c73746",
                VirtualPath = null,
                Definition = TesterUtility.StringToByteArray(TesterUtility.LoadRDLFile(testReportPath))
            };

            exportWriterMock = new Mock<IExportWriter>();

            exportWriterMock.Setup(e => e.Save(It.IsAny<string>(), It.IsAny<byte[]>(), true));

            // Mock IExporter.Save where the filename exists but overwrite = false
            exportWriterMock.Setup(e => e.Save(expectedReportItemFileName, It.IsAny<byte[]>(), false))
                .Throws(new IOException(string.Format("File '{0}' already exists.", expectedReportItemFileName)));

            exporter = new ReportItemExporter(exportWriterMock.Object);
        }

        #region Constructor Tests
        [Test]
        public void Constructor_Succeed()
        {
            ReportItemExporter reportItemExporter = new ReportItemExporter(exportWriterMock.Object);

            Assert.NotNull(reportItemExporter);
        }

        [Test]
        public void Constructor_NullIExportWriter()
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(
                delegate
                {
                    ReportItemExporter reportItemExporter = new ReportItemExporter(null);
                });

            Assert.That(ex.Message, Is.EqualTo("Value cannot be null.\r\nParameter name: exportWriter"));

        }
        #endregion

        #region SaveItem Tests
        [Test]
        public void SaveItem()
        {
            ExportStatus actualStatus = exporter.SaveItem(reportItem, expectedReportItemFileName, true);

            exportWriterMock.Verify(e => e.Save(expectedReportItemFileName, reportItem.Definition, true));

            Assert.True(actualStatus.Success);
            Assert.AreEqual(expectedReportItemFileName, actualStatus.ToPath);
            Assert.AreEqual(reportItem.Path, actualStatus.FromPath);
        }

        [Test]
        public void SaveItem_FileExists_DontOverwrite()
        {
            ExportStatus actualStatus = exporter.SaveItem(reportItem, expectedReportItemFileName, false);

            Assert.False(actualStatus.Success);
            Assert.NotNull(actualStatus.Errors);
            Assert.AreEqual(actualStatus.Errors[0], string.Format("File '{0}' already exists.", expectedReportItemFileName));
        }

        [Test]
        public void SaveItem_NullItem()
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(
               delegate
               {
                   exporter.SaveItem(null, expectedReportItemFileName);
               });

            Assert.That(ex.Message, Is.EqualTo("Value cannot be null.\r\nParameter name: item"));
        }

        [Test]
        public void SaveItem_NullFileName()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(
                delegate
                {
                    exporter.SaveItem(reportItem, null);
                });

            Assert.That(ex.Message, Is.EqualTo("fileName"));
        }

        [Test]
        public void SaveItem_EmptyFileName()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(
               delegate
               {
                   exporter.SaveItem(reportItem, "");
               });

            Assert.That(ex.Message, Is.EqualTo("fileName"));
        }
        #endregion
    }
}
