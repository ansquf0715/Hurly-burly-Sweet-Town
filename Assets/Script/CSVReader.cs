using System.IO;
using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

public class CSVReader
{
	static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
	static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
	static char[] TRIM_CHARS = { '\"' };

	public static List<Dictionary<string, object>> Read(string file)
	{
		var list = new List<Dictionary<string, object>>();
		TextAsset data = Resources.Load(file) as TextAsset;

		var lines = Regex.Split(data.text, LINE_SPLIT_RE);

		if (lines.Length <= 1) return list;

		var header = Regex.Split(lines[0], SPLIT_RE);
		for (var i = 1; i < lines.Length; i++)
		{

			var values = Regex.Split(lines[i], SPLIT_RE);
			if (values.Length == 0 || values[0] == "") continue;

			var entry = new Dictionary<string, object>();
			for (var j = 0; j < header.Length && j < values.Length; j++)
			{
				string value = values[j];
				value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
				object finalvalue = value;
				int n;
				float f;
				if (int.TryParse(value, out n))
				{
					finalvalue = n;
				}
				else if (float.TryParse(value, out f))
				{
					finalvalue = f;
				}
				entry[header[j]] = finalvalue;
			}
			list.Add(entry);
		}
		return list;
	}
	public static void Write(List<Dictionary<string, object>> data, string file)
	{
		using (var writer = new StreamWriter(file))
		{
			var header = string.Join(",", data[0].Keys.ToArray());
			writer.WriteLine(header);

			foreach (var row in data)
			{
				var values = row.Values.Select(o => Escape(o.ToString())).ToArray();
				writer.WriteLine(string.Join(",", values));
			}

			writer.Close();
			writer.Dispose();
		}
	}

	private static string Escape(string value)
	{
		if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
		{
			value = "\"" + value.Replace("\"", "\"\"") + "\"";
		}
		return value;
	}
}
