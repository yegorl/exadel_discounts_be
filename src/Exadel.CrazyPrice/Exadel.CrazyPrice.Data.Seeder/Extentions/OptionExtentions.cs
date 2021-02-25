using Exadel.CrazyPrice.Data.Seeder.Models.Option;
using System;

namespace Exadel.CrazyPrice.Data.Seeder.Extentions
{
    public static class OptionExtentions
    {
        public static DestinationOption ToDestinationOption(this string key, DestinationOption defaultValue = DestinationOption.mg, bool raiseException = false)
        {
            if (Enum.TryParse(key, true, out DestinationOption value))
            {
                return value;
            }
            else
            {
                return raiseException ? throw new ArgumentException($"{key} is not DestinationOption value.") : defaultValue;
            }
        }
    }
}
