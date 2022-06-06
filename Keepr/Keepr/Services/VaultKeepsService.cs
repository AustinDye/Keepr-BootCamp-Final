using Keepr.Models;
using Keepr.Repositories;

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


 }
}