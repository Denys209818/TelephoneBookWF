﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TelephoneBook.Models
{
    /// <summary>
    ///     Клас, від вмісту якого залежить результат пошуку (Фільтрації) данних телефонного довідника
    /// </summary>
    public class SearchModel
    {
        public string Name { get; set; }
        public string telNum { get; set; }
        public bool? State { get; set; }
        public int page { get; set; }
        public int countOnePage { get; set; } = 10;
    }
}
