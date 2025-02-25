﻿using BalancedLife.Domain.Enums;

namespace BalancedLife.Application.DTOs.Snack {
    public class SnacksByDayDTO {
        public DateTime Date { get; set; }
        public string NameUser { get; set; }
        public double Carbohydrates { get; set; }
        public double Calories { get; set; }
        public double Fat { get; set; }
        public double Colesterol { get; set; }
        public double Protein { get; set; }
        public double Others { get; set; }
        public double TotalCalories { get; set; }
        public List<SnackListDTO> Snacks { get; set; }
    }

    public class SnackListDTO {
        public int Id { get; set; }
        public string Title { get; set; }
        public double TotalCalories { get; set; }
        public int IdMeal { get; set; }
        public StatusMeal StatusSnack { get; set; }
    }
}
