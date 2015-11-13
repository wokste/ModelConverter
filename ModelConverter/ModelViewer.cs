using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ModelConverter.Tools;
using System.IO;
using ModelConverter.FileSystem;

namespace ModelConverter {
	public partial class ModelViewer : Form {
		ModelToolChain modelToolChain = new ModelToolChain();

		public ModelViewer() {
			InitializeComponent();

			modelToolChain.onModelChange += new Action(showInfo);
			showInfo();
		}

		private void showInfo() {
			lbSnap.Text = Convert.ToString(modelToolChain.SnapSize);
			lbSnap.Enabled = modelToolChain.ShouldSnap;

			//lbInfo.Text = modelToolChain.Describe();
		}

		private void loadToolStripMenuItem_Click(object sender, EventArgs e) {
			Stream fileStream = null;
			var fileDialog = new OpenFileDialog();

			fileDialog.Filter = FileIOHelper.FileFormats;
			fileDialog.FilterIndex = 1;
			fileDialog.RestoreDirectory = true;

			if (fileDialog.ShowDialog() == DialogResult.OK) {
				try {
					if ((fileStream = fileDialog.OpenFile()) != null) {
						using (fileStream) {
							// Insert code to read the stream here.
							var extention = fileDialog.FileName.Split('.').Last();
							modelToolChain.RawModel = FileIOHelper.loadModel(fileStream, extention);
						}
					}
				} catch (Exception ex) {
					MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
				}
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
			Stream fileStream = null;
			var fileDialog = new SaveFileDialog();

			fileDialog.Filter = FileIOHelper.FileFormats;
			fileDialog.FilterIndex = 1;
			fileDialog.RestoreDirectory = true;

			if (fileDialog.ShowDialog() == DialogResult.OK) {
				try {
					if ((fileStream = fileDialog.OpenFile()) != null) {
						using (fileStream) {
							// Insert code to read the stream here.
							var extention = fileDialog.FileName.Split('.').Last();
							FileIOHelper.saveModel(modelToolChain.FinalModel, fileStream, extention);
						}
					}
				} catch (Exception ex) {
					MessageBox.Show("Error: Could not write file to disk. Original error: " + ex.Message);
				}
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void cbSnap_CheckedChanged(object sender, EventArgs e) {
			modelToolChain.ShouldSnap = cbSnap.Checked;
		}

		private void tbSnap_Scroll(object sender, EventArgs e) {
			modelToolChain.SnapSize = Math.Pow(2, tbSnap.Value);
		}
	}
}
