using System;
using System.Diagnostics;
using System.IO;

namespace LSystemsMG.Util.External
{
    public class FileWriter : IDisposable
    {
        private StreamWriter sw;
        private bool writeAsHtml = false;
        private bool swOpen = true;

        public FileWriter(string outputFilenamepath, bool writeAsHtml = false)
        {
            this.writeAsHtml= writeAsHtml;
            Debug.WriteLine($"Writing to {outputFilenamepath}");
            sw = new StreamWriter(outputFilenamepath);
            sw.NewLine = "\n";
        }

        public void CloseStreamWriter()
        {
            if (writeAsHtml)
            {
                WriteHtmlFooter();
            }
            sw.Close();
            swOpen = false;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && sw != null)
            {
                if (swOpen)
                {
                    CloseStreamWriter();
                }
                sw.Dispose();
                sw = null;
            }
        }

        public void Write(string text)
        {
            sw.Write(text);
        }

        public void WriteLine(string text)
        {
            sw.WriteLine(text);
        }

        public void WriteHtmlHeader(string browserTitle, string pageHeader)
        {
            writeAsHtml = true;
            string html_header = "" +
                $"<!DOCTYPE html>\n<html>\n<head>\n  <title>{browserTitle}</title>\n" +
                $"  <link href='/includes/styles.css' rel='stylesheet' type='text/css' />\n" +
                $"</head>\n<body>\n<b>{pageHeader}</b>\n<pre>";
            sw.WriteLine(html_header);
        }

        private void WriteHtmlFooter()
        {
            sw.WriteLine("</pre>\n</html>");
        }
    }
}

