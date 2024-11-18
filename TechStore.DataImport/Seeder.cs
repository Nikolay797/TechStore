using Newtonsoft.Json;
using TechStore.Infrastructure.Data.Models.AttributesClasses;
using static TechStore.DataImport.Constants.HeadphoneConstants;
using static TechStore.DataImport.Constants.KeyboardConstants;
using static TechStore.DataImport.Constants.LaptopConstants;
using static TechStore.DataImport.Constants.SmartWatchConstants;
using static TechStore.DataImport.Constants.TelevisionConstants;
using static TechStore.DataImport.Constants.MouseConstants;
using Television = TechStore.Infrastructure.Data.Models.Television;
using Type = TechStore.Infrastructure.Data.Models.AttributesClasses.Type;
using Headphone = TechStore.Infrastructure.Data.Models.Headphone;
using Keyboard = TechStore.Infrastructure.Data.Models.Keyboard;
using Laptop = TechStore.Infrastructure.Data.Models.Laptop;
using Mouse = TechStore.Infrastructure.Data.Models.Mouse;
using SmartWatch = TechStore.Infrastructure.Data.Models.SmartWatch;

namespace TechStore.DataImport
{
    internal class Seeder
    {
        private readonly TechStoreDbContext dbContext;
        private readonly Random random;

        internal Seeder()
        {
            this.dbContext = new TechStoreDbContext();
            this.random = new Random();
        }

        internal void Seed()
        {
            this.SeedLaptops(LaptopsSourcePath);

            this.SeedTelevisions(TelevisionsSourcePath);

            this.SeedKeyboards(KeyboardsSourcePath);

            this.SeedMice(MiceSourcePath);

            this.SeedHeadphones(HeadphonesSourcePath);

            this.SeedSmartwatches(SmartWatchesSourcePath);
        }

        private void SeedLaptops(string laptopsSourcePath)
        {
            string laptopsAsString = File.ReadAllText(laptopsSourcePath);

            var laptopInputModels = JsonConvert.DeserializeObject<IEnumerable<DataCreator.Classes.Laptop>>(laptopsAsString);

            if (laptopInputModels is not null)
            {
                foreach (var laptopInputModel in laptopInputModels)
                {
                    var laptop = new Laptop()
                    {
                        ImageUrl = laptopInputModel.ImageUrl,
                        Warranty = laptopInputModel.Warranty,

                        IsDeleted = false,

                        Price = this.random.Next(LaptopPriceMinValue, LaptopPriceMaxValue),
                        Quantity = this.random.Next(LaptopQuantityMinValue, LaptopQuantityMaxValue),
                        AddedOn = DateTime.UtcNow.Date,
                    };

                    var dbBrand = this.dbContext.Brands.FirstOrDefault(b => b.Name == laptopInputModel.Brand);
                    dbBrand ??= new Brand { Name = laptopInputModel.Brand };
                    laptop.Brand = dbBrand;

                    var dbCpu = this.dbContext.CPUs.FirstOrDefault(cpu => cpu.Name == laptopInputModel.CPU);
                    dbCpu ??= new CPU { Name = laptopInputModel.CPU };
                    laptop.CPU = dbCpu;

                    var dbRam = this.dbContext.RAMs.FirstOrDefault(ram => ram.Value == laptopInputModel.RAM);
                    dbRam ??= new RAM { Value = laptopInputModel.RAM };
                    laptop.RAM = dbRam;

                    var dbSsdCapacity = this.dbContext.SSDCapacities.FirstOrDefault(s => s.Value == laptopInputModel.SSDCapacity);
                    dbSsdCapacity ??= new SSDCapacity { Value = laptopInputModel.SSDCapacity };
                    laptop.SSDCapacity = dbSsdCapacity;

                    var dbVideoCard = this.dbContext.VideoCards.FirstOrDefault(vc => vc.Name == laptopInputModel.VideoCard);
                    dbVideoCard ??= new VideoCard { Name = laptopInputModel.VideoCard };
                    laptop.VideoCard = dbVideoCard;

                    var dbType = this.dbContext.Types.FirstOrDefault(t => t.Name == laptopInputModel.Type);
                    dbType ??= new Type { Name = laptopInputModel.Type };
                    laptop.Type = dbType;

                    var dbDisplaySize = this.dbContext.DisplaySizes.FirstOrDefault(ds => ds.Value == laptopInputModel.DisplaySize);
                    dbDisplaySize ??= new DisplaySize { Value = laptopInputModel.DisplaySize };
                    laptop.DisplaySize = dbDisplaySize;

                    var dbDisplayCoverage = this.dbContext.DisplayCoverages.FirstOrDefault(dc => dc.Name == laptopInputModel.DisplayCoverage);
                    dbDisplayCoverage ??= new DisplayCoverage { Name = laptopInputModel.DisplayCoverage };
                    laptop.DisplayCoverage = dbDisplayCoverage;

                    var dbDisplayTechnology = this.dbContext.DisplayTechnologies.FirstOrDefault(dt => dt.Name == laptopInputModel.DisplayTechnology);
                    dbDisplayTechnology ??= new DisplayTechnology { Name = laptopInputModel.DisplayTechnology };
                    laptop.DisplayTechnology = dbDisplayTechnology;

                    var dbResolution = this.dbContext.Resolutions.FirstOrDefault(r => r.Value == laptopInputModel.Resolution);
                    dbResolution ??= new Resolution { Value = laptopInputModel.Resolution };
                    laptop.Resolution = dbResolution;

                    var dbColor = this.dbContext.Colors.FirstOrDefault(c => c.Name == laptopInputModel.Color);
                    dbColor ??= new Color { Name = laptopInputModel.Color };
                    laptop.Color = dbColor;

                    this.dbContext.Laptops.Add(laptop);

                    this.dbContext.SaveChanges();
                }
            }
        }

