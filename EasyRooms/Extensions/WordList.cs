using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyRooms.Extensions
{
	public static class WordList
	{
		public static List<string> RemoveHomeVisitRows(this List<string> strings)
		{
			var indicesOfHomeVisitEntries = Enumerable.Range(0, strings.Count)
				.Where(i => string.Equals(strings[i], Constants.Constants.HomeVisit, StringComparison.OrdinalIgnoreCase))
				.ToList();

			indicesOfHomeVisitEntries
				.OrderByDescending(i => i)
				.ToList()
				.ForEach(index => strings.RemoveRange(index - 2, Constants.Constants.ElementsPerRowWithHouseVisitEntry));
			return strings;
		}
	}
}
