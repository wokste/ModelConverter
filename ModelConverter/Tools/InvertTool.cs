using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelConverter.DataStructures;

namespace ModelConverter.Tools {
	class InvertTool {
		internal static void Apply(Model m) {
			// Swap the v2 and v3 arguments in all polygons
			// This inverts the face of the polygons
			for (int i = 0; i < m.polygons.Count; i++) {
				m.polygons[i] = new Polygon(m.polygons[i].v1, m.polygons[i].v3, m.polygons[i].v2);
			}
		}
	}
}
