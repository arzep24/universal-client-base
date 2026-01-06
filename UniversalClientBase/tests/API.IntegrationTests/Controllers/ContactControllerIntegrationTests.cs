using Microsoft.AspNetCore.Mvc.Testing;
using API.IntegrationTests.Shared;
using UniversalClientBase.Application.Dtos;
using System.Collections.Generic;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace API.IntegrationTests.Controllers;

public class ContactControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ContactControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetContacts_ShouldReturnSuccessStatusCode()
    {
        // Act - Llamamos al endpoint que ya desarrollamos
        var response = await _client.GetAsync("/api/contact");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task SearchContacts_ByLastName_ShouldReturnFilteredResults()
    {
        // Act: Realizamos la petición al endpoint de búsqueda desarrollado
        var response = await _client.GetAsync("/api/contact?searchTerm=Perez");

        // Assert
        response.EnsureSuccessStatusCode();
        
        
        var results = await response.Content.ReadFromJsonAsync<List<ContactDto>>(); 
        results.Should().NotBeNull();
        results.Should().OnlyContain(c => c.LastName != null && c.LastName.Contains("Perez"));
    }
}