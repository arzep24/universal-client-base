using UniversalClientBase.Core.Entities;
using Xunit;
using FluentAssertions;

namespace Core.Unittest.Entities;

public class ContactTests
{
    [Fact]
    public void NewContact_ShouldHaveDefaultStatusAsProspecto()
    {
        // Arrange & Act
        var contact = new Contact { 
            FirstName = "Test", 
            Email = "test@ucb.com" 
        };

        // Assert
        contact.Status.Should().Be("Prospecto");
        contact.CreatedAt.Should().BeBefore(DateTime.UtcNow.AddSeconds(1));
        
    }
}