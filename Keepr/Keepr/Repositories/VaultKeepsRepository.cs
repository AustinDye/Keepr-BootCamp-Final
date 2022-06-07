using System.Data;
using Keepr.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;


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

    internal List<Keep> GetKeepsByVault(int vaultId)
  {
   string sql = @"
    SELECT
      k.*,
      vk.id AS vaultKeepId
      FROM vaultKeeps vk
      JOIN keeps k ON vk.keepId = k.id
      WHERE vk.vaultId = @vaultId
    ";
   return _db.Query<Keep>(sql, new { vaultId }).ToList();
  }

  internal VaultKeep GetVaultKeepById(string id)
        {
            string sql = "SELECT * FROM vaultKeeps WHERE id = @id";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }

   internal void Delete(int id)
  {
   string sql = "DELETE FROM vaultKeeps WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
  }


 
 }

