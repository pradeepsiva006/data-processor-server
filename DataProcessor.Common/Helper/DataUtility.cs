using DataProcessor.Common.Models;
using System.Drawing;
using System.Text.RegularExpressions;

namespace DataProcessor.Common.Helper
{
    public class DataUtility : IDataUtility
    {

        public DataItem ParseLine(string line)
        {
            try
            {
                string[] parts = line.Split(':');
                if (parts.Length != 3 || !line.StartsWith("#"))
                {
                    throw new FormatException("Invalid line format");
                }

                string name = parts[0].Substring(1); 
                if (string.IsNullOrWhiteSpace(name) || !Regex.IsMatch(name, @"^[a-zA-Z0-9]+$"))
                {
                    throw new FormatException("Invalid name");
                }

                string color = parts[1].ToUpper(); 
                if (string.IsNullOrWhiteSpace(color) || !IsValidColor(color))
                {
                    throw new FormatException("Invalid color");
                }

                if (!int.TryParse(parts[2], out int value))
                {
                    throw new FormatException("Invalid value");
                }

                return new DataItem { Name = name, Color = color, Value = value };
            }

            catch (Exception)
            {
                throw;
            }
        }

        public bool IsValidColor(string colorName)
        {
            Color color = Color.FromName(colorName);
            return color.IsKnownColor;
        }

        public string GetColorCode(string colorName)
        {
            try
            {
                Color color = Color.FromName(colorName);
                if (color.IsKnownColor)
                {
                    return string.Format("{0:x6}", color.ToArgb());
                }
                else
                {
                    throw new ArgumentException("Invalid color name");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
