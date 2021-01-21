﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TelephoneBook.Models
{
    /// <summary>
    ///     Колекція для зручного використання масиву абонентів, та щоб завжди знати його розмір
    /// </summary>
    public class AbonentDataCollection : IEnumerable
    {
        public List<AbonentData> abonents { get; set; }
        public int Count { get; set; }

        public IEnumerator GetEnumerator()
        {
            return this.abonents.GetEnumerator();
        }
    }
}
