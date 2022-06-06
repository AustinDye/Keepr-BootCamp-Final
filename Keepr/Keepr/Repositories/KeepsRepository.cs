using System.Data;
using Keepr.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Keepr.Repositories
{
 public class KeepsRepository
  {
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
   _db = db;
  }

  internal Keep Create(Keep newKeep)
  {
   string sql = @"
    INSERT INTO keeps
    (name, description, img, creatorId)
    VALUES
     (@Name, @Description, @Img, @CreatorId);
      SELECT LAST_INSERT_ID();
    ";
   int id =_db.ExecuteScalar<int>(sql, newKeep);
   newKeep.Id = id;
   return newKeep;
  } 

  internal List<Keep> Get()
  {
   string sql = @"
  SELECT 
            k.*,
            a.*
        FROM keeps k
        JOIN accounts a ON k.creatorId = a.id";
   return _db.Query<Keep, Account,Keep>(sql, (k, a) =>
   {
    k.Creator = a;
    return k;
   },splitOn: "id").ToList();
  }

  internal Keep Get(int id)
  {
   string sql = @"
    SELECT
    k.*,
    a.*
    FROM keeps k
    JOIN accounts a ON k.creatorId = a.id
    WHERE k.id = @id
    ";
    return _db.Query<Keep, Account, Keep>(sql, (k, a) => 
    {
     k.Creator = a;
     return k;
    }, new { id }).FirstOrDefault();
  }

  internal List<Keep> GetKeepsByVault(int id)
  {
   string sql = @"
    SELECT
      k.*,
      a.*,
      vk.*
      FROM vaultKeeps vk
      JOIN accounts a ON k.creatorId = a.id
      JOIN keeps k ON vk.keepId = k.id
      WHERE vk.vaultId = @id
    ";
   return _db.Query<Keep, Account,Keep>(sql, (k, a) =>
   {
    k.Creator = a;
    return k;
   },new {id},splitOn: "id").ToList<Keep>();
  }

  internal Keep Edit(Keep original)
  {
   string sql = @"
   UPDATE keeps
   SET
    name = @Name,
    description = @Description,
    img = @Img
    WHERE id = @Id
   ";
   _db.Execute(sql, original);
  return original;
  }
  internal void Delete(int id)
  {
   string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
  }

 }
}