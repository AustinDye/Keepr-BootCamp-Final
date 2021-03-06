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
  private readonly VaultsService _vaultsServ;
  private readonly KeepsService _keepsServ;

  public VaultKeepsController(VaultKeepsService vaultKeepsServ, KeepsService keepsServ, VaultsService vaultsServ)
  {
    _vaultKeepsServ = vaultKeepsServ;
   _keepsServ = keepsServ;
     _vaultsServ = vaultsServ;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<VaultKeep>> Post([FromBody] VaultKeep vaultKeepData)
  {
   try
   {
    Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
    Vault vault = _vaultsServ.GetById(vaultKeepData.VaultId);
    if(vault.CreatorId != userInfo.Id)
    {
     return Forbid();
    }
    vaultKeepData.CreatorId = userInfo.Id;
    VaultKeep newVaultKeep = _vaultKeepsServ.Create(vaultKeepData);
    return Created($"/api/vaultKeeps/{newVaultKeep.Id}", newVaultKeep);
   }

   catch (Exception e)
   {
    return BadRequest(e.Message);
   }

  }

  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<VaultKeep>> Delete(string id)
  {
   try
   {
    Account account = await HttpContext.GetUserInfoAsync<Account>();
    _vaultKeepsServ.Delete(id, account?.Id);
    return Ok(id);
   }
   catch (Exception e)
   {
    return BadRequest(e.Message);
   }
  }

  




 
}
}
