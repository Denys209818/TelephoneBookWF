using System;
using System.Collections.Generic;
using System.Text;

namespace TelephoneBook.Models
{
    /// <summary>
    ///     Клас для  роботи із елеентом comboBox, а саме вибір число елементів, що потрібно відобразити
    /// </summary>
    public class ComboBoxView
    {
        public int Id { get; set; }
        public string ViewStr { get; set; }

        public override string ToString()
        {
            return this.ViewStr;
        }
    }
}
