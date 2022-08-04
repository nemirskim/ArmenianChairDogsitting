using ArmenianChairDogsitting.Business.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmenianChairDogsitting.Business.Interfaces;

public interface IAuthService
{
    public string GetToken(User user);
    public User GetUserForLogin(string email, string password);
}
