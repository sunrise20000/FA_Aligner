using VisionLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static VisionLib.VisionDefinitions;

namespace JPT_TosaTest.Converter
{
    public class IsChecked2RegionOperator : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Enum_REGION_OPERATOR)value == Enum_REGION_OPERATOR.ADD;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Enum_REGION_OPERATOR.ADD : Enum_REGION_OPERATOR.SUB;
        }
    }
}
