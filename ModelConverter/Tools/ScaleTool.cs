using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelConverter.DataStructures;

namespace ModelConverter.Tools {
	class ScaleTool {
		static void scale(Model m, double scale) {
			for (int i = 0; i < m.vertices.Count; i++) {
				m.vertices[i] = scaleVertex(m.vertices[i], scale);
			}
		}

		static Vertex scaleVertex(Vertex v, double scale) {
			v.x *= scale;
			v.y *= scale;
			v.z *= scale;
			return v;
		}
	}
}
