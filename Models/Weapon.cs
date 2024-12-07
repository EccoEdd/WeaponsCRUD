using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinApp.Models
{
    public class Weapon
    {
		public int Id { get; set; }
		public string full_name { get; set; }
		public string date { get; set; }
		public string description { get; set; }
		public bool active { get; set; }
		public DateTime created_at { get; set; }
		public DateTime updated_at { get; set; }
	}
}
