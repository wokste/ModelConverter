using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelConverter.DataStructures;

namespace ModelConverter.Tools {
	class ModelToolChain {
		private Model _rawModel = new Model();

		public ModelToolChain() {
			Refresh();
		}

		public Model RawModel{
			get { return _rawModel; }
			set {
				_rawModel = value;
				Refresh();
			}
		}

		private Model _finalModel;

		public Model FinalModel {
			get { return _finalModel; }
		}

		private double _snapSize = 1;
		public double SnapSize {
			get { return _snapSize; }
			set {
				_snapSize = value;
				Refresh();
			}
		}

		private bool _shouldSnap = true;
		public bool ShouldSnap {
			get { return _shouldSnap; }
			set {
				_shouldSnap = value;
				Refresh();
			}
		}

		public event Action onModelChange;

		public void Refresh() {
			_finalModel = RawModel.clone();

			if (ShouldSnap)
				SnapTool.snap(_finalModel, SnapSize);

			if (onModelChange != null)
				onModelChange();
		}

		internal string Describe() {
			return FinalModel.getDescription() + BoundingBoxCalculator.getBoundingBox(FinalModel) + FinalModel.getErrors();
		}
	}
}
