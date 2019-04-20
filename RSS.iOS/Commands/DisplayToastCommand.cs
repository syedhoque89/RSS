using CoreGraphics;
using RSS.Commands;
using RSS.iOS.Commands;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(DisplayToastCommand))]

namespace RSS.iOS.Commands
{
	public class DisplayToastCommand : IDisplayToastCommand
	{
		public void Execute(string message, bool longDuration = false)
		{
			var keyWindow = UIApplication.SharedApplication.KeyWindow;
			var toastView = new UILabel
			{
				Text = message,
				TextColor = UIColor.White,
				BackgroundColor = UIColor.Black.ColorWithAlpha(0.8f),
				TextAlignment = UITextAlignment.Center
			};
			toastView.Layer.CornerRadius = 20;
			toastView.Layer.MasksToBounds = true;

			var size = toastView.SizeThatFits(keyWindow.Frame.Size);
			var sizeWithBorder = new CGSize(size.Width + 20, size.Height + 20);
			var left = (keyWindow.Frame.Width - sizeWithBorder.Width) / 2;
			var top = keyWindow.Frame.Height - sizeWithBorder.Height - 20;
			toastView.Frame = new CGRect(new CGPoint(left, top), sizeWithBorder);

			keyWindow.AddSubview(toastView);
			UIView.Animate(longDuration ? 2.0f : 1.0f, 3.0f, UIViewAnimationOptions.CurveEaseOut,
						   () => toastView.Alpha = 0.0f,
						   () => toastView.RemoveFromSuperview());
		}
	}
}