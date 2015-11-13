using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelConverter.DataStructures {
	internal struct Vertex {
		public double x;
		public double y;
		public double z;

		public Vertex(double x, double y, double z) {
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static Vertex operator -(Vertex a, Vertex b) {
			return new Vertex(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		internal static Vertex cross(Vertex v1, Vertex v2) {
			return new Vertex(v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x);
		}

		internal Vertex normalize() {
			double length = Math.Sqrt(x * x + y * y + z * z);
			x /= length;
			y /= length;
			z /= length;
			return this;
		}
	}
}
