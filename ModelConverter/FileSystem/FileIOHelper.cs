using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using ModelConverter.DataStructures;

namespace ModelConverter.FileSystem {
	class FileIOHelper {
		internal static string FileFormats = "supported files|*.obj;*.t3d|obj files (*.obj)|*.obj|Unreal t3d files (*.t3d)|*.t3d|All files (*.*)|*.*";

		internal static NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

		public static Model loadModel(Stream fileStream, string format) {
			using (var reader = new StreamReader(fileStream)){
				switch (format) {
					case "obj":
						return ObjFileFormat.load(reader);
					case "t3d":
						return T3DFileFormat.load(reader);
				}
			}
			return new Model();
		}

		public static void saveModel(Model model, Stream fileStream, string format) {
			using (var reader = new StreamWriter(fileStream)) {
				switch (format) {
					case "obj":
						ObjFileFormat.save(model, reader);
						break;
					case "t3d":
						T3DFileFormat.save(model, reader);
						break;
				}
			}
		}
	}
}
