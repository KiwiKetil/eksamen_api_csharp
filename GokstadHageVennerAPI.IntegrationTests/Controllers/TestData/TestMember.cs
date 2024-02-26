using GokstadHageVennerAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GokstadHageVennerAPI.UnitTests.Controllers.TestData;

public class TestMember
{
    public Member? Member { get; set; }
    public string Base64EncodedUserNamePassword { get; init; } = string.Empty;
}
