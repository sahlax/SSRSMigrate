﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SSRSMigrate.Forms
{
    public partial class DebugForm : Form
    {
        #region Public Methods
        /// <summary>
        /// Adds a message to the debug logger.
        /// </summary>
        /// <param name="Message">The message text.</param>
        public void LogMessage(string message)
        {
            if (message == null)
                message = "";

            StackTrace stackTrace = new StackTrace();
            var callerMethod = new StackTrace().GetFrame(1).GetMethod();
            if (callerMethod != null)
            {
                var callerClass = callerMethod.ReflectedType.Name;
                string caller = string.Format("{0}.{1}", callerClass, callerMethod.Name);

                ListViewItem oItem = new ListViewItem(DateTime.Now.ToString());
                oItem.SubItems.Add(caller);
                oItem.SubItems.Add(message);
                oItem.ToolTipText = message;

                lstDebug.Invoke(new Action(() => lstDebug.Items.Add(oItem)));
                lstDebug.Invoke(new Action(() => oItem.EnsureVisible()));
            }
        }

        /// <summary>
        /// Adds a message to the debug logger.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <param name="isWarning">if set to <c>true</c> [is warning].</param>
        public void LogMessage(string message, bool isWarning = false)
        {
            if (message == null)
                message = "";

            StackTrace stackTrace = new StackTrace();
            var callerMethod = new StackTrace().GetFrame(1).GetMethod();
            if (callerMethod != null)
            {
                var callerClass = callerMethod.ReflectedType.Name;
                string caller = string.Format("{0}.{1}", callerClass, callerMethod.Name);

                ListViewItem oItem = new ListViewItem(DateTime.Now.ToString());
                oItem.SubItems.Add(caller);
                oItem.SubItems.Add(message);
                oItem.ToolTipText = message;

                if (isWarning)
                    oItem.ForeColor = Color.OrangeRed;

                lstDebug.Invoke(new Action(() => lstDebug.Items.Add(oItem)));
                lstDebug.Invoke(new Action(() => oItem.EnsureVisible()));
            }
        }

        /// <summary>
        /// Adds an exception to the debug logger.
        /// </summary>
        /// <param name="Message">The message text.</param>
        /// <param name="Exception">The exception object.</param>
        public void LogMessage(string message, Exception exception)
        {
            if (message == null)
                message = "";

            string sException = exception.GetType().ToString();

            if (sException == null)
                sException = "";

            StackTrace stackTrace = new StackTrace();
            var callerMethod = new StackTrace().GetFrame(1).GetMethod();
            if (callerMethod != null)
            {
                var callerClass = callerMethod.ReflectedType.Name;
                string caller = string.Format("{0}.{1}", callerClass, callerMethod.Name);

                ListViewItem oItem = new ListViewItem(DateTime.Now.ToString());
                oItem.SubItems.Add(caller);
                oItem.SubItems.Add(message);
                oItem.SubItems.Add(sException);
                oItem.ForeColor = Color.Red;
                oItem.ToolTipText = message;

                //lstDebug.Items.Add(oItem);
                lstDebug.Invoke(new Action(() => lstDebug.Items.Add(oItem)));
                lstDebug.Invoke(new Action(() => oItem.EnsureVisible()));
            }
        }
        #endregion

        public DebugForm()
        {
            InitializeComponent();
        }

        private void DebugForm_Load(object sender, EventArgs e)
        {

        }

        private void DebugForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
