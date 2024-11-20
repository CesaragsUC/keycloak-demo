using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeyCloack.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{

    [Route("read")]
    [HttpGet]
    [Authorize(Roles = "Read")]
    public async Task<IActionResult> Read()
    {
        return Ok(Names());
    }

    [Route("create")]
    [HttpPost]
    [Authorize(Roles = "Create")]
    public async Task<IActionResult> Create()
    {
        return Ok("Produt created successfully");
    }

    [Route("updte")]
    [HttpPut]
    [Authorize(Roles = "Update")]
    public async Task<IActionResult> Update()
    {
        return Ok("Produt updated successfully");
    }

    [Route("delete")]
    [HttpDelete]
    [Authorize(Roles = "Delete")]
    public async Task<ActionResult> Delete()
    {
        return Ok("Produt removed Successfully");
    }

    [Route("admin")]
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Admin()
    {
        return Ok("Welcome to admin area");
    }


    private List<string> Names()
    {
        return new List<string>
        {
            "Iphone",
            "Samsung",
            "Xbox",
            "Playtaion 5"
        };
    }
}
