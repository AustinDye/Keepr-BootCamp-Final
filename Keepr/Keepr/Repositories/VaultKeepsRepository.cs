using System.Data;
using Keepr.Models;
using Dapper;
namespace Keepr.Repositories
{
 public class VaultKeepsRepository
 {
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
   _db = db;
  }

  internal VaultKeep Create(VaultKeep newVaultKeep)
  {
     string sql = @"
    INSERT INTO vaultKeeps
    (keepId, vaultId, creatorId)
    VALUES
     (@KeepId, @VaultId, @CreatorId);
      SELECT LAST_INSERT_ID();
    ";
   int id =_db.ExecuteScalar<int>(sql, newVaultKeep);
   newVaultKeep.Id = id;
   return newVaultKeep;
  }
 }
}
