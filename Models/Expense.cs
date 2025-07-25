﻿using System.ComponentModel.DataAnnotations;

namespace PersonalFinances.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; } = null!; // e bejm null per inicializim

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public double Amount { get; set; }

        [Required]
        public String Category { get; set; } = null!;

        public DateTime Date { get; set; } = DateTime.Now; //jep current date qe eshte ber blerja


    }
}