        private void SeedTelevisions(string televisionsSourcePath)
        {
            string televisionsAsString = File.ReadAllText(televisionsSourcePath);

            var televisionInputModels = JsonConvert.DeserializeObject<IEnumerable<DataCreator.Classes.Television>>(televisionsAsString);

            if (televisionInputModels is not null)
            {
                foreach (var televisionInputModel in televisionInputModels)
                {
                    var television = new Television()
                    {
                        ImageUrl = televisionInputModel.ImageUrl,
                        Warranty = televisionInputModel.Warranty,

                        IsDeleted = false,

                        Price = this.random.Next(TelevisionPriceMinValue, TelevisionPriceMaxValue),
                        Quantity = this.random.Next(TelevisionQuantityMinValue, TelevisionQuantityMaxValue),
                        AddedOn = DateTime.UtcNow.Date,
                    };

                    var dbBrand = this.dbContext.Brands.FirstOrDefault(b => b.Name == televisionInputModel.Brand);
                    dbBrand ??= new Brand { Name = televisionInputModel.Brand };
                    television.Brand = dbBrand;

                    var dbDisplaySize = this.dbContext.DisplaySizes.FirstOrDefault(ds => ds.Value == televisionInputModel.DisplaySize);
                    dbDisplaySize ??= new DisplaySize { Value = televisionInputModel.DisplaySize };
                    television.DisplaySize = dbDisplaySize;

                    var dbType = this.dbContext.Types.FirstOrDefault(t => t.Name == televisionInputModel.Type);
                    dbType ??= new Type { Name = televisionInputModel.Type };
                    television.Type = dbType;


                    var dbDisplayTechnology = this.dbContext.DisplayTechnologies.FirstOrDefault(dt => dt.Name == televisionInputModel.DisplayTechnology);
                    dbDisplayTechnology ??= new DisplayTechnology { Name = televisionInputModel.DisplayTechnology };
                    television.DisplayTechnology = dbDisplayTechnology;

                    var dbResolution = this.dbContext.Resolutions.FirstOrDefault(r => r.Value == televisionInputModel.Resolution);
                    dbResolution ??= new Resolution { Value = televisionInputModel.Resolution };
                    television.Resolution = dbResolution;

                    var dbColor = this.dbContext.Colors.FirstOrDefault(c => c.Name == televisionInputModel.Color);
                    dbColor ??= new Color { Name = televisionInputModel.Color };
                    television.Color = dbColor;

                    this.dbContext.Televisions.Add(television);

                    this.dbContext.SaveChanges();
                }
            }
        }

