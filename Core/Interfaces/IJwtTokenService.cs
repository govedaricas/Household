﻿using Infrastructure.Entities;

namespace Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(User user);
    }
}
