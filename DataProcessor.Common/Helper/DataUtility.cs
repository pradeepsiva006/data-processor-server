using DataProcessor.Common.CustomExceptions;
using DataProcessor.Common.Models;
using System.Drawing;
using System.Text.RegularExpressions;

namespace DataProcessor.Common.Helper
{
    /// <summary>
    /// Utility class responsible for parsing data lines into DataItem objects.
    /// </summary>
    public class DataUtility : IDataUtility
    {
        /// <summary>
        /// Parses a single line of data into a DataItem object.
        /// </summary>
        /// <param name="line">The line of data to be parsed.</param>
        /// <returns>A DataItem object containing the parsed data or throws an exception if the line format is invalid.</returns>
        /// <exception cref="ParsingException">Thrown if the line format is invalid or an error occurs during parsing.</exception>

        public DataItem ParseLine(string line)
        {
            try
            {
                string[] parts = line.Split(Constants.Parse_SplitDelimeter);
                if (parts.Length != 3 || !line.StartsWith(Constants.Parse_StartDelimiter))
                {
                    throw new ParsingException("Invalid line format");
                }

                string name = parts[0].Substring(1); 
                if (string.IsNullOrWhiteSpace(name) || !Regex.IsMatch(name, Constants.AlphanumericRegex))
                {
                    throw new ParsingException("Invalid Name");
                }

                string color = parts[1].ToUpper(); 
                if (string.IsNullOrWhiteSpace(color) || !IsValidColor(color))
                {
                    throw new ParsingException("Invalid Color");
                }

                if (!int.TryParse(parts[2], out int value))
                {
                    throw new ParsingException("Invalid Value");
                }

                return new DataItem { Name = name, Color = color, Value = value };
            }
            catch (FormatException ex)
            {
                throw new ParsingException($"Invalid data format encountered in line => {line}", ex);
            }
            catch (RegexMatchTimeoutException ex)
            {
                throw new ParsingException($"Regular expression matching timed out for line {line}", ex);
            }
            catch (ArgumentException ex)
            {
                throw new ParsingException($"Invalid argument provided for parsing line {line}", ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new ParsingException($"Index out of bounds error occurred while parsing line {line}", ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Checks if the provided string represents a valid named color.
        /// </summary>
        /// <param name="colorName">The name of the color to validate.</param>
        /// <returns>True if the color name is valid, False otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the colorName parameter is null.</exception>
        public bool IsValidColor(string colorName)
        {
            try
            {
                Color color = Color.FromName(colorName);
                return color.IsKnownColor;
            }

            catch (ArgumentNullException)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves the hexadecimal color code for a valid named color.
        /// **Currently not used in the implemented functionality.**
        /// </summary>
        /// <param name="colorName">The name of the color for which to retrieve the code.</param>
        /// <returns>The hexadecimal color code in the format "#RRGGBB" if the color is valid, otherwise throws an exception.</returns>
        /// <exception cref="ParsingException">Thrown if the color name is invalid.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the colorName parameter is null.</exception>
        public string GetColorCode(string colorName)
        {
            // This method is currently not used in the implemented functionality.
            // It can be used in the future if clients require color codes.
            try
            {
                Color color = Color.FromName(colorName);
                if (color.IsKnownColor)
                {
                    return string.Format("{0:x6}", color.ToArgb());
                }
                else
                {
                    throw new ParsingException("Invalid color name");
                }
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }
    }
}
