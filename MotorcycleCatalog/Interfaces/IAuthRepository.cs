﻿namespace MotorcycleCatalog.Interfaces
{
    public interface IAuthRepository
    {
        string? Authenticate(string adminId, string secret);
    }
}
