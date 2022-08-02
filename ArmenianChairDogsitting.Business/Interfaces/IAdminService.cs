﻿
using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Business.Interfaces;

public interface IAdminService
{
    public int AddAdmin(Admin admin);
    public void RemoveOrRestoreById(int id, bool isDelete);
}
