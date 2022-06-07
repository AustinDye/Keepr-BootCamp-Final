using System;
using Microsoft.AspNetCore.Mvc;
using Keepr.Models;
using Keepr.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using System.Linq;

namespace Keepr.Controllers
{
 [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly AccountService _acctServ;
    private readonly VaultsService _vaultsServ;


    private readonly KeepsService _keepsServ;
    public ProfilesController(AccountService acctServ, VaultsService vaultsServ, KeepsService keepsServ)
    {
      _acctServ = acctServ;
      _vaultsServ = vaultsServ;
      _keepsServ = keepsServ;
  }

    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
      try
      {
        Profile profile = _acctServ.GetProfileById(id);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/vaults")]

    public async Task<ActionResult<List<Vault>>> GetVaults(string id)
    {
      try
      {
       
        List<Vault> vaults = _vaultsServ.GetProfileVaults(id);
        Account account = await HttpContext.GetUserInfoAsync<Account>();

        List<Vault> privateVaults = vaults.Where( (v) =>
        {
          if(account != null)
          {
            if (v.CreatorId == account.Id)
            {
              return true;
            }
            return !v.IsPrivate;
          }
         return !v.IsPrivate;

        }).ToList<Vault>();

    return Ok(privateVaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetKeeps(string id)
    {
      try
      {
        List<Keep> keeps = _keepsServ.GetProfileKeeps(id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}