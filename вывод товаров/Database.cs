using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Controls;

namespace тест
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfGames { get; set; }
        public int Rating { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public override string ToString()
        {
            return Name; 
        }
    }

    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Genre { get; set; }
        public int AgeRestrictions { get; set; }
        public decimal Price { get; set; }
        public int NumberOfCopiesSold { get; set; }
        public byte[] Photo { get; set; } 
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Achievement> Achievements { get; set; }
    }


    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int HoursPlayed { get; set; }
        public bool IsBlocked { get; set; }
        public UserType UserType { get; set; } // Новое свойство для типа пользователя
        public virtual ICollection<Score> Scores { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        // Другие свойства заказа

        // Внешний ключ для связи с пользователем
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }

    public enum UserType
    {
        Regular,
        Admin
    }


    public class Achievement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
    }

    public class Score
    {
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual User User { get; set; }
    }

    public class Currency
    {
        public int Id { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}