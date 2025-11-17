using Business.DTOs;
using Business.Services;
using FluentAssertions;
using Moq;

namespace KotivWeatherApp.Tests.Services
{
    public class SearchHistoryServiceTests
    {
        [Fact]
        public async Task GetRecentSearchesAsync_ShouldReturnRecentSearches()
        {
            // Arrange
            var mockRepo = new Mock<ISearchHistoryRepository>();
            mockRepo.Setup(r => r.GetRecentSearchesAsync(It.IsAny<int>()))
                    .ReturnsAsync(new List<SearchHistoryDto>
                    {
                        new() { Id = 1, SearchTerm = "Denver", Latitude = 39.74M, Longitude = -104.99M }
                    });

            var service = new SearchHistoryService(mockRepo.Object);

            // Act
            var results = await service.GetRecentSearchesAsync();

            // Assert
            results.Should().HaveCount(1);
            results.First().SearchTerm.Should().Be("Denver");
        }
    }
}
