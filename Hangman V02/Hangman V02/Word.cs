﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Hangman_V02
{
    class Word
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public string hangmanword { get; set; }
    }
}