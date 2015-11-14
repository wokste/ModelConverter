using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ModelConverter.DataStructures;

namespace ModelConverter.FileSystem {
	class T3DFileFormat {
		internal static void save(Model m, StreamWriter w) {
			w.WriteLine("Begin Map");
			w.WriteLine("Begin Actor Class=LevelInfo Name=LevelInfo2");
			w.WriteLine("    TimeSeconds=5.440500");
			w.WriteLine("    AIProfile(0)=148");
			w.WriteLine("    Level=LevelInfo'MyLevel.LevelInfo2'");
			w.WriteLine("    Tag=LevelInfo");
			w.WriteLine("    Region=(Zone=LevelInfo'MyLevel.LevelInfo2',iLeaf=-1)");
			w.WriteLine("    Name=LevelInfo2");
			w.WriteLine("End Actor");
			w.WriteLine("Begin Actor Class=Brush Name=Brush2");
			w.WriteLine("    MainScale=(SheerAxis=SHEER_ZX)");
			w.WriteLine("    PostScale=(SheerAxis=SHEER_ZX)");
			w.WriteLine("    Group=Cube");
			w.WriteLine("    Level=LevelInfo'MyLevel.LevelInfo2'");
			w.WriteLine("    Tag=Brush");
			w.WriteLine("    Region=(Zone=LevelInfo'MyLevel.LevelInfo2',iLeaf=-1)");
			w.WriteLine("    Begin Brush Name=Brush");

			savePolyList(m, w);

			w.WriteLine("    End Brush");
			w.WriteLine("    Brush=Model'MyLevel.Model4'");
			w.WriteLine("    Name=Brush3");
			w.WriteLine("End Actor");
			w.WriteLine("End Map");
		}

		private static void savePolyList(Model m, StreamWriter w) {
			w.WriteLine("Begin PolyList");

			foreach (var p in m.polygons) {
				savePolygon(m,p,w);
			}

			w.WriteLine("End PolyList");
		}

		static void savePolygon(Model m, Polygon p, StreamWriter w) {
			
			w.WriteLine("   Begin Polygon");
			writeVertex("Origin  ", m.vertices[p.v1], w);
			writeVertex("Normal  ", p.getNormal(m), w);
			writeVertex("TextureU", new Vertex(1, 0, 0), w);
			writeVertex("TextureV", new Vertex(0, 1, 0), w);
			writeVertex("Vertex  ", m.vertices[p.v1], w);
			writeVertex("Vertex  ", m.vertices[p.v2], w);
			writeVertex("Vertex  ", m.vertices[p.v3], w);
			w.WriteLine("   End Polygon");
		}

		static void writeVertex(String name, Vertex v, StreamWriter w) {
			w.WriteLine("      " + name + " " + formatNr(v.x) + "," + formatNr(v.y) + "," + formatNr(v.z));
		}

		private static string formatNr(double nr) {
			return ((nr >= 0) ? "+" : "-") + Math.Abs(nr).ToString("00000.000000", FileIOHelper.nfi);
		}

		internal static Model load(StreamReader r) {
			Model m = new Model();

			while(!r.EndOfStream) {
				string line = r.ReadLine();
				if (line.Contains("Begin Polygon")){
					loadPolygon(m, r);
				} else if (line.Contains("End Polylist")) {
					// Quickfix to solve T3D files which contain more than one polygon.
					break;
				}
			}
			return m;
		}

		static void loadPolygon(Model m, StreamReader r) {
			List<int> vertices = new List<int>();

			while (!r.EndOfStream) {
				string line = r.ReadLine();

				if (line.Contains("Vertex")) {
					vertices.Add(getVertex(m, line));
				} else if (line.Contains("End Polygon")) {
					break;
				}
			}

			if (vertices.Count < 3) {
				throw new Exception("Vertex with less than 3 vertices.\n");
			}

			m.addPolygonByVertices(vertices);
		}

		static int getVertex(Model m, string line) {
			string[] strArr = line.Trim().Split(' ').Last().Split(',');

			if (strArr.Length != 3){
				throw new Exception("Vertex with an incorrect amount of arguments (3).\n" + line);
			}

			Vertex v = new Vertex(getNr(strArr[0]), getNr(strArr[1]), getNr(strArr[2]));

			int id = m.vertices.IndexOf(v);
			if (id == -1){
				m.vertices.Add(v);
				return m.vertices.LastIndexOf(v);
			}
			return id;
		}

		private static double getNr(string nr) {
			return Double.Parse(nr, FileIOHelper.nfi);
		}
	}
}
