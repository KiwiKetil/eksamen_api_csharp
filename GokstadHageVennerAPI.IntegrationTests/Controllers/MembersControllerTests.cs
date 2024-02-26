using GokstadHageVennerAPI.Models.DTOs;
using GokstadHageVennerAPI.Models.Entities;
using GokstadHageVennerAPI.UnitTests.Controllers.TestData;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GokstadHageVennerAPI.IntegrationTests.Controllers;

public class MembersControllerTests : IDisposable
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public MembersControllerTests()
    {
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient();
    }

    public void Dispose()
    {
        _factory.Dispose();
        _client.Dispose();
    }

    [Fact]
    public async Task GetAllMembers_DefaultPageSize_ReturnTwoMembers()
    {
        // Arrange

        List<Member> members = new();
        Member memberA = new()
        {
            Id = 3,
            UserName = "kris",
            FirstName = "Kristoffer",
            LastName = "Sveberg",
            HashedPassword = "$2a$11$u1Q2UhBxVyGODkeXHuZQ0.1cbckoS2zdOF6.Q0Z8KAZtduQH3InVO",
            Salt = "$2a$11$u1Q2UhBxVyGODkeXHuZQ0.",
            Email = "kristoffer@gmail.com",
            Created = new DateTime(2024, 12, 14, 11, 00, 00),
            Updated = new DateTime(2024, 12, 14, 11, 05, 00),
            IsAdminUser = false
        };
        
        Member memberB = new()
        {
            Id = 1,
            UserName = "Yngve",
            FirstName = "Yngve",
            LastName = "Magnussen",
            HashedPassword = "$2a$11$B0X0zfKssgRHdM3E0Kdgwus3HwtpShhhHhxQoT5vG6cZkA2MCpaMW",
            Salt = "$2a$11$B0X0zfKssgRHdM3E0Kdgwu",
            Email = "yngve.magnussen@gokstadakademiet.no",
            Created = new DateTime(2024, 12, 14, 11, 00, 00),
            Updated = new DateTime(2024, 12, 14, 11, 05, 00),
            IsAdminUser = true
        };        

        members.Add(memberA);
        members.Add(memberB);

        _factory.MemberRepositoryMock.Setup(m => m.GetAllAsync(1, 10)).ReturnsAsync(members);
        _factory.MemberRepositoryMock.Setup(m => m.GetByNameAsync(memberA.UserName)).ReturnsAsync(memberA);

        // login Kristoffer:

        string base64EncodedAuthenticationString = "a3JpczpLMWlzdG9mZmVyIw==";
        _client.DefaultRequestHeaders.Add("Authorization", $"Basic {base64EncodedAuthenticationString}");

        // Act

        var response = await _client.GetAsync("/api/v1/Members");

        var data = JsonConvert.DeserializeObject<IEnumerable<MemberDTO>>(await response.Content.ReadAsStringAsync());

        // Assert

        Assert.NotNull(data);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Collection(data, response_member =>
        {
            Assert.Equal(memberA.Id, response_member.Id);
            Assert.Equal(memberA.UserName, response_member.UserName);
            Assert.Equal(memberA.FirstName, response_member.FirstName);
            Assert.Equal(memberA.LastName, response_member.LastName);       
            Assert.Equal(memberA.Email, response_member.Email);
            Assert.Equal(memberA.Created, response_member.Created);
            Assert.Equal(memberA.Updated, response_member.Updated);
        },
                
        m =>
        {
            Assert.Equal(memberB.Id, m.Id);
            Assert.Equal(memberB.UserName, m.UserName);
            Assert.Equal(memberB.FirstName, m.FirstName);
            Assert.Equal(memberB.LastName, m.LastName);         
            Assert.Equal(memberB.Email, m.Email);
            Assert.Equal(memberB.Created, m.Created);
            Assert.Equal(memberB.Updated, m.Updated);           
        });
    }

    [Theory]
    [MemberData(nameof(TestMemberDataItems.GetTestMembers), MemberType = typeof(TestMemberDataItems))]
    public async Task GetAllMembers_DefaultPageSize_ReturnOneMember( TestMember testMember) 
    {
        // Arrange

        Member member = testMember.Member!;

        _factory.MemberRepositoryMock.Setup(m => m.GetAllAsync(1, 10)).ReturnsAsync(new List<Member> { member! });
        _factory.MemberRepositoryMock.Setup(m => m.GetByNameAsync(member.UserName)).ReturnsAsync(member);

        _client.DefaultRequestHeaders.Add("Authorization", $"Basic {testMember.Base64EncodedUserNamePassword}");

        // Act

        var response = await _client.GetAsync("/api/v1/Members");

        var data = JsonConvert.DeserializeObject<IEnumerable<MemberDTO>>(await response.Content.ReadAsStringAsync());

        // Assert

        Assert.NotNull(data);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Collection(data, response_member =>
        {
            Assert.Equal(member.Id, response_member.Id);
            Assert.Equal(member.UserName, response_member.UserName);
            Assert.Equal(member.FirstName, response_member.FirstName);
            Assert.Equal(member.LastName, response_member.LastName);
            Assert.Equal(member.Email, response_member.Email);
            Assert.Equal(member.Created, response_member.Created);
            Assert.Equal(member.Updated, response_member.Updated);
        });
    }
}
