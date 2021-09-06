using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RxSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var frm = new Form { Controls = { } };

            var xl = from e in Observable.FromEventPattern<MouseEventArgs>(frm, "MouseMove")
                     let l = e.EventArgs.Location
                     where l.X == l.Y
                     select l;

            xl.Subscribe(x => Console.WriteLine(x));

            Application.Run(frm);

            Console.ReadLine();

        }
    }
}
