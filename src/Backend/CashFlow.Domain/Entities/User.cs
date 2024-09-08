﻿using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Entities;
public class User
{
    public int Id { get; set; }
    public Guid UserIdentifier { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.User;
}