        private void SeedKeyboards(string keyboardsSourcePath)
        {
            string keyboardsAsString = File.ReadAllText(keyboardsSourcePath);

            var keyboardInputModels = JsonConvert.DeserializeObject<IEnumerable<DataCreator.Classes.Keyboard>>(keyboardsAsString);

            if (keyboardInputModels is not null)
            {
                foreach (var keyboardInputModel in keyboardInputModels)
                {
                    var keyboard = new Keyboard()
                    {
                        ImageUrl = keyboardInputModel.ImageUrl,
                        Warranty = keyboardInputModel.Warranty,

                        IsDeleted = false,

                        Price = this.random.Next(KeyboardPriceMinValue, KeyboardPriceMaxValue),
                        IsWireless = this.random.Next() % 2 == 1 ? true : false,
                        Quantity = this.random.Next(KeyboardQuantityMinValue, KeyboardQuantityMaxValue),
                        AddedOn = DateTime.UtcNow.Date,
                    };

                    var dbBrand = this.dbContext.Brands.FirstOrDefault(b => b.Name == keyboardInputModel.Brand);
                    dbBrand ??= new Brand { Name = keyboardInputModel.Brand };
                    keyboard.Brand = dbBrand;

                    var dbFormat = this.dbContext.Formats.FirstOrDefault(f => f.Name == keyboardInputModel.Format);
                    dbFormat ??= new Format { Name = keyboardInputModel.Format };
                    keyboard.Format = dbFormat;

                    var dbType = this.dbContext.Types.FirstOrDefault(t => t.Name == keyboardInputModel.Type);
                    dbType ??= new Type { Name = keyboardInputModel.Type };
                    keyboard.Type = dbType;

                    var dbColor = this.dbContext.Colors.FirstOrDefault(c => c.Name == keyboardInputModel.Color);
                    dbColor ??= new Color { Name = keyboardInputModel.Color };
                    keyboard.Color = dbColor;

                    this.dbContext.Keyboards.Add(keyboard);

                    this.dbContext.SaveChanges();
                }
            }
        }

        private void SeedMice(string miceSourcePath)
        {
            string miceAsString = File.ReadAllText(miceSourcePath);

            var mouseInputModels = JsonConvert.DeserializeObject<IEnumerable<DataCreator.Classes.Mouse>>(miceAsString);

            if (mouseInputModels is not null)
            {
                foreach (var mouseInputModel in mouseInputModels)
                {
                    var mouse = new Mouse()
                    {
                        ImageUrl = mouseInputModel.ImageUrl,
                        Warranty = mouseInputModel.Warranty,

                        IsDeleted = false,

                        Price = this.random.Next(MousePriceMinValue, MousePriceMaxValue),
                        IsWireless = this.random.Next() % 2 == 1 ? true : false,
                        Quantity = this.random.Next(MouseQuantityMinValue, MouseQuantityMaxValue),
                        AddedOn = DateTime.UtcNow.Date,
                    };

                    var dbBrand = this.dbContext.Brands.FirstOrDefault(b => b.Name == mouseInputModel.Brand);
                    dbBrand ??= new Brand { Name = mouseInputModel.Brand };
                    mouse.Brand = dbBrand;

                    var dbType = this.dbContext.Types.FirstOrDefault(t => t.Name == mouseInputModel.Type);
                    dbType ??= new Type { Name = mouseInputModel.Type };
                    mouse.Type = dbType;

                    var dbSensitivity = this.dbContext.Sensitivities.FirstOrDefault(s => s.Range == mouseInputModel.Sensitivity);
                    dbSensitivity ??= new Sensitivity { Range = mouseInputModel.Sensitivity };
                    mouse.Sensitivity = dbSensitivity;

                    var dbColor = this.dbContext.Colors.FirstOrDefault(c => c.Name == mouseInputModel.Color);
                    dbColor ??= new Color { Name = mouseInputModel.Color };
                    mouse.Color = dbColor;

                    this.dbContext.Mice.Add(mouse);

                    this.dbContext.SaveChanges();
                }
            }
        }

