// See https://aka.ms/new-console-template for more information
using System;



    public abstract class Appliance
    {
        public string ItemNumber { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
        public int Wattage { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }

        public Appliance(string itemNumber, string brand, int quantity, int wattage, string color, double price)
        {
            ItemNumber = itemNumber;
            Brand = brand;
            Quantity = quantity;
            Wattage = wattage;
            Color = color;
            Price = price;
        }

        public abstract override string ToString();
    }


    public class Refrigerator : Appliance
    {
        public int NumberOfDoors { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public Refrigerator(string itemNumber, string brand, int quantity, int wattage, string color, double price,
            int numberOfDoors, int height, int width)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            NumberOfDoors = numberOfDoors;
            Height = height;
            Width = width;
        }

        public override string ToString()
        {
            return $"Item Number: {ItemNumber}\nBrand: {Brand}\nQuantity: {Quantity}\nWattage: {Wattage}\nColor: {Color}\nPrice: {Price}\nNumber of Doors: {NumberOfDoors}\nHeight: {Height}\nWidth: {Width}";
        }
    }


    public class Vacuum : Appliance
    {
        public string Grade { get; set; }
        public int BatteryVoltage { get; set; }

        public Vacuum(string itemNumber, string brand, int quantity, int wattage, string color, double price,
            string grade, int batteryVoltage)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            Grade = grade;
            BatteryVoltage = batteryVoltage;
        }

        public override string ToString()
        {
            return $"Item Number: {ItemNumber}\nBrand: {Brand}\nQuantity: {Quantity}\nWattage: {Wattage}\nColor: {Color}\nPrice: {Price}\nGrade: {Grade}\nBattery Voltage: {BatteryVoltage}";
        }
    }

    public class Microwave : Appliance
    {
        public double Capacity { get; set; }
        public string RoomType { get; set; }

        public Microwave(string itemNumber, string brand, int quantity, int wattage, string color, double price,
            double capacity, string roomType)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            Capacity = capacity;
            RoomType = roomType;
        }

        public override string ToString()
        {
            return $"Item Number: {ItemNumber}\nBrand: {Brand}\nQuantity: {Quantity}\nWattage: {Wattage}\nColor: {Color}\nPrice: {Price}\nCapacity: {Capacity}\nRoom Type: {RoomType}";
        }
    }


    public class Dishwasher : Appliance
    {
        public string Feature { get; set; }
        public string SoundRating { get; set; }

        public Dishwasher(string itemNumber, string brand, int quantity, int wattage, string color, double price,
            string feature, string soundRating)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            Feature = feature;
            SoundRating = soundRating;
        }

        public override string ToString()
        {
            return $"Item Number: {ItemNumber}\nBrand: {Brand}\nQuantity: {Quantity}\nWattage: {Wattage}\nColor: {Color}\nPrice: {Price}\nFeature: {Feature}\nSoundRating: {SoundRating}";
        }
    }



//p.2


using System;
using System.Collections.Generic;
using System.IO;
using ProblemDomain;

public class ApplianceManager
{
    private List<Appliance> appliances = new List<Appliance>();

    public void LoadAppliances(string filePath)
    {
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split(';');
                    string itemNumber = parts[0];

                    int type = GetApplianceType(itemNumber);

                    Appliance appliance = type switch
                    {
                        1 => CreateRefrigerator(parts),
                        2 => CreateVacuum(parts),
                        3 => CreateMicrowave(parts),
                        4 or 5 => CreateDishwasher(parts),
                        _ => null
                    };

                    if (appliance != null)
                        appliances.Add(appliance);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading appliance file: " + ex.Message);
        }
    }

    private int GetApplianceType(string itemNumber)
    {
        if (!string.IsNullOrEmpty(itemNumber) && itemNumber.Length > 0)
            return int.Parse(itemNumber.Substring(0, 1));
        return -1;
    }

    private Refrigerator CreateRefrigerator(string[] parts)
    {
        return new Refrigerator(
            parts[0],
            parts[1],
            int.Parse(parts[2]),
            int.Parse(parts[3]),
            parts[4],
            double.Parse(parts[5]),
            int.Parse(parts[6]),
            int.Parse(parts[7]),
            int.Parse(parts[8])
        );
    }

    private Vacuum CreateVacuum(string[] parts)
    {
        return new Vacuum(
            parts[0],
            parts[1],
            int.Parse(parts[2]),
            int.Parse(parts[3]),
            parts[4],
            double.Parse(parts[5]),
            parts[6],
            int.Parse(parts[7])
        );
    }

    private Microwave CreateMicrowave(string[] parts)
    {
        return new Microwave(
            parts[0],
            parts[1],
            int.Parse(parts[2]),
            int.Parse(parts[3]),
            parts[4],
            double.Parse(parts[5]),
            double.Parse(parts[6]),
            parts[7]
        );
    }

    private Dishwasher CreateDishwasher(string[] parts)
    {
        return new Dishwasher(
            parts[0],
            parts[1],
            int.Parse(parts[2]),
            int.Parse(parts[3]),
            parts[4],
            double.Parse(parts[5]),
            parts[6],
            parts[7]
        );
    }

    public List<Appliance> GetAppliances()
    {
        return appliances;
    }

    public Appliance FindByItemNumber(string itemNumber)
    {
        return appliances.Find(a => a.ItemNumber == itemNumber);
    }

    public List<Appliance> FindByBrand(string brand)
    {
        return appliances.FindAll(a => a.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase));
    }

    public List<Appliance> GetRandomAppliances(int count)
    {
        var random = new Random();
        var result = new List<Appliance>();
        var copy = new List<Appliance>(appliances);

        for (int i = 0; i < count && copy.Count > 0; i++)
        {
            int index = random.Next(copy.Count);
            result.Add(copy[index]);
            copy.RemoveAt(index);
        }

        return result;
    }
}
