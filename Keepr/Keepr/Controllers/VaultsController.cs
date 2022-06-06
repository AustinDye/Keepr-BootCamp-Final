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

 private readonly VaultsService _vaultServ;

public VaultsController(VaultsService vaultServ)
  {
    _vaultServ = vaultServ;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Vault>> Post([FromBody] Vault vaultData)
  {
   try
   {
    Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
    vaultData.CreatorId = userInfo.Id;
    Vault newVault = _vaultServ.Create(vaultData);
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
    List<Vault> vault = _vaultServ.Get();
    return Ok(vault);
   }
   catch (Exception e)
   {
    return BadRequest(e.Message);
   }
  }

 /* [HttpGet("{id}")]
 public async Task<ActionResult<Vault>> Get(int id)
  {
   try
   {
    Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
    Vault vault = _vaultServ.Get(id);
    return Ok(vault);
   }
   catch (Exception e)
   {
    return BadRequest(e.Message);
   }
  }*/

  [HttpPut("{id}")]
  [Authorize]
  public async Task<ActionResult<Vault>> Put(int id, [FromBody] Vault vault)
  {
   try
   {
    Account account = await HttpContext.GetUserInfoAsync<Account>();
    vault.CreatorId = account.Id;
    vault.Id = id;
    Vault updatedVault = _vaultServ.Edit(vault);
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
    _vaultServ.Delete(id, account.Id);
    return Ok();
   }
   catch (Exception e)
   {
    return BadRequest(e.Message);
   }
  }
}
 
}
