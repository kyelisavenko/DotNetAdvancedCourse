using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RxBinding
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var o = Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove");
            var filtered = o.Where(x => x.EventArgs.X < Width/2);
            filtered.Subscribe(x => label1.Text = $"{x.EventArgs.X} {x.EventArgs.Y}");
        }
    }
}
