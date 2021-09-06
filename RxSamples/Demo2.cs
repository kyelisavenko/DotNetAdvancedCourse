using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PlayingWithRx
{
    class CustomObserver : IObserver<string>
    {
        public void OnNext(string value)
        {
            Console.WriteLine("CustomObserver.OnNext: {0}", value);
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
    }
    class Demo2
    {
        public static void TextBoxDemo()
        {
            var textBox = new TextBox();
            var frm = new Form {Controls = {textBox}};

            IObservable<string> xs1 = (from e in Observable.FromEventPattern(textBox, "TextChanged")
                                      let txt = (TextBox) e.Sender
                                      select txt.Text);
            
            var xs = xs1
                     .Throttle(TimeSpan.FromMilliseconds(200))
                     .DistinctUntilChanged();

            var disposable = xs.Subscribe(x => Console.WriteLine(x));
            
            Application.Run(frm);
        }

        
        public void MouseMoveDemo()
        {
            Form frm = new Form();
            var xs = from e in Observable.FromEventPattern<MouseEventArgs>(frm, "MouseMove")
                     let l = e.EventArgs.Location
                     where l.X == l.Y
                     select l;

            xs.Subscribe(
                l => Console.WriteLine("From simple subscriber " + l));
            //xs.Subscribe(l =>
            //                 {
            //                     Console.WriteLine("Begin...");
            //                     Console.WriteLine(l);
            //                     Thread.Sleep(200);
            //                     Console.WriteLine("End...");
            //                 });
            Application.Run(frm);
        }



        
    }
}
