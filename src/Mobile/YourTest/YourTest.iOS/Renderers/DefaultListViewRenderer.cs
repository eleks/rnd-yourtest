using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using YourTest.iOS.Renderers;

[assembly: ExportRenderer(typeof(ListView), typeof(DefaultListViewRenderer))]
namespace YourTest.iOS.Renderers
{
    public class DefaultListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.TableFooterView = new UIView();
            }
        }
    }
}
