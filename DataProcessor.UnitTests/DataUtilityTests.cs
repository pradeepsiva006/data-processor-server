
namespace DataProcessor.UnitTests
{
    public class DataUtilityTests
    {
        private DataUtility _dataUtility;
        [SetUp]
        public void Setup()
        {
            _dataUtility = new DataUtility();
        }

        [Test]
        public void ParseLine_ValidInput_ReturnsDataItem()
        {
            // Arrange
            var line = "#A:RED:5";

            // Act
            var result = _dataUtility.ParseLine(line);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo("A"));
            Assert.That(result.Color, Is.EqualTo("RED"));
            Assert.That(result.Value, Is.EqualTo(5));
        }

        [Test]
        public void ParseLine_InvalidInput_MissingHashSymbol_ThrowsFormatException()
        {
            // Arrange
            var line = "A:RED:5";

            // Act & Assert
            Assert.Throws<FormatException>(() => _dataUtility.ParseLine(line));
        }

        [Test]
        public void ParseLine_InvalidInput_null_ThrowsNullRefException()
        {
            // Arrange, Act & Assert
            Assert.Throws<NullReferenceException>(() => _dataUtility.ParseLine(null));
        }

        [Test]
        public void ParseLine_InvalidInput_IncorrectNumberOfParts_ThrowsFormatException()
        {
            // Arrange
            var line = "#A:RED";

            // Act & Assert
            Assert.Throws<FormatException>(() => _dataUtility.ParseLine(line));
        }

        [Test]
        public void ParseLine_InvalidInput_EmptyName_ThrowsFormatException()
        {
            // Arrange
            var line = "#:RED:5";

            // Act & Assert
            Assert.Throws<FormatException>(() => _dataUtility.ParseLine(line));
        }

        [Test]
        public void ParseLine_InvalidInput_InvalidName_ThrowsFormatException()
        {
            // Arrange
            var line = "##InvalidName:RED:5";

            // Act & Assert
            Assert.Throws<FormatException>(() => _dataUtility.ParseLine(line));
        }

        [Test]
        public void ParseLine_InvalidInput_EmptyColor_ThrowsFormatException()
        {
            // Arrange
            var line = "#A::5";

            // Act & Assert
            Assert.Throws<FormatException>(() => _dataUtility.ParseLine(line));
        }

        [Test]
        public void ParseLine_InvalidInput_InvalidColor_ThrowsFormatException()
        {
            // Arrange
            var line = "#A:InvalidColor:5";

            // Act & Assert
            Assert.Throws<FormatException>(() => _dataUtility.ParseLine(line));
        }

        [Test]
        public void ParseLine_InvalidInput_InvalidValue_ThrowsFormatException()
        {
            // Arrange
            var line = "#A:RED:invalid";

            // Act & Assert
            Assert.Throws<FormatException>(() => _dataUtility.ParseLine(line));
        }

        [Test]
        public void GetColorCode_ValidColorName_ReturnsColorCode()
        {
            // Arrange
            var colorName = "Red";
            var expectedColorCode = "ffff0000";

            // Act
            var result = _dataUtility.GetColorCode(colorName);

            // Assert
            Assert.That(result, Is.EqualTo(expectedColorCode));
        }

        [Test]
        public void GetColorCode_InvalidColorName_ThrowsArgumentException()
        {
            // Arrange
            var invalidColorName = "InvalidColor";

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _dataUtility.GetColorCode(invalidColorName));
        }

        [Test]
        public void GetColorCode_NullColorName_ThrowsArgumentNullException()
        {
            // Arrange, Act and Assert
            Assert.Throws<ArgumentNullException>(() => _dataUtility.GetColorCode(null));
        }

        [Test]
        public void GetColorCode_EmptyColorName_ThrowsArgumentException()
        {
            // Arrange
            var emptyColorName = string.Empty;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _dataUtility.GetColorCode(emptyColorName));
        }


        [Test]
        public void IsValidColor_ValidColorName_ReturnsColorCode()
        {
            // Arrange
            var colorName = "Red";

            // Act
            bool result = _dataUtility.IsValidColor(colorName);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void IsValidColor_InvalidColorName_ThrowsArgumentException()
        {
            // Arrange
            var invalidColorName = "InvalidColor";

            // Act
            bool result = _dataUtility.IsValidColor(invalidColorName);

            // Assert
            Assert.That(result, Is.EqualTo(false));

        }

        [Test]
        public void IsValidColor_NullColorName_ThrowsArgumentNullException()
        {
            // Arrange, Act and Assert
            Assert.Throws<ArgumentNullException>(() => _dataUtility.IsValidColor(null));
        }

        [Test]
        public void IsValidColor_EmptyColorName_ThrowsArgumentException()
        {
            // Arrange
            var emptyColorName = string.Empty;

            // Act
            bool result = _dataUtility.IsValidColor(emptyColorName);

            // Assert
            Assert.That(result, Is.EqualTo(false));

        }


    }
}