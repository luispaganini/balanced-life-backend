﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BalancedLife.Domain.Entities;

public partial class State
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Country { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}