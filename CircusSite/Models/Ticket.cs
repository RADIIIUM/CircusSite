using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CircusSite.Models
{
    public partial class Ticket
    {
        public int IdTicket { get; set; }

        [Required(ErrorMessage = "Не указано название программы")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Некорректный ввод программы (min: 6 | max: 100)")]
        public string ProgrammName { get; set; } = null!;

        [Required(ErrorMessage = "Не указана дата и время\nпроведения программы")]
        public DateTime DataProvedenia { get; set; }

        [Required(ErrorMessage = "Не указана цена программы")]
        [Range(0,999999, ErrorMessage = "Некорректный ввод цены(мин 0/превышена цена)")]
        public int? Price { get; set; }
    }
}
