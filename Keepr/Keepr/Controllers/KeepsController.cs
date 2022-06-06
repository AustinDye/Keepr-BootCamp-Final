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

public class KeepsController : ControllerBase
{

 private readonly KeepsService _keepsServ;

public KeepsController(KeepsService keepsServ)
  {
    _keepsServ = keepsServ;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Keep>> Post([FromBody] Keep keepData)
  {
   try
   {
    Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
    keepData.CreatorId = userInfo.Id;
    Keep newKeep = _keepsServ.Create(keepData);
    newKeep.Creator = userInfo;
    return Created($"api/keeps/{newKeep.Id}", newKeep);
   }
   catch (Exception e)
   {
    return BadRequest(e.Message);
   }

  }

  [HttpGet]
  public ActionResult<List<Keep>> Get()
  {
   try
   {
    List<Keep> keeps = _keepsServ.Get();
    return Ok(keeps);
   }
   catch (Exception e)
   {
    return BadRequest(e.Message);
   }
  }

   [HttpGet("{id}")]
  public async Task<ActionResult<Keep>> Get(int id)
  {
   try
   {
    Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
    Keep keep = _keepsServ.GetById(id);
    return Ok(keep);
   }
   catch (Exception e)
   {
    return BadRequest(e.Message);
   }
  } 

  [HttpPut("{id}")]
  [Authorize]
  public async Task<ActionResult<Keep>> Put(int id, [FromBody] Keep keep)
  {
   try
   {
    Account account = await HttpContext.GetUserInfoAsync<Account>();
    keep.CreatorId = account.Id;
    keep.Id = id;
    Keep updatedKeep = _keepsServ.Edit(keep);
    return Ok(updatedKeep);
   }
   catch(Exception e)
   {
    return BadRequest(e.Message);
   }
  }
  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<Keep>> Delete(int id)
  {
   try
   {
    Account account = await HttpContext.GetUserInfoAsync<Account>();
    _keepsServ.Delete(id, account.Id);
    return Ok(id);
   }
   catch (Exception e)
   {
    return BadRequest(e.Message);
   }
  }
}
 
}