        private void SeedHeadphones(string headphonesSourcePath)
        {
            string headphonesAsString = File.ReadAllText(headphonesSourcePath);

            var headphoneInputModels = JsonConvert.DeserializeObject<IEnumerable<DataCreator.Classes.Headphone>>(headphonesAsString);

            if (headphoneInputModels is not null)
            {
                foreach (var headphoneInputModel in headphoneInputModels)
                {
                    var headphone = new Headphone()
                    {
                        ImageUrl = headphoneInputModel.ImageUrl,
                        Warranty = headphoneInputModel.Warranty,

                        IsDeleted = false,

                        Price = this.random.Next(HeadphonePriceMinValue, HeadphonePriceMaxValue),
                        IsWireless = this.random.Next() % 2 == 1 ? true : false,
                        HasMicrophone = this.random.Next() % 2 == 1 ? true : false,
                        Quantity = this.random.Next(HeadphoneQuantityMinValue, HeadphoneQuantityMaxValue),
                        AddedOn = DateTime.UtcNow.Date,
                    };

                    var dbBrand = this.dbContext.Brands.FirstOrDefault(b => b.Name == headphoneInputModel.Brand);
                    dbBrand ??= new Brand { Name = headphoneInputModel.Brand };
                    headphone.Brand = dbBrand;

                    var dbType = this.dbContext.Types.FirstOrDefault(t => t.Name == headphoneInputModel.Type);
                    dbType ??= new Type { Name = headphoneInputModel.Type };
                    headphone.Type = dbType;

                    var dbColor = this.dbContext.Colors.FirstOrDefault(c => c.Name == headphoneInputModel.Color);
                    dbColor ??= new Color { Name = headphoneInputModel.Color };
                    headphone.Color = dbColor;

                    this.dbContext.Headphones.Add(headphone);

                    this.dbContext.SaveChanges();
                }
            }
        }

        private void SeedSmartwatches(string smartwatchesSourcePath)
        {
            string smartwatchesAsString = File.ReadAllText(smartwatchesSourcePath);

            var smartwatchInputModels = JsonConvert.DeserializeObject<IEnumerable<DataCreator.Classes.Smartwatch>>(smartwatchesAsString);

            if (smartwatchInputModels is not null)
            {
                foreach (var smartwatchInputModel in smartwatchInputModels)
                {
                    var microphone = new SmartWatch()
                    {
                        ImageUrl = smartwatchInputModel.ImageUrl,
                        Warranty = smartwatchInputModel.Warranty,

                        IsDeleted = false,

                        Price = this.random.Next(SmartWatchesPriceMinValue, SmartWatchesPriceMaxValue),
                        Quantity = this.random.Next(SmartWatchesQuantityMinValue, SmartWatchesQuantityMaxValue),
                        AddedOn = DateTime.UtcNow.Date,
                    };

                    var dbBrand = this.dbContext.Brands.FirstOrDefault(b => b.Name == smartwatchInputModel.Brand);
                    dbBrand ??= new Brand { Name = smartwatchInputModel.Brand };
                    microphone.Brand = dbBrand;

                    var dbColor = this.dbContext.Colors.FirstOrDefault(c => c.Name == smartwatchInputModel.Color);
                    dbColor ??= new Color { Name = smartwatchInputModel.Color };
                    microphone.Color = dbColor;

                    this.dbContext.SmartWatches.Add(microphone);

                    this.dbContext.SaveChanges();
                }
            }
        }
    }
}
