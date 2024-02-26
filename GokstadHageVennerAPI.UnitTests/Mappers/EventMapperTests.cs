using GokstadHageVennerAPI.Mappers;
using GokstadHageVennerAPI.Models.DTOs;
using GokstadHageVennerAPI.Models.Entities;
using StudentBloggAPI.Mappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GokstadHageVennerAPI.UnitTests.Mappers;

public class EventMapperTests
{
    private readonly IMapper<Event, EventDTO> _eventMapper = new EventMapper();

    [Fact]
    public void MapToDTO_When_EventEntity_Given_ShouldReturn_EventDTO()
    {
        // Arrange

        Event evt = new()
        {
            Id = 1,
            MemberId = 2,
            EventType = "EventType",
            EventName = "EventName",
            Description = "Description",
            EventDate = new DateOnly(2024, 05, 05),
            EventTime = new TimeOnly(10,00,00),
            Created = new DateTime(2023, 12, 13, 11, 00, 00),
            Updated = new DateTime(2023, 12, 13, 13, 00, 00),        
        };

        // Act

        var eventDTO = _eventMapper.MapToDTO(evt);

        // Assert

        Assert.NotNull(eventDTO);   
        Assert.Equal(evt.Id, eventDTO.Id);
        Assert.Equal(evt.MemberId, eventDTO.MemberId);
        Assert.Equal(evt.EventType, eventDTO.EventType);
        Assert.Equal(evt.EventName, eventDTO.EventName);
        Assert.Equal(evt.Description, eventDTO.Description);
        Assert.Equal(evt.EventDate, eventDTO.EventDate);
        Assert.Equal(evt.EventTime, eventDTO.EventTime);
        Assert.Equal(evt.Created, eventDTO.Created);
        Assert.Equal(evt.Updated, eventDTO.Updated);
    }

    [Fact]
    public void MapToEntity_When_EventDTO_Given_Should_Return_EventEntity()
    {
        // Arrange         

        var date = new DateTime(2023, 12, 13, 11, 30, 0);

        EventDTO eventDTO = new() 
        {
            Id = 1,
            MemberId = 2,
            EventType = "EventType",
            EventName = "EventName",
            Description = "Description",
            EventDate = new DateOnly(2024, 05, 05),
            EventTime = new TimeOnly(10, 00, 00),
            Created = new DateTime(2023, 12, 13, 11, 30, 0),
            Updated = date
        };

        // Act

        var evt = _eventMapper.MapToEntity(eventDTO); 

        // Assert

        Assert.NotNull(evt);
        Assert.Equal(evt.Id, eventDTO.Id);
        Assert.Equal(evt.MemberId, eventDTO.MemberId);
        Assert.Equal(evt.EventType, eventDTO.EventType);
        Assert.Equal(evt.EventName, eventDTO.EventName);
        Assert.Equal(evt.Description, eventDTO.Description);
        Assert.Equal(evt.EventDate, eventDTO.EventDate);
        Assert.Equal(evt.EventTime, eventDTO.EventTime);

        Assert.Equal(date, eventDTO.Created); 
        Assert.Equal(date, eventDTO.Updated);
    }
}
