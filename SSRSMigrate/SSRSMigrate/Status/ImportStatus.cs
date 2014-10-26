﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSRSMigrate.Status
{
    public class ImportStatus
    {
        private string mFromFileName;
        private string mFromPath;
        private string[] mErrors;
        private bool mSuccess;

        public string FromFileName
        {
            get { return mFromFileName; }
            protected set { mFromFileName = value; }
        }

        public string FromPath
        {
            get { return mFromPath; }
            protected set { mFromPath = value; }
        }

        public string[] Errors
        {
            get { return mErrors; }
            protected set { mErrors = value; }
        }

        public bool Success
        {
            get { return mSuccess; }
            protected set { mSuccess = value; }
        }

        public ImportStatus(
            string fromFileName,
            string fromPath,
            string[] errors,
            bool success)
        {
            if (string.IsNullOrEmpty(fromFileName))
                throw new ArgumentException("patfromFileNamehTo");

            if (string.IsNullOrEmpty(fromPath))
                throw new ArgumentException("fromPath");

            this.mFromFileName = fromFileName;
            this.mFromPath = fromPath;
            this.mErrors = errors;
            this.mSuccess = success;
        }
    }
}
