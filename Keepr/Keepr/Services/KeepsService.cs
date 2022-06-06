using Keepr.Models;
using Keepr.Repositories;
using System.Collections.Generic;
using System;

namespace Keepr.Services
{
 public class KeepsService
 {
  private readonly KeepsRepository _repo;

  public KeepsService(KeepsRepository repo)
  {
   _repo = repo;
  }

  internal List<Keep> Get()
  {
   List<Keep> keeps =_repo.Get();
   return keeps;
  }

  internal Keep GetById(int id)
  {
   Keep found = _repo.Get(id);
   if(found == null)
   {
    throw new Exception("Could not find that keep");
   }
   return found;
  }

   internal Keep Create(Keep keep)
    {
     Keep newKeep = _repo.Create(keep);
   return newKeep;
  }

    internal Keep Edit(Keep keepData)
    {
      Keep original = GetById(keepData.Id);
      if (original.CreatorId != keepData.CreatorId)
      {
        throw new Exception("You cant do that!");
      }
      original.Name = keepData.Name ?? original.Name;
      original.Description = keepData.Description ?? original.Description;
      original.Img = keepData.Img ?? original.Img;
      original.Views = keepData.Views;
      original.Kept = keepData.Kept;

   _repo.Edit(original);

   return GetById(original.Id);
  }

  internal void Delete(int id, string userId)
  {
   Keep keep = GetById(id);
   if(keep.CreatorId != userId)
   {
    throw new Exception("You can not delete keeps you did not make");
   }
   _repo.Delete(keep.Id);
   
  }
 }
}