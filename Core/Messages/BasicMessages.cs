using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messages
{
    public static class BasicMessages
    {
        public static void InputMessage(string name) => Console.WriteLine($"Input {name}");
        public static void WhatToChangeTeacher() => Console.WriteLine("name/surname/both");
        public static void SuccessMessage(string name, string type) => Console.WriteLine($"{name} succesfully {type}");
        public static void InoutBeginDateMessage(string name) => Console.WriteLine($"Input {name} (format: dd/MM/yyyy)");
        public static void InoutEndDateMessage(string name, int period) => Console.WriteLine($"Input {name} difference between begin and end date should be {period} month (format: dd/MM/yyyy)");
        public static void WantToChangeMessage(string name) => Console.WriteLine($"Do you want to change {name}");
    }
}
