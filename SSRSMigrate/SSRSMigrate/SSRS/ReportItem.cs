﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSRSMigrate.SSRS
{
    public class ReportItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public string ID { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Size { get; set; }
        public string VirtualPath { get; set; }
        public byte[] Definition { get; set; }
        public List<ReportItem> SubReports { get; set; }

        public ReportItem()
        {
            this.SubReports = new List<ReportItem>();
        }
    }
}
