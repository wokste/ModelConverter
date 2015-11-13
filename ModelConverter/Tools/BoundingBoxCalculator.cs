using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelConverter.DataStructures;

namespace ModelConverter.Tools {
	class BoundingBoxCalculator {
		internal static string getBoundingBox(Model m) {
			if (m.vertices.Count == 0)
				return "";

			Vertex min = m.vertices[0];
			Vertex max = m.vertices[0];

			foreach (var v in m.vertices) {
				min = getMin(min, v);
				max = getMax(max, v);
			}

			return write("min", min) + write("max", max) + write("size", max - min);
		}

		static string write(string name, Vertex v) {
			return name + "[" + v.x + ", " + v.y + ", " + v.z + "]\n";
		}

		static Vertex getMin(Vertex min, Vertex cur) {
			min.x = min.x < cur.x ? min.x : cur.x;
			min.y = min.y < cur.y ? min.y : cur.y;
			min.z = min.z < cur.z ? min.z : cur.z;
			return min;
		}

		static Vertex getMax(Vertex max, Vertex cur) {
			max.x = max.x > cur.x ? max.x : cur.x;
			max.y = max.y > cur.y ? max.y : cur.y;
			max.z = max.z > cur.z ? max.z : cur.z;
			return max;
		}
	}
}
