using System;
using System.Globalization;
using Xamarin.Forms;

namespace RSS.ValueConverters
{
	public class BoolReverseConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool val)
				return !val;
			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}