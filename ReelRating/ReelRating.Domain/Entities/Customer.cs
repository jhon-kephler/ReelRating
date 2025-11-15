using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SelectedMovie.Domain.Entities
{
    public class Customer
    {
        private int Id { get; set; }
        private string Nickname { get; set; }
        private string Name { get; set; }
        private string Email { get; set; }
        private string Password { get; set; }
        private DateTime CreatedAt { get; set; }
        private bool Status { get; set; }
    }
}
