﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using xorg.Tools;
using System.IO;

namespace Domain.Entities
{
	[Table("Sessions")]
	public class OSession
	{
		public int Id { get; set; }
		public int NodeId { get; set; }

		/// <summary>
		/// The session owner
		/// </summary>
		[Column("Node")]
		public Node Owner { get; set; }
		//public virtual Node Owner { get; set; }
		public string Description { get; set; }
		public string Report { get; set; }
		public DateTime Start { get; set; }
		public int ProvidedSeconds { get; set; }
		public string DNA { get; set; }
		public string OwnerDefinition
		{
			get
			{
				if (Owner == null) return "not loaded";
				return Owner.text;
			}
		}

		public string Name
		{
			get
			{
				if (Owner == null) return "not loaded";
				return $"{Owner.path}";

    //            if (string.IsNullOrEmpty(Owner.name))
    //            {
				//	using (var reader = new StringReader(Owner.))
				//	{
				//		return reader.ReadLine();
				//	}
				//}

				//return Owner.name;
			}
		}

		public string ShortName
		{
			get
			{
				if (Owner == null) return "not loaded";
				return $"{Owner.name}";
			}
		}

		//private List<NodeTextPage> _deser(string txt)
		//{
		//	// here strange thing happends.
		//	// sometimes txt comes as "null", sometimes as not null but empty string.
		//	if (string.IsNullOrEmpty(txt))
		//	{
		//		return new List<NodeTextPage> { new NodeTextPage { name = "", text = "" } };
		//	}

		//	List<NodeTextPage> res = null;

		//	try
		//	{
		//		res = JsonTool.Deserialize<List<NodeTextPage>>(txt);
		//	}
		//	catch (Exception)
		//	{
		//		res = new List<NodeTextPage>();
		//		res.Add(new NodeTextPage { text = txt });
		//	}

		//	return res;
		//}

		public string DirName
		{
			get
			{
				return Owner == null ? "---" : Owner.name;
			}
		}

		public DateTime Finish
		{
			get
			{
				return Start + TimeSpan.FromSeconds(ProvidedSeconds);
			}
		}
		// Фактический счетчик
		public int TotalSeconds { get; set; }
		public bool Closed { get; set; }

		public string WorkedMinutes
		{
			get
			{
				var res = TimeSpan.FromSeconds(TotalSeconds);
				return ($"{(int)res.TotalHours:d2}:{res.Minutes:d2}");
				//return ($"{res.TotalHours:n0}:{res.Minutes}");
			}
		}

		//===========================================================

		List<NoteRec> _log = new List<NoteRec>();
		public IEnumerable<NoteRec> Items => _log;

		/// <summary>
		/// Исходник, хранится в бд
		/// </summary>
		public string notes_source
		{
			get
			{
				return ser();
			}
			set
			{
				_log = deser(value);
			}
		}

		public void AddNote(DateTime dt, string text)
		{
			_log.Add(new NoteRec(dt, text));
		}

		public string Html
		{
			get
			{
				StringBuilder str = new StringBuilder();// FFFE0B    old b8daff

				string alert_primary = ".alert-primary { color: #FAC189; background-color: #3C6382;}";

				string alert = ".alert " +
					"{font-family: Roboto Condensed, sans-serif;" +
					"position: relative; " +
					"padding: 0.75mm 1.25mm; " +
					"margin-bottom: 4mm; " +
					"border: 2px solid transparent;" +
					"border-color: #003a51;" +
					"-webkit-border-radius: 15px;}";

				string html = "html { background-color: #3C6382;}";

				string styles = $"<style>{html}{alert}{alert_primary}</style>";

				str.Append(styles);

				foreach (var item in _log.AsEnumerable().Reverse())
				{
					var ttt = item.Text.Replace("\n", "<br>");

					str.Append("<div class=\"alert alert-primary\">");
					str.Append($"<b>{item.Title}</b>");
					str.Append($"<p>{ttt}</p></div>");
				}

				return str.ToString();
			}
		}

		private string ser()
		{
			return Newtonsoft.Json.JsonConvert.SerializeObject(_log);
		}

		private List<NoteRec> deser(string str)
		{
			if (string.IsNullOrEmpty(str)) return new List<NoteRec>();

			return Newtonsoft.Json.JsonConvert.DeserializeObject<List<NoteRec>>(str);
		}

		//===========================================================

		public OSession Clone()
		{
			return JsonTool.Clone(this);
		}

		public void CopyFrom(OSession o)
		{
			o.CopyPropertiesTo(this);
		}
	}
}
