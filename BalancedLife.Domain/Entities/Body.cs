﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BalancedLife.Domain.Entities;

public partial class Body
{
    public long Id { get; set; }

    public double Weight { get; set; }

    public double Height { get; set; }

    public DateTime Datetime { get; set; }

    public long IdUser { get; set; }

    public virtual UserInfo IdUserNavigation { get; set; }
}