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
     (@Name, @Description, @IsPrivate, @CreatorId)
    SELECT LAST_INSERT_ID();
    ";
   _db.Execute(sql, newVault);
   return newVault;
  } 

  internal List<Vault> Get()
  {
   string sql = @"
    SELECT
    v.*
    act.*
    FROM vault v
    JOIN accounts act ON v.creatorId = act.id
   ";
   return _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
   {
    vault.Creator = account;
    return vault;
   }).ToList();
  }

  internal Vault Get(int id)
  {
   string sql = @"
    SELECT
    v.*
    act.*
    FROM vault v
    JOIN accounts act ON v.creatorId = act.id
    WHERE v.id = @id
    ";
    return _db.Query<Vault, Account, Vault>(sql, (vault, account) => 
    {
     vault.Creator = account;
     return vault;
    }, new { id }).FirstOrDefault();
  }

  internal Vault Edit(Vault original)
  {
   string sql = @"
   UPDATE vault
   SET
    name = @Name,
    description = @Description
    img = @Img
    WHERE id = @Id
   ";
   _db.Execute(sql, original);
  return original;
  }
  internal void Delete(int id)
  {
   string sql = "DELETE FROM vault WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
  }

 }
}