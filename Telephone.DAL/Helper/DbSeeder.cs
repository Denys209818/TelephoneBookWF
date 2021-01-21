using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telephone.DAL.DBFiles;

namespace Telephone.DAL.Helper
{
    /// <summary>
    ///     Статичний клас, для початкової ініціалізації елементів
    /// </summary>
    public static class DbSeeder
    {
        /// <summary>
        ///     Метод, який кладе у БД 2 елемента у таблицю, якщо її вміст дорівнює 0
        /// </summary>
        /// <param name="context">Зв'язок з БД</param>
        public static void SeedTelBook(MyContext context) 
        {
            if (context.persons.Count() == 0) 
            {
                context.persons.AddRange(new TelBookPerson[] 
                {
                new TelBookPerson { 
                    FirstName = "Василь", 
                    SecondName = "Петров", 
                    LastName = "Петрович", 
                    telNummer = "0680122735", 
                    Gender = false },
                new TelBookPerson { 
                    FirstName = "Марина", 
                    SecondName = "Сидорова",
                    LastName = "Висилівна", 
                    telNummer = "0984151625",
                    Gender = true }
                });

                context.SaveChanges();
            }

        }
    }
}
