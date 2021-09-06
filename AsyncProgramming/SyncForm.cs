using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AsyncProgramming
{
    public partial class SyncForm : Form
    {
        private static readonly List<string> urls = new List<string>{
            "https://www.lipsum.com/", "https://generator.lorem-ipsum.info/", "https://en.wikipedia.org/wiki/Lorem_ipsum"};

        private long _summaryContentLength;

        public SyncForm()
        {
            InitializeComponent();
        }

        // Обработчик кнопки получения данных от веб-страниц
        private void receiveDataButton_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                _summaryContentLength = 0;
                foreach (var url in urls)
                {
                    var webRequest = WebRequest.Create(url);
                    using (WebResponse webResponse = webRequest.GetResponse())
                    {
                        _summaryContentLength += webResponse.ContentLength;

                        summaryContentLengthTextBox.Text = _summaryContentLength.ToString();
                        executionTimeTextBox.Text = sw.ElapsedMilliseconds.ToString();
                    }
                }
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error");
            }
            finally
            {
                receiveDataButton.Enabled = true;
            }
        }

        private async void buttonAsync_Click(object sender, EventArgs e)
        {
            try
            {
                receiveDataButton.Enabled = false;

                Stopwatch sw = Stopwatch.StartNew();
                _summaryContentLength = 0;
                foreach (var url in urls)
                {
                    var webRequest = WebRequest.Create(url);

                    using (WebResponse webResponse = await webRequest.GetResponseAsync())
                    {
                        _summaryContentLength += webResponse.ContentLength;

                        summaryContentLengthTextBox.Text = _summaryContentLength.ToString();
                        executionTimeTextBox.Text = sw.ElapsedMilliseconds.ToString();
                    }
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error");
            }
            finally
            {
                receiveDataButton.Enabled = true;
            }
        }
    }
}
