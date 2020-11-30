using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lithtech_World_Explorer
{
    public partial class Form1 : Form
    {
        public ED editorFile;
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Lithtech Dedit World|*.ed";
            if (fileDialog.ShowDialog() != DialogResult.Cancel)
            {
                // If the file name is not an empty string open it for saving.
                if (fileDialog.FileName != "")
                {
                    //Clear the message box
                    resultText.Clear();

                    //Load the .ed file
                    editorFile = new ED();

                    //Setup our class with this handle
                    editorFile.file = (System.IO.FileStream)fileDialog.OpenFile();

                    //Read the header of the file
                    editorFile.ReadHeader();

                    editorFile.ReadDataBlockSizes();

                    editorFile.UncompressDataBlocks();


                    //Update our richtext box

                    string tempString = "Lithtech .ed World Reader \n";

                    tempString += "Header Info:\n";
                    tempString += "World Version: " + editorFile.FileHeader.version.ToString() + "\n";
                    tempString += "Compressed: " + editorFile.FileHeader.compressed.ToString() + "\n";
                    tempString += "World String: " + editorFile.FileHeader.worldString + "\n";
                    tempString += "Compressed Blocks: " + editorFile.FileHeader.compressedBlocks + "\n";

                    tempString += "     Datablock layout:\n\n";

                    int i = 1;
                    foreach(DataBlock temp in editorFile.dataBlocks)
                    {
                        tempString += "\n     DataBlockID: " + i.ToString();
                        tempString += "\n          DataLength: " + temp.compressedDataLength.ToString();
                        tempString += "\n          Uncompressed Size: " + temp.uncompressedData.Length.ToString();
                            i++;
                    }

                    resultText.Text = tempString;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorFile.WriteFile();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
