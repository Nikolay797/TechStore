using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DataImport
{
    internal class Constants
    {
        internal static class ConfigurationConstants
        {
            internal const string ConnectionString =
                "Server=DESKTOP-KMMU50S;Database=TechStoreDB24;Trusted_Connection=True;TrustServerCertificate=True";
        }

        internal static class LaptopConstants
        {
            internal const string LaptopsSourcePath = "../../../../TechStore.DataCreator/Datasets/laptops.json";
            internal const int LaptopPriceMinValue = 600;
            internal const int LaptopPriceMaxValue = 5000;
            internal const int LaptopQuantityMinValue = 1;
            internal const int LaptopQuantityMaxValue = 10;
        }

        internal static class TelevisionConstants
        {
            internal const string TelevisionsSourcePath = "../../../../TechStore.DataCreator/Datasets/televisions.json";
            internal const int TelevisionPriceMinValue = 500;
            internal const int TelevisionPriceMaxValue = 8000;
            internal const int TelevisionQuantityMinValue = 1;
            internal const int TelevisionQuantityMaxValue = 10;
        }

        internal static class KeyboardConstants
        {
            internal const string KeyboardsSourcePath = "../../../../TechStore.DataCreator/Datasets/keyboards.json";
            internal const int KeyboardPriceMinValue = 10;
            internal const int KeyboardPriceMaxValue = 200;
            internal const int KeyboardQuantityMinValue = 1;
            internal const int KeyboardQuantityMaxValue = 10;
        }

        internal static class MouseConstants
        {
            internal const string MiceSourcePath = "../../../../TechStore.DataCreator/Datasets/mice.json";
            internal const int MousePriceMinValue = 10;
            internal const int MousePriceMaxValue = 200;
            internal const int MouseQuantityMinValue = 1;
            internal const int MouseQuantityMaxValue = 10;
        }

        internal static class HeadphoneConstants
        {
            internal const string HeadphonesSourcePath = "../../../../TechStore.DataCreator/Datasets/headphones.json";
            internal const int HeadphonePriceMinValue = 30;
            internal const int HeadphonePriceMaxValue = 500;
            internal const int HeadphoneQuantityMinValue = 1;
            internal const int HeadphoneQuantityMaxValue = 10;
        }

        internal static class SmartWatchConstants
        {
            internal const string SmartWatchesSourcePath = "../../../../TechStore.DataCreator/Datasets/smartwatches.json";
            internal const int SmartWatchesPriceMinValue = 20;
            internal const int SmartWatchesPriceMaxValue = 500;
            internal const int SmartWatchesQuantityMinValue = 1;
            internal const int SmartWatchesQuantityMaxValue = 10;
        }
    }
}
