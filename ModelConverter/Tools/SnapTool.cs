using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelConverter.DataStructures;

namespace ModelConverter.Tools {
	class SnapTool {
		internal static void snap(Model m, double snap) {
			for (int i = 0; i < m.vertices.Count; i++) {
				m.vertices[i] = snapVertex(m.vertices[i], snap);
			}
		}

		static Vertex snapVertex(Vertex v, double snap) {
			v.x = Math.Round(v.x / snap) * snap;
			v.y = Math.Round(v.y / snap) * snap;
			v.z = Math.Round(v.z / snap) * snap;
			return v;
		}
	}
}
