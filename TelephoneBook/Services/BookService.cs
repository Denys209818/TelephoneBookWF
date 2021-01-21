using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telephone.DAL.DBFiles;
using TelephoneBook.Models;
using static Bogus.DataSets.Name;

namespace TelephoneBook.Services
{
    /// <summary>
    ///     Сервіс, для роботи з БД
    /// </summary>
    public class BookService
    {
        /// <summary>
        ///     Клас, який заповнює БД
        /// </summary>
        /// <param name="context">Зв'язок з БД</param>
        /// <returns>Повертає масив усіх елементів (ті що додались та ті що були)</returns>
        public static AbonentDataCollection FillRandomAbonents(MyContext context) 
        {
            //  Ініціалізація Faker (Bogus)
            Faker f = new Faker();
            Faker<AbonentData> faker = new Faker<AbonentData>("uk");

            AbonentDataCollection coll = BookService.Search(context, new SearchModel { page = 1, countOnePage = 10 });
            for (int i = 0; i < 100; i++) 
            {
                ///  
                ///     Встановлення правил для коректної генерації Bogus елементів
                /// 
                Gender gender = f.PickRandom<Gender>();

                faker.RuleFor((AbonentData data) => data.FullName, (Faker faker) => 
                faker.Name.LastName(gender) + " " 
                + faker.Name.FirstName(gender) + " " 
                + faker.Name.FirstName(Gender.Male) + (gender == Gender.Male ? "ович" : "івна"));

                faker.RuleFor((AbonentData data) => data.Gender, (Faker gen) => (gender == Gender.Male ? false : true));

                faker.RuleFor((AbonentData data) => data.telNummer, (Faker faker) => faker.Phone.PhoneNumber());
                ///
                ///     Закінчення встановлення правил  
                ///  

                //  Генерація типу абонента телефонної книжки
                AbonentData data = faker.Generate();
                //  Заповнення БД
                context.persons.Add(new TelBookPerson
                {
                    FirstName = data.FullName.Split(" ")[1],
                    SecondName = data.FullName.Split(" ")[0],
                    LastName = data.FullName.Split(" ")[2],
                    Gender = gender == Gender.Male ? false : true,
                    telNummer = data.telNummer
                });
                //  Додавання елемента в масив елементів, щоб потім повернути
                coll.abonents.Add(data);
            }
            context.SaveChanges();
            return coll;
        }
        /// <summary>
        ///     Клас, який виконує пошук по БД 
        /// </summary>
        /// <param name="context">Зв'язок з БД</param>
        /// <param name="search">Тип по параметрам якого проводиться пошук</param>
        /// <returns>Повертає колекцію знайдених елементів</returns>
        public static AbonentDataCollection Search(MyContext context, SearchModel search = null) 
        {
            //  Повернення силки на БД, щоб створювати запроси через Linq до БД
            var query = context.persons.AsQueryable();

            /// 
            ///фільтр для пошуку данних
            ///
            if (search != null)
            {

                //  Пошук за П.І.Б
                if (!string.IsNullOrEmpty(search.Name))
                {
                    query = query.Where(x => (x.SecondName + " " + x.FirstName + " " + x.LastName).Contains(search.Name));
                }

                //  Пошук за телефонним номером
                if (!string.IsNullOrEmpty(search.telNum))
                {
                    query = query.Where(x => x.telNummer.Contains(search.telNum));
                }

                //  Пошук за статтю
                if (search.State.HasValue) 
                {
                query = search.State == true ?
                    query
                    .Where(x => x.Gender == true) :
                    query
                    .Where(x => x.Gender == false);
                }
            }

            //  Формування колекції елементів, яких необхідно вивести у DataGriedView 
            AbonentDataCollection collection = new AbonentDataCollection();
            collection.abonents = query
                .Select(x => new AbonentData
                {
                    FullName = x.SecondName + " " + x.FirstName + " " + x.LastName,
                    Gender = x.Gender,
                    Id = x.Id,
                    telNummer = x.telNummer
                })
                .OrderBy(x => x.Id)
                .Skip((search.page - 1) * search.countOnePage)
                .Take(search.countOnePage).ToList();
            //  Запис кількості елементів (для коррекного відображення під час пагінації)
            collection.Count = query.Count();

            return collection;
        }
    }
}
