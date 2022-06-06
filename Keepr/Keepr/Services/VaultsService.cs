using Keepr.Models;
using Keepr.Repositories;
using System.Collections.Generic;
using System;

namespace Keepr.Services
{
 public class VaultsService
 {
  private readonly VaultsRepository _repo;

  public VaultsService(VaultsRepository repo)
  {
   _repo = repo;
  }

  internal List<Vault> Get()
  {
    List<Vault> vaults = _repo.Get();
   return vaults;
  }

  internal Vault GetById(int id)
  {
   Vault found = _repo.Get(id);
   if(found == null)
   {
    throw new Exception("Could not find that vault");
   }
   return found;
  }

   internal Vault Create(Vault vaultData)
    {
      Vault newVault = _repo.Create(vaultData);
      return newVault;
    }

    internal Vault Edit(Vault vaultData)
    {
      Vault original = GetById(vaultData.Id);
      if (original.CreatorId != vaultData.CreatorId)
      {
        throw new Exception("You cant do that");
      }
      original.Name = vaultData.Name ?? original.Name;
      original.Description = vaultData.Description ?? original.Description;
      original.IsPrivate = vaultData.IsPrivate;


   _repo.Edit(original);

   return GetById(original.Id);
  }

  internal void Delete(int id, string userId)
  {
   Vault vault = GetById(id);
   if(vault.CreatorId != userId)
   {
    throw new Exception("You can not delete vaults you did not make");
   }
   _repo.Delete(vault.Id);
  }
 }
}