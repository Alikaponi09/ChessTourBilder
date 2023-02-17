﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Data.ChessClasses
{
    ///<summary>
    ///Фигура Ферзь
    ///</summary>
    internal class Queen : Figure
    {
        public override string Name { get; } = "Q";
        public Queen(string poziton, bool IsWhile) : base(poziton, IsWhile) { }

        public override void Move() { }
    }
}