using System;
using System.Collections.Generic;
using System.Text;

namespace TelephoneBook.Models
{
    /// <summary>
    ///     Клас для роботи з елемонтом Абонент з БД
    /// </summary>
    public class AbonentData
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public string telNummer { get; set; }
    }
}
