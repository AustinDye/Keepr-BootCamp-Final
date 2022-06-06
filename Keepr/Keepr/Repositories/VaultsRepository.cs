using System.Data;
using Keepr.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Keepr.Repositories
{
 public class VaultsRepository
  {
  private readonly IDbConnection _db;

  public VaultsRepository(IDbConnection db)
  {
   _db = db;
  }

  internal Vault Create(Vault newVault)
  {
   string sql = @"
    INSERT INTO vaults
    (name, description, isPrivate, creatorId)
    VALUES
     (@Name, @Description, @IsPrivate, @CreatorId);
    SELECT LAST_INSERT_ID();
    ";
   int id = _db.ExecuteScalar<int>(sql, newVault);
   newVault.Id = id;
   return newVault;
  } 

  internal List<Vault> Get()
  {
   string sql = @"
    SELECT
    v.*,
    a.*
    FROM vaults v
    JOIN accounts a ON v.creatorId = a.id
   ";
   return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
   {
    v.Creator = a;
    return v;
   },splitOn: "id").ToList();
  }

  internal Vault Get(int id)
  {
   string sql = @"
    SELECT
    v.*,
    a.*
    FROM vaults v
    JOIN accounts a ON v.creatorId = a.id
    WHERE v.id = @id
    ";
    return _db.Query<Vault, Account, Vault>(sql, (v, a) => 
    {
     v.Creator = a;
     return v;
    }, new { id }).FirstOrDefault();
  }

  internal Vault Edit(Vault original)
  {
   string sql = @"
   UPDATE vaults
   SET
    name = @Name,
    description = @Description,
    isPrivate = @IsPrivate
    WHERE id = @Id
   ";
   _db.Execute(sql, original);
  return original;
  }
  internal void Delete(int id)
  {
   string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
  }

 }
}