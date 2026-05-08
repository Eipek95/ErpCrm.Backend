using ErpCrm.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpCrm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = "Admin")]
public class FakeDataController : ControllerBase
{
    private readonly IFakeDataSeeder _fakeDataSeeder;

    public FakeDataController(IFakeDataSeeder fakeDataSeeder)
    {
        _fakeDataSeeder = fakeDataSeeder;
    }

    [HttpPost("seed")]
    public async Task<IActionResult> Seed(CancellationToken cancellationToken)
    {
        await _fakeDataSeeder.SeedAsync(cancellationToken);
        return Ok(new
        {
            success = true,
            message = "Fake data seeded successfully."
        });
    }
}
