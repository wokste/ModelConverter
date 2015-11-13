using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelConverter.DataStructures {
	internal class Model {
		public List<Vertex> vertices = new List<Vertex>();
		public List<Polygon> polygons = new List<Polygon>();

		internal Model clone() {
			Model clone = new Model();
			clone.vertices.AddRange(vertices);
			clone.polygons.AddRange(polygons);
			return clone;
		}

		public string getErrors() {
			var sb = new StringBuilder();
			for (int i = 0; i < polygons.Count; i++) {
				var p = polygons[i];

				if (p.v1 < 0 || p.v1 >= vertices.Count) {
					sb.AppendLine("In polygon " + i + ", vertex 1 (" + p.v1 + ") is out of range ");
				}

				if (p.v2 < 0 || p.v2 >= vertices.Count) {
					sb.AppendLine("In polygon " + i + ", vertex 2 (" + p.v2 + ") is out of range ");
				}

				if (p.v3 < 0 || p.v3 >= vertices.Count) {
					sb.AppendLine("In polygon " + i + ", vertex 3 (" + p.v3 + ") is out of range ");
				}
				
				if (p.v1 == p.v2 || p.v1 == p.v3 || p.v2 == p.v3) {
					sb.AppendLine("In polygon " + i + "(" + p.v1 + ", " + p.v2 + ", " + p.v3 + ") , references to the same vertex are made");
				}
			}
			return sb.ToString();
		}

		public string getDescription() {
			return
				"Model details:\n" +
				"vertices: " + vertices.Count + "\n" +
				"polygons: " + polygons.Count + "\n";
		}

		/// <summary>
		/// Subdivides a CONVEX polygon into a number of smaller triangles
		/// The subdividing is undefined if the polygon is not CONVEX or not in a plane.
		/// TODO: Change the requirement in a concave polygon
		/// </summary>
		internal void addPolygonByVertices(IList<int> ints) {
			for (int numPolygon = 0; numPolygon < ints.Count - 2; numPolygon++) {
				polygons.Add(new Polygon(ints[0], ints[numPolygon + 1], ints[numPolygon + 2]));
			}
		}
	}
}
