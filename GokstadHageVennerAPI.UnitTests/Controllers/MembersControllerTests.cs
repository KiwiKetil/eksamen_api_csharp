using GokstadHageVennerAPI.Controllers;
using GokstadHageVennerAPI.Models.DTOs;
using GokstadHageVennerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GokstadHageVennerAPI.UnitTests.Controllers;

public class MembersControllerTests
{
    private readonly MembersController _membersController;
    private readonly Mock<IMemberService> _memberServiceMock = new();
    private readonly Mock<Serilog.ILogger> _loggerMock = new();


    public MembersControllerTests()
    {
        _membersController = new MembersController(_memberServiceMock.Object,
            Mock.Of<ILogger<MembersController>>());
    }

    [Fact]
    public async Task GetMemberById_WhenIdIsGiven_ShouldReturn_MemberDTOWithId() 
    {
        // Arrange

        int id = 1;

        var memberDTO = new MemberDTO(
            id,
            "UserName",
            "FirstName",
            "LastName",
            "Email@Email.com",
            new DateTime(2024, 12, 13, 13, 00, 00),
            new DateTime(2024, 12, 13, 13, 00, 00)
            );

        _memberServiceMock.Setup( x => x.GetByIdAsync(id)).ReturnsAsync(memberDTO);
       
        // Act

        var res = await _membersController.GetMemberById(id);

        // Assert

        var actionresult = Assert.IsType<ActionResult<MemberDTO>>(res);
        var returnValue = Assert.IsType<OkObjectResult>(actionresult.Result);
        var dto = Assert.IsType<MemberDTO>(returnValue.Value);

        Assert.Equal(dto.Id, memberDTO.Id);
        Assert.Equal(dto.UserName, memberDTO.UserName);
        Assert.Equal(dto.FirstName, memberDTO.FirstName);
        Assert.Equal(dto.LastName, memberDTO.LastName);
        Assert.Equal(dto.Email, memberDTO.Email);
        Assert.Equal(dto.Created, memberDTO.Created);
        Assert.Equal(dto.Updated, memberDTO.Updated);            
    }

    [Fact]
    public async Task GetMemberById_WhenIdIsGivenAndNotFound_ShouldReturn_NotFound()
    {
        // Arrange

        int id = 1000;

        _memberServiceMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(() => null);

        // Act

        var res = await _membersController.GetMemberById(id);

        // Assert

        var actionresult = Assert.IsType<ActionResult<MemberDTO>>(res);
        var returnValue = Assert.IsType<NotFoundObjectResult>(actionresult.Result);
        var errorMessage = Assert.IsType<string>(returnValue.Value);
        Assert.Equal("Could not find any member with this id", errorMessage);
    }
}
