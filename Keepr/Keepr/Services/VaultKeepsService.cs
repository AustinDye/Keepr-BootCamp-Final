using Keepr.Models;
using System.Collections.Generic;
using System;

namespace Keepr.Services
{
 public class VaultKeepsService
 {
  private readonly VaultKeepsRepository _repo;

  public VaultKeepsService(VaultKeepsRepository repo)
  {
   _repo = repo;
  }

   internal VaultKeep Create(VaultKeep vaultKeep)
    {
     VaultKeep newVaultKeep = _repo.Create(vaultKeep);
     return newVaultKeep;
    }

    internal List<Keep> GetKeeps(int vaultId)
    {
     List<Keep> keeps = _repo.GetKeepsByVault(vaultId);
     return keeps;
    }

     internal void Delete(string id, string userId)
  {
   VaultKeep vaultKeep = _repo.GetVaultKeepById(id);
   if(vaultKeep.CreatorId != userId)
   {
    throw new Exception("You can not delete vaults you did not make");
   }
   _repo.Delete(vaultKeep.Id);
  }


 }
}