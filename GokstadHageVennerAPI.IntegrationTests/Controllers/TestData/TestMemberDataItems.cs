using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GokstadHageVennerAPI.UnitTests.Controllers.TestData;

public class TestMemberDataItems
{
    public static IEnumerable<object[]> GetTestMembers => new List<object[]>
    {
        new object[]
        {
            new TestMember
            {
                Base64EncodedUserNamePassword = "a3JpczpLMWlzdG9mZmVyIw==",
                Member = new Models.Entities.Member
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
                }
            }        
        },
        new object[]
        {
            new TestMember
            {
                Base64EncodedUserNamePassword = "WW5ndmU6aGVtbWVsaWc=",
                Member = new Models.Entities.Member
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
                }
            }

        }
    };
}
