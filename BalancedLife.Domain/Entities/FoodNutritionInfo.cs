﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BalancedLife.Domain.Entities;

public partial class FoodNutritionInfo
{
    public long Id { get; set; }

    public long IdUnitMeasurement { get; set; }

    public double? Quantity { get; set; }

    public long? IdNutritionalComposition { get; set; }

    public long? IdFood { get; set; }

    public virtual Food IdFoodNavigation { get; set; }

    public virtual NutritionalComposition IdNutritionalCompositionNavigation { get; set; }

    public virtual UnitMeasurement IdUnitMeasurementNavigation { get; set; }
}