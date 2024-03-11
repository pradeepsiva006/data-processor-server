using DataProcessor.Common.Models;

namespace DataProcessor.Common.Helper
{
    /// <summary>
    /// Interface defining methods for parsing data lines and validating colors.
    /// </summary>
    public interface IDataUtility
    {
        /// <summary>
        /// Parses a single line of data into a DataItem object.
        /// </summary>
        /// <param name="line">The line of data to be parsed.</param>
        /// <returns>A DataItem object containing the parsed data or throws an exception if the line format is invalid.</returns>
        /// <exception cref="ParsingException">Thrown if the line format is invalid or an error occurs during parsing.</exception>
        DataItem ParseLine(string line);

        /// <summary>
        /// Checks if the provided string represents a valid named color.
        /// </summary>
        /// <param name="colorName">The name of the color to validate.</param>
        /// <returns>True if the color name is valid, False otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the colorName parameter is null.</exception>
        bool IsValidColor(string colorName);

        /// <summary>
        /// Retrieves the hexadecimal color code for a valid named color.
        /// </summary>
        /// <param name="colorName">The name of the color for which to retrieve the code.</param>
        /// <returns>The hexadecimal color code in the format "#RRGGBB" if the color is valid, otherwise throws an exception.</returns>
        /// <exception cref="ParsingException">Thrown if the color name is invalid.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the colorName parameter is null.</exception>
        string GetColorCode(string colorName);
    }
}
