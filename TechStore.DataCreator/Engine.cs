using Newtonsoft.Json;
using TechStore.DataCreator.Classes;
using TechStore.DataCreator.Classes.AttributesClasses;
using Type = TechStore.DataCreator.Classes.AttributesClasses.Type;
using static TechStore.DataCreator.Constants;
using TechStore.Infrastructure.Data.Models;
using Headphone = TechStore.DataCreator.Classes.Headphone;
using Keyboard = TechStore.DataCreator.Classes.Keyboard;
using Laptop = TechStore.DataCreator.Classes.Laptop;
using Mouse = TechStore.DataCreator.Classes.Mouse;
using Television = TechStore.DataCreator.Classes.Television;

namespace TechStore.DataCreator
{
    internal class Engine
    {
        private readonly Random randomNumber;

        internal Engine()
        {
            this.randomNumber = new Random();
        }

        internal Brand Brands => this.Deserialize<Brand>("InitialSourceFiles/brands.json");

        internal Color Colors => this.Deserialize<Color>("InitialSourceFiles/colors.json");

        internal CPU CPUs => this.Deserialize<CPU>("InitialSourceFiles/cpus.json");

        internal DisplayCoverage DisplayCoverages => this.Deserialize<DisplayCoverage>(
                                                   "InitialSourceFiles/displayCoverages.json");

        internal DisplaySize DisplaySizes => this.Deserialize<DisplaySize>("InitialSourceFiles/displaySizes.json");

        internal DisplayTechnology DisplayTechnologies => this.Deserialize<DisplayTechnology>(
                                                        "InitialSourceFiles/displayTechnologies.json");

        internal Format Formats => this.Deserialize<Format>("InitialSourceFiles/formats.json");

        internal ImageUrl ImageUrls => this.Deserialize<ImageUrl>("InitialSourceFiles/imageURLs.json");

        internal RAM RAMs => this.Deserialize<RAM>("InitialSourceFiles/rams.json");

        internal Resolution Resolutions => this.Deserialize<Resolution>("InitialSourceFiles/resolutions.json");

        internal Sensitivity Sensitivities => this.Deserialize<Sensitivity>("InitialSourceFiles/sensitivities.json");

        internal SSDCapacity SSDCapacities => this.Deserialize<SSDCapacity>("InitialSourceFiles/ssdCapacities.json");

        internal Type Types => this.Deserialize<Type>("InitialSourceFiles/types.json");

        internal VideoCard VideoCards => this.Deserialize<VideoCard>("InitialSourceFiles/videoCards.json");

        internal Warrantly Warranties => this.Deserialize<Warrantly>("InitialSourceFiles/warranties.json");

        internal void Run()
        {
            var laptops = this.CreateLaptops();

            this.WriteToFileAsJson(laptops, "../../../Datasets/laptops.json");

            var televisions = this.CreateTelevisions();

            this.WriteToFileAsJson(televisions, "../../../Datasets/televisions.json");

            var keyboards = this.CreateKeyboards();

            this.WriteToFileAsJson(keyboards, "../../../Datasets/keyboards.json");

            var mice = this.CreateMice();

            this.WriteToFileAsJson(mice, "../../../Datasets/mice.json");

            var headphones = this.CreateHeadphones();

            this.WriteToFileAsJson(headphones, "../../../Datasets/headphones.json");

            var smartwatches = this.CreateSmartwatches();

            this.WriteToFileAsJson(smartwatches, "../../../Datasets/smartwatches.json");
        }

        private T Deserialize<T>(string destinationPath)
        {
            string jsonAsString = File.ReadAllText(destinationPath);

            return JsonConvert.DeserializeObject<T>(jsonAsString);
        }

        private List<Laptop> CreateLaptops()
        {
            var laptops = new List<Laptop>(LaptopsCount);

            for (int i = 0; i < LaptopsCount; i++)
            {
                var laptop = new Laptop()
                {
                    Brand = this.Brands.LaptopBrands[randomNumber.Next(this.Brands.LaptopBrands.Count)],
                    Color = this.Colors.LaptopColors[randomNumber.Next(this.Colors.LaptopColors.Count)],
                    ImageUrl = this.ImageUrls.LaptopImageUrls[randomNumber.Next(this.ImageUrls.LaptopImageUrls.Count)],
                    Warranty = this.Warranties.Warranties[randomNumber.Next(this.Warranties.Warranties.Count)],
                    CPU = this.CPUs.LaptopCPUs[randomNumber.Next(this.CPUs.LaptopCPUs.Count)],
                    RAM = this.RAMs.LaptopRAMs[randomNumber.Next(this.RAMs.LaptopRAMs.Count)],
                    SSDCapacity = this.SSDCapacities.LaptopSSDCapacities[randomNumber
                                                    .Next(this.SSDCapacities.LaptopSSDCapacities.Count)],
                    VideoCard = this.VideoCards.LaptopVideoCards[randomNumber
                                               .Next(this.VideoCards.LaptopVideoCards.Count)],
                    Type = this.Types.LaptopTypes[randomNumber.Next(this.Types.LaptopTypes.Count)],
                    DisplaySize = this.DisplaySizes.LaptopDisplaySizes[randomNumber
                                                   .Next(this.DisplaySizes.LaptopDisplaySizes.Count)],
                    DisplayCoverage = this.DisplayCoverages.DisplayCoverages[randomNumber
                                                           .Next(this.DisplayCoverages.DisplayCoverages.Count)],
                    DisplayTechnology = this.DisplayTechnologies.DisplayTechnologies[randomNumber
                                                               .Next(this.DisplayTechnologies.DisplayTechnologies.Count)],
                    Resolution = this.Resolutions.LaptopResolutions[randomNumber
                                                 .Next(this.Resolutions.LaptopResolutions.Count)],
                };

                laptops.Add(laptop);
            }

            return laptops;
        }

