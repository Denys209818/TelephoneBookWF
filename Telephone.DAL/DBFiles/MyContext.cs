using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Telephone.DAL.DBFiles
{
    /// <summary>
    ///     Клас для зв'язку з БД методом CodeFirst
    /// </summary>
    public sealed class MyContext : DbContext
    {
        public DbSet<TelBookPerson> persons { get; set; }
        /// <summary>
        ///     Метод, який встановлює налаштування БД
        /// </summary>
        /// <param name="optionsBuilder">Параметр, який встановлює з'єднання з БД (PostgreSQL)</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=95.214.10.36;Port=5432;Database=denysdb;Username=denys;Password=qwerty1*;");
        }
    }
}
