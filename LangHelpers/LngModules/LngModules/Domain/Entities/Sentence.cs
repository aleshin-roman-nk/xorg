﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LngModules.Domain.Entities
{
	public class Sentence
	{
		public Sentence()
		{
			Lexems = new List<Lexem>();
		}

		public int id { get; set; }
		public string text { get; set; }
		public string description { get; set; }
		public int ParagraphId { get; set; }
		public virtual ICollection<Lexem> Lexems { get; set; }
	}
}