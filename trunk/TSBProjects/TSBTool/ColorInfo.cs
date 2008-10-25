using System;
using System.Collections;
using System.Drawing;

namespace TSBTool
{
	/*
		regarding uniform editing: will you code in the option to edit the alternate jersey matchups?

		the addresses are the 4 bytes immediately trailing each teams' uniform/action seq data.
		as far as how it works, from jstout ->
		x00 x00 x00 x00 = When to Use Jersey 1 and 2 this is done bitwise 
		(Buffalo to Atlanta then AFC to NFC with final 2 bits always being 0s) 
		where a 0 = Jersey 1 and 1 = Jersey 2.
		(x00000000 would use Jersey 1 vs every team and xFFFFFFFC would use Jersey 2 vs every team)

		Now what I don't understand is how the extra 4 teams are going to work out with alt jerseys 
		- haven't looked close though.
	 */
	/// <summary>
	/// this class is used as arguments to the color form.
	/// </summary>
	public class ColorInfo
	{
		private Color[] mColors;

		private string[] mColorStrings;

		private int mNumberOfColors=1;

		private string mLabelText;
 
		public ColorInfo()
		{
		}

		public ColorInfo(int numberOfColors, string labelText)
		{
			mNumberOfColors = numberOfColors;
			mLabelText = labelText;
			mColors = new Color[mNumberOfColors];
			mColorStrings = new string[mNumberOfColors];
		}
	}
}
