﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BalancedLife.Domain.Entities;

public partial class Status
{
    public long Id { get; set; }

    public string Description { get; set; }

    public virtual ICollection<StatusUser> StatusUsers { get; set; } = new List<StatusUser>();
}