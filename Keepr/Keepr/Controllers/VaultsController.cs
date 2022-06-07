using System;
using Microsoft.AspNetCore.Mvc;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;


namespace Keepr.Controllers
{
 [ApiController]
[Route("api/[controller]")]

public class VaultsController : ControllerBase
{

 private readonly VaultsService _vaultsServ;
  private readonly VaultKeepsService _vaultKeepsServ;

public VaultsController(VaultsService vaultsServ, VaultKeepsService vaultKeepServ)
  {
    _vaultsServ = vaultsServ;
   _vaultKeepsServ = vaultKeepServ;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Vault>> Post([FromBody] Vault vaultData)
  {
   try
   {
    Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
    vaultData.CreatorId = userInfo.Id;
    Vault newVault = _vaultsServ.Create(vaultData);
    newVault.Creator = userInfo;
    return Created($"/api/vaults/{newVault.Id}", newVault);
   }
   catch (Exception e)
   {
    return BadRequest(e.Message);
   }

  }

  [HttpGet]
  public ActionResult<List<Vault>> Get()
  {
   try
   {
    List<Vault> vault = _vaultsServ.Get();
    return Ok(vault);
   }
   catch (Exception e)
   {
    return BadRequest(e.Message);
   }
  }

  [HttpGet("{id}")]
 public async Task<ActionResult<Vault>> Get(int id)
  {
   try
   {
    Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
    Vault vault = _vaultsServ.GetById(id);
    return Ok(vault);
   }
   catch (Exception e)
   {
    return BadRequest(e.Message);
   }
  }



  [HttpPut("{id}")]
  [Authorize]
  public async Task<ActionResult<Vault>> Put(int id, [FromBody] Vault vault)
  {
   try
   {
    Account account = await HttpContext.GetUserInfoAsync<Account>();
    vault.CreatorId = account.Id;
    vault.Id = id;
    Vault updatedVault = _vaultsServ.Edit(vault);
    return Ok(updatedVault);
   }
   catch(Exception e)
   {
    return BadRequest(e.Message);
   }
  }
  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<Vault>> Delete(int id)
  {
   try
   {
    Account account = await HttpContext.GetUserInfoAsync<Account>();
    _vaultsServ.Delete(id, account.Id);
    return Ok(id);
   }
   catch (Exception e)
   {
    return BadRequest(e.Message);
   }
  }

  [HttpGet("{id}/keeps")]
  public ActionResult<List<Keep>> GetVaultKeeps(int vaultId)
  {
   try
   {
    List<Keep> keeps = _vaultKeepsServ.GetKeeps(vaultId);
    return keeps;
   } 
   catch (Exception e) 
   {
    return BadRequest(e.Message);
   }
  }
}
 
}
