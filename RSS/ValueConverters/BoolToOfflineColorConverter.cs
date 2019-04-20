using System;
using System.Globalization;
using Xamarin.Forms;

namespace RSS.ValueConverters
{
	public class BoolToOfflineColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool val))
				return Color.Transparent;

			if (val)
				return (Color) Application.Current.Resources["SavedColor"];

			return (Color) Application.Current.Resources["OfflineColor"];
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}