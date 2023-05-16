﻿using System.Text.Json.Serialization;

namespace FinanceManager.Model;

public class Operation
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
