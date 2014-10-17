using System;
using System.IO;
using System.Text;
using System.Xml;

using Commons.Xml.Relaxng;
using Commons.Xml.Relaxng.Rnc;

namespace ValidateRelaxNG {
class Program {
	public static void Main(string[] args) {
		try {
			string xmlfile = args[0];
			string patternfile = args[1];

			XmlTextReader xmlReader = new XmlTextReader(xmlfile);
			RelaxngValidatingReader validator;
			
			using( TextReader source = File.OpenText(patternfile)) {
				RncParser parser = new RncParser(new NameTable());
				RelaxngPattern pattern = parser.Parse(source);
				validator = new RelaxngValidatingReader(xmlReader,pattern);
			}
			
			validator.InvalidNodeFound += delegate(XmlReader source, string message) {  
				Console.WriteLine("Error: " + message);
				return true;
			};
		
			while (validator.Read()) {
				// do nothing yet, just check for errors
			}
		} catch (Exception e) {
			Console.WriteLine(e);
		}
	}
}
}