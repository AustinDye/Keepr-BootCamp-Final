using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class AccountService
  {
    private readonly AccountsRepository _repo;
    public AccountService(AccountsRepository repo)
    {
      _repo = repo;
    }

    internal Profile GetProfileById(string id)
    {
      Profile found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("invalid id");
      }
      return found;
    }
    internal Account GetProfileByEmail(string email)
    {
      return _repo.GetByEmail(email);
    }
    internal Account GetOrCreateProfile(Account userInfo)
    {
      Account profile = _repo.GetById(userInfo.Id);
      if (profile == null)
      {
        return _repo.Create(userInfo);
      }
      return profile;
    }

    internal Account Edit(Account editData, string userEmail)
    {
      Account original = GetProfileByEmail(userEmail);
      original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
      original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
      return _repo.Edit(original);
    }
  }
}