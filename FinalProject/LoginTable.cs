using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace FinalProject
{
    //Tạo bảng users
    [Table("users")]
    class LoginTable
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int id { get; set; }

        [MaxLength(30)]
        public string name { get; set; }

        [MaxLength(30), Unique]
        public string email { get; set; }

        [MaxLength(25)]
        public string password { get; set; }

        [MaxLength(25), NotNull]
        public string role { get; set; }
    }
}