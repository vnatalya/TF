using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TF.Renderers
{
    public class BorderlessButton : Button
    {
        public BorderlessButton() : base()
        {
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            this.BackgroundColor = Color.White;
            this.TextColor = Color.Green;
        }
    }
}
