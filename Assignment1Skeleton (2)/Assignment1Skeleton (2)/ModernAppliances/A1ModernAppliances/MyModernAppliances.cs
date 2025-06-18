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