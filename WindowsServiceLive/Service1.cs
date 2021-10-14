using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsServiceLive
{
    public partial class Service1 : ServiceBase
    {
        Timer tm = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service starts at " + DateTime.Now);
            tm.Elapsed += new ElapsedEventHandler(onElapsedTime);
            tm.Enabled = true;
            tm.Interval = 1000;
        }

        private void onElapsedTime(object sender, ElapsedEventArgs e)
        {
            WriteToFile("Service recalls at " + DateTime.Now);
        }

        protected override void OnStop()
        {
            WriteToFile("Service stops at " + DateTime.Now);
        }

        private void WriteToFile(string message)
        {
            string folderpath = AppDomain.CurrentDomain.BaseDirectory + "ServiceLogPath";
            
            if (!Directory.Exists(folderpath))
                Directory.CreateDirectory(folderpath);

            string filepath = folderpath + @"\" + "logfile" + ".txt";

            if (!File.Exists(filepath))
            {
                using(StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(message);
                }
            }
        }
    }
}
