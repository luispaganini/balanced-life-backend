﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BalancedLife.Domain.Entities;

public partial class UnitMeasurement
{
    public long Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<FoodNutritionInfo> FoodNutritionInfos { get; set; } = new List<FoodNutritionInfo>();

    public virtual ICollection<Snack> Snacks { get; set; } = new List<Snack>();
}