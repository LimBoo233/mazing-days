using System;

namespace Core.Architecture
{
	[AttributeUsage(AttributeTargets.Field)]
	public class AutoBindAttribute : Attribute
	{
		public string Name;
		public AutoBindAttribute(string name = null) { this.Name = name; }
	}
}