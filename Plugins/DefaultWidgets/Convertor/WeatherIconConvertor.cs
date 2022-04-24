using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DefaultWidgets.Convertor
{


    internal class WeatherIconConvertor: IValueConverter
	{
		Dictionary<int, string> _icons = new Dictionary<int, string>
		{
			{23, "RainShowersDayV2" },
			{26, "SnowShowersDayV2"},
			{6, "BlowingHailV2"},
			{5, "CloudyV3"},
			{20, "LightSnowV2"},
			{91, "WindyV2"},
			{27, "ThunderstormsV2"},
			{10, "FreezingRainV2"},
			{77, "RainSnowV2"},
			{12, "HazySmokeV2"},
			{39, "HazeSmokeNightV2_106"},
			{24, "RainSnowV2"},
			{78, "RainSnowShowersNightV2"},
			{9, "FogV2"},
			{3, "PartlyCloudyDayV3"},
			{43, "HailNightV2"},
			{16, "HailDayV2"},
			{8, "LightRainV2"},
			{15, "HeavySnowV2"},
			{28, "ClearNightV3"},
			{30, "PartlyCloudyNightV2"},
			{14, "ModerateRainV2"},
			{1, "SunnyDayV3"},
			{7, "BlowingSnowV2"},
			{50, "RainShowersNightV2"},
			{82, "SnowShowersNightV2"},
			{2, "MostlySunnyDay"},
			{29, "MostlyClearNight"},
			{4, "MostlyCloudyDayV2"},
			{31, "MostlyCloudyNightV2"},
			{19, "LightRainV3"},
			{17, "LightRainShowerDay"},
			{44, "LightRainShowerNight" }

			};

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var iconIndex =(int) float.Parse(value.ToString());
            var iconIndex =(int) float.Parse(value.ToString());

			return $"https://assets.msn.cn/bundles/v1/weather/latest/{_icons[iconIndex]}.svg";

		}

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
