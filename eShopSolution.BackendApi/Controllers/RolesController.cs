using eShopSolution.Application.System.Roles;
using eShopSolution.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/Roles")]
    [ApiController]

    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleManager;
        public RolesController(IRoleService roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleManager.GetAll();
            return Ok(roles);
        }
    }
}
