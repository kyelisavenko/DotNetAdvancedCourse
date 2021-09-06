using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigAwait
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            Task<string> t = Task.Factory.StartNew(()=>
            {
                Thread.Sleep(TimeSpan.FromSeconds(10));
                return "WOW!";
            });
            label1.Text = await t.ConfigureAwait(false);
        }
    }
}
