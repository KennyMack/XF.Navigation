﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Models
{
    public enum MenuItemType
    {
        Second,
        Third,
        Browse,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
