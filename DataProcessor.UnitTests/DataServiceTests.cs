using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using DataProcessor.Common.Models;
using DataProcessor.Core.DataStore;
using DataProcessor.Core.Implementations;
using System.Text;

namespace DataProcessor.UnitTests
{
    public class DataServiceTests
    {
        private Mock<IDataUtility> _dataUtilityMock;
        private Mock<IDataStore> _dataStoreMock;
        private Mock<ILogger<DataService>> _loggerMock;
        private DataService _service;

        [SetUp]
        public void SetUp()
        {
            _dataUtilityMock = new Mock<IDataUtility>();
            _dataStoreMock = new Mock<IDataStore>();
            _loggerMock = new Mock<ILogger<DataService>>();
            _service = new DataService(_dataStoreMock.Object, _dataUtilityMock.Object, _loggerMock.Object);
        }
        [Test]
        public async Task ProcessFile_ValidInputFile_ReturnsParsedDataAsync()
        {
            // Arrange
            var fileContent = "#A:RED:5\n#B:BLUE:10";
            var inputFileMock = new Mock<IFormFile>();
            inputFileMock.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes(fileContent)));

            var expectedData = new List<DataItem>
            {
                new DataItem { Name = "A", Color = "RED", Value = 5 },
                new DataItem { Name = "B", Color = "BLUE", Value = 10 }
            };

            _dataUtilityMock.SetupSequence(x => x.ParseLine(It.IsAny<string>()))
            .Returns(new DataItem { Name = "A", Color = "RED", Value = 5 })
            .Returns(new DataItem { Name = "B", Color = "BLUE", Value = 10 });

            _dataStoreMock.Setup(x => x.UpdateData(It.IsAny<List<DataItem>>()));

            // Act
            var result = await _service.ParseFileAsync(inputFileMock.Object);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(expectedData.Count));
            AssertDataListsAreEqual(expectedData, result);
            _dataUtilityMock.Verify(x => x.ParseLine(It.IsAny<string>()), Times.Exactly(2));
            _dataStoreMock.Verify(x => x.UpdateData(It.IsAny<List<DataItem>>()), Times.Once);
        }

        private void AssertDataListsAreEqual(List<DataItem> expected, List<DataItem> actual)
        {
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.That(actual[i].Name, Is.EqualTo(expected[i].Name));
                Assert.That(actual[i].Color, Is.EqualTo(expected[i].Color));
                Assert.That(actual[i].Value, Is.EqualTo(expected[i].Value));
            }
        }

        [Test]
        public void ProcessFile_InvalidInputFile_ThrowsFormatException()
        {
            // Arrange
            var invalidFileContent = "InvalidLine\n#B:BLUE:10";
            var invalidInputFileMock = new Mock<IFormFile>();
            invalidInputFileMock.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes(invalidFileContent)));

            _dataUtilityMock.Setup(d => d.ParseLine(It.IsAny<string>()))
                            .Throws<FormatException>();

            // Act and Assert
            Assert.ThrowsAsync<FormatException>(async () => await _service.ParseFileAsync(invalidInputFileMock.Object));
        }

        [Test]
        public void ProcessFile_EmptyInputFile_ReturnsEmptyList()
        {
            // Arrange
            var emptyFileContent = string.Empty;
            var emptyInputFileMock = new Mock<IFormFile>();
            emptyInputFileMock.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes(emptyFileContent)));

            // Act and Assert
            Assert.ThrowsAsync<FormatException>(async () => await _service.ParseFileAsync(emptyInputFileMock.Object));
        }

        [Test]
        public void GetUpdatedData_WhenDataStoreIsEmpty_ReturnsEmptyList()
        {
            //Arrange
            
            _dataStoreMock.Setup(x => x.GetData()).Returns(new List<DataItem>());

            // Act
            var result = _service.GetUpdatedData();

            // Assert
            Assert.IsEmpty(result);
            _dataStoreMock.Verify(x => x.GetData(), Times.Exactly(2));
            _dataStoreMock.Verify(x => x.UpdateData(It.IsAny<List<DataItem>>()), Times.Never);
        }

        [Test]
        public void GetUpdatedData_WhenDataStoreHasData_ReturnsRandomizedData()
        {
            // Arrange
            var inMemoryData = new List<DataItem>
            {
                new DataItem { Name = "A", Color = "RED", Value = 5 },
                new DataItem { Name = "B", Color = "BLUE", Value = 10 }
            };
            _dataStoreMock.Setup(x => x.GetData()).Returns(inMemoryData);
            _dataStoreMock.Setup(x => x.UpdateData(It.IsAny<List<DataItem>>()));

            // Act
            var result = _service.GetUpdatedData();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result[0].Value, Is.GreaterThanOrEqualTo(5).And.LessThanOrEqualTo(20));
            Assert.That(result[1].Value, Is.GreaterThanOrEqualTo(5).And.LessThanOrEqualTo(20));
            _dataStoreMock.Verify(x => x.GetData(), Times.Exactly(2));
            _dataStoreMock.Verify(x => x.UpdateData(It.IsAny<List<DataItem>>()), Times.Once);
        }


    }
}