        private List<Television> CreateTelevisions()
        {
            var televisions = new List<Television>(TelevisionsCount);

            for (int i = 0; i < TelevisionsCount; i++)
            {
                var television = new Television()
                {
                    Brand = this.Brands.TelevisionBrands[randomNumber.Next(this.Brands.TelevisionBrands.Count)],
                    Color = this.Colors.TelevisionColors[randomNumber.Next(this.Colors.TelevisionColors.Count)],
                    ImageUrl = this.ImageUrls.TelevisionImageUrls[randomNumber.Next(this.ImageUrls.TelevisionImageUrls.Count)],
                    Warranty = this.Warranties.Warranties[randomNumber.Next(this.Warranties.Warranties.Count)],
                    DisplaySize = this.DisplaySizes.TelevisionDisplaySizes[randomNumber
                                                   .Next(this.DisplaySizes.TelevisionDisplaySizes.Count)],
                    Type = this.Types.TelevisionTypes[randomNumber.Next(this.Types.TelevisionTypes.Count)],
                    DisplayTechnology = this.DisplayTechnologies.DisplayTechnologies[randomNumber
                                                               .Next(this.DisplayTechnologies.DisplayTechnologies.Count)],
                    Resolution = this.Resolutions.TelevisionResolutions[randomNumber
                                                 .Next(this.Resolutions.TelevisionResolutions.Count)],
                };

                televisions.Add(television);
            }

            return televisions;
        }

        private List<Keyboard> CreateKeyboards()
        {
            var keyboards = new List<Keyboard>(KeyboardsCount);

            for (int i = 0; i < KeyboardsCount; i++)
            {
                var keyboard = new Keyboard()
                {
                    Brand = this.Brands.KeyboardBrands[randomNumber.Next(this.Brands.KeyboardBrands.Count)],
                    Color = this.Colors.KeyboardColors[randomNumber.Next(this.Colors.KeyboardColors.Count)],
                    ImageUrl = this.ImageUrls.KeyboardImageUrls[randomNumber
                                             .Next(this.ImageUrls.KeyboardImageUrls.Count)],
                    Warranty = this.Warranties.Warranties[randomNumber.Next(this.Warranties.Warranties.Count)],
                    Format = this.Formats.KeyboardFormats[randomNumber.Next(this.Formats.KeyboardFormats.Count)],
                    Type = this.Types.KeyboardTypes[randomNumber.Next(this.Types.KeyboardTypes.Count)],
                };

                keyboards.Add(keyboard);
            }

            return keyboards;
        }

        private List<Mouse> CreateMice()
        {
            var mice = new List<Mouse>(MiceCount);

            for (int i = 0; i < MiceCount; i++)
            {
                var mouse = new Mouse()
                {
                    Brand = this.Brands.MouseBrands[randomNumber.Next(this.Brands.MouseBrands.Count)],
                    Color = this.Colors.MouseColors[randomNumber.Next(this.Colors.MouseColors.Count)],
                    ImageUrl = this.ImageUrls.MouseImageUrls[randomNumber.Next(this.ImageUrls.MouseImageUrls.Count)],
                    Warranty = this.Warranties.Warranties[randomNumber.Next(this.Warranties.Warranties.Count)],
                    Type = this.Types.MouseTypes[randomNumber.Next(this.Types.MouseTypes.Count)],
                    Sensitivity = this.Sensitivities.MouseSensitivities[randomNumber
                                                    .Next(this.Sensitivities.MouseSensitivities.Count)],
                };

                mice.Add(mouse);
            }

            return mice;
        }

        private List<Headphone> CreateHeadphones()
        {
            var headphones = new List<Headphone>(HeadphonesCount);

            for (int i = 0; i < HeadphonesCount; i++)
            {
                var headphone = new Headphone()
                {
                    Brand = this.Brands.HeadphoneBrands[randomNumber.Next(this.Brands.HeadphoneBrands.Count)],
                    Color = this.Colors.HeadphoneColors[randomNumber.Next(this.Colors.HeadphoneColors.Count)],
                    ImageUrl = this.ImageUrls.HeadphoneImageUrls[randomNumber
                                             .Next(this.ImageUrls.HeadphoneImageUrls.Count)],
                    Warranty = this.Warranties.Warranties[randomNumber.Next(this.Warranties.Warranties.Count)],
                    Type = this.Types.HeadphoneTypes[randomNumber.Next(this.Types.HeadphoneTypes.Count)],
                };

                headphones.Add(headphone);
            }
            return headphones;
        }

        private List<Smartwatch> CreateSmartwatches()
        {
            var smartwatches = new List<Smartwatch>(SmartwatchesCount);

            for (int i = 0; i < SmartwatchesCount; i++)
            {
                var smartwatch = new Smartwatch()
                {
                    Brand = this.Brands.SmartWatchBrands[randomNumber.Next(this.Brands.SmartWatchBrands.Count)],
                    Color = this.Colors.SmartWatchColors[randomNumber.Next(this.Colors.SmartWatchColors.Count)],
                    ImageUrl = this.ImageUrls.SmartWatchImageUrls[randomNumber
                                             .Next(this.ImageUrls.SmartWatchImageUrls.Count)],
                    Warranty = this.Warranties.Warranties[randomNumber.Next(this.Warranties.Warranties.Count)],
                };

                smartwatches.Add(smartwatch);
            }

            return smartwatches;
        }

        private void WriteToFileAsJson(IEnumerable<object> collection, string destinationPath)
        {
            string collectionAsJson = JsonConvert.SerializeObject(collection, Formatting.Indented);

            File.WriteAllText(destinationPath, collectionAsJson);
        }
    }
}
