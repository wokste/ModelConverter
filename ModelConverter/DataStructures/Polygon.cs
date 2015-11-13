using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelConverter.DataStructures {
	internal struct Polygon {
		public int v1;
		public int v2;
		public int v3;

		public Polygon(int v1, int v2, int v3) {
			this.v1 = v1;
			this.v2 = v2;
			this.v3 = v3;
		}

		public Vertex getNormal(Model m) {
			var v21 = m.vertices[v2] - m.vertices[v1];
			var v31 = m.vertices[v3] - m.vertices[v1];
			return Vertex.cross(v21, v31).normalize();
		}
	}
}
