using System;
using Microsoft.AspNetCore.Mvc;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;


namespace Keepr.Controllers
{
 [ApiController]
[Route("api/[controller]")]

public class VaultKeepsController : ControllerBase
{

 private readonly VaultKeepsService _vaultKeepsServ;

public VaultKeepsController(VaultKeepsService vaultKeepsServ)
  {
    _vaultKeepsServ = vaultKeepsServ;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<VaultKeep>> Post([FromBody] VaultKeep vaultKeepData)
  {
   try
   {
    Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
    vaultKeepData.CreatorId = userInfo.Id;
    VaultKeep newVaultKeep = _vaultKeepsServ.Create(vaultKeepData);
    return Created($"/api/vaultkeeps/{newVaultKeep.Id}", newVaultKeep);
   }
   catch (Exception e)
   {
    return BadRequest(e.Message);
   }

  }

 
}
}
