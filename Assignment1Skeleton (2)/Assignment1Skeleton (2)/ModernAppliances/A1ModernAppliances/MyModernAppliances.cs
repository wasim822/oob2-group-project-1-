using ModernAppliances.Entities;
using ModernAppliances.Entities.Abstract;
using ModernAppliances.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModernAppliances
{
    /// <summary>
    /// Manager class for Modern Appliances
    /// </summary>
    internal class MyModernAppliances : ModernAppliances
    {
        /// <summary>
        /// Option 1: Performs a checkout
        /// </summary>
        public override void Checkout()
        {
            Console.Write("Enter the item number of an appliance: ");
            string input = Console.ReadLine();
            if (!long.TryParse(input, out long itemNumber))
            {
                Console.WriteLine("Invalid item number.");
                return;
            }

            Appliance foundAppliance = null;


        ///part2
using System;
using System.Collections.Generic;
using System.IO;
using ProblemDomain;

public class ApplianceManager
{
    /// This list holds all loaded appliances from the file
    private List<Appliance> appliances = new List<Appliance>();

    // Reads the appliance file and populates the appliances list
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

                    // Determine appliance type and create corresponding object
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

    /// Extracts first digit of item number to determine appliance type
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

    /// Returns full list of appliances (used by other parts of the program)
    public List<Appliance> GetAppliances()
    {
        return appliances;
    }

    /// Finds appliance by exact item number
    public Appliance FindByItemNumber(string itemNumber)
    {
        return appliances.Find(a => a.ItemNumber == itemNumber);
    }

    /// Finds appliances matching a specific brand (case-insensitive)
    public List<Appliance> FindByBrand(string brand)
    {
        return appliances.FindAll(a => a.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase));
    }

    /// Returns a list of random appliances
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


            foreach (var appliance in Appliances)
            {
                if (appliance.ItemNumber == itemNumber)
                {
                    foundAppliance = appliance;
                    break;
                }
            }

            if (foundAppliance == null)
            {
                Console.WriteLine("No appliances found with that item number.");
            }
            else
            {
                if (foundAppliance.IsAvailable)
                {
                    foundAppliance.Checkout();
                    Console.WriteLine("Appliance has been checked out.");
                }
                else
                {
                    Console.WriteLine("The appliance is not available to be checked out.");
                }
            }
        }

        /// <summary>
        /// Option 2: Finds appliances
        /// </summary>
        public override void Find()
        {
            Console.Write("Enter brand to search for: ");
            string brand = Console.ReadLine()?.Trim();

            var found = new List<Appliance>();

            foreach (var appliance in Appliances)
            {
                if (appliance.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase))
                {
                    found.Add(appliance);
                }
            }

            DisplayAppliancesFromList(found, 0);
        }
