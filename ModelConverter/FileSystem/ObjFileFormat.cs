using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using ModelConverter.DataStructures;

namespace ModelConverter.FileSystem {
	class ObjFileFormat {
		internal static Model load(StreamReader sr) {
			//NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
			//nfi.NumberDecimalSeparator = ".";

			Model m = new Model();

			string line;

			while ((line = sr.ReadLine()) != null) {
				Console.WriteLine(line);

				var words = line.Split(' ');
				switch (words[0]) {
					case "v":
						// There are convertions with coordinate systems.
						// format in obj: v <Y> <Z> <X>
						m.vertices.Add(new Vertex(Double.Parse(words[3], FileIOHelper.nfi), Double.Parse(words[1], FileIOHelper.nfi), Double.Parse(words[2], FileIOHelper.nfi)));
						break;
					case "f":
						// Note, the -1 is the conversion between indexing between the obj files and the internal structures
						// obj files start conting with index 1
						// csharp arrays start counting with index 0
						m.polygons.Add(new Polygon(Int32.Parse(words[1], FileIOHelper.nfi) - 1, Int32.Parse(words[2], FileIOHelper.nfi) - 1, Int32.Parse(words[3], FileIOHelper.nfi) - 1));
						break;
				}
			}

			return m;
		}

		internal static void save(Model m, StreamWriter w) {

			w.WriteLine("# OBJ Exported from Titleds Model Converter");
			w.WriteLine("usemtl (null)");
			w.WriteLine("s 0");

			foreach (var v in m.vertices){
				// There are convertions with coordinate systems.
				// format in obj: v <Y> <Z> <X>
				w.WriteLine("v " + v.y + " " + v.z + " " + v.x);
			}

			foreach (var p in m.polygons) {
				// Note, the +1 is the conversion between indexing between the obj files and the internal structures
				// obj files start conting with index 1
				// csharp arrays start counting with index 0
				w.WriteLine("f " + (p.v1 + 1) + " " + (p.v2 + 1) + " " + (p.v3 + 1));
			}
		}
	}
}
