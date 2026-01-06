using System.Net;
using System.Net.Http.Json;
using API.IntegrationTests.Shared;
using FluentAssertions;
using UniversalClientBase.Application.Dtos;
using Xunit;

namespace API.IntegrationTests.Controllers;

public class ContactControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ContactControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    // 1. RF-001: INTEGRIDAD DE PERSISTENCIA (POST + GET)
    [Fact]
    public async Task CreateAndRetrieveContact_ShouldMaintainIntegrity()
    {
        // Arrange
        var newContact = new CreateContactDto("Integridad", "p@test.com","Prueba",  "123", "Prospecto");

        // Act - Crear
        var postResponse = await _client.PostAsJsonAsync("/api/contact", newContact);
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var created = await postResponse.Content.ReadFromJsonAsync<ContactDto>();
        
        // Fix: Dereference of a possibly null reference
        created.Should().NotBeNull();
        var contactId = created!.Id;

        // Act - Recuperar
        var getResponse = await _client.GetAsync($"/api/contact/{contactId}");

        // Assert
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var retrieved = await getResponse.Content.ReadFromJsonAsync<ContactDto>();
        retrieved!.Email.Should().Be(newContact.Email);
    }

    // 2. VALIDACIÓN DE FORMATOS (FLUENTVALIDATION)
    [Fact]
    public async Task CreateContact_ShouldReturnBadRequest_WhenEmailIsInvalid()
    {
        // Arrange - Email mal formado
        var invalidContact = new CreateContactDto("Juan", "email-invalido@@test","Perez",  "123", "Prospecto");

        // Act
        var response = await _client.PostAsJsonAsync("/api/contact", invalidContact);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errorContent = await response.Content.ReadAsStringAsync();
        errorContent.Should().Contain("formato del correo electrónico no es válido");
    }

    // 3. RF-002: MANEJO DE NULOS EN BÚSQUEDA
    [Fact]
    public async Task SearchContacts_ShouldHandleNoResults_Correctly()
    {
        // Act - Buscar algo que no existe
        var response = await _client.GetAsync("/api/contact?searchTerm=Inexistente123");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var results = await response.Content.ReadFromJsonAsync<List<ContactDto>>();
        results.Should().NotBeNull();
        results!.Should().BeEmpty();
    }

    // 4. MANEJO DE ERRORES: NOT FOUND (404)
    [Fact]
    public async Task GetContact_ShouldReturnNotFound_WhenIdDoesNotExist()
    {
        // Arrange - Un ID que no existe (SQLite usa enteros o GUIDs según tu config actual)
        var fakeId = 99999; 

        // Act
        var response = await _client.GetAsync($"/api/contact/{fakeId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}