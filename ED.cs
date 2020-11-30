using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zlib;


public class ED
{

    public header FileHeader = new header();
    public FileStream file;
    public List<DataBlock> dataBlocks = new List<DataBlock>();

    public struct header
    {
        public int version { get; set; }
        public bool compressed { get; set; }
        public int worldStringLength { get; set; }
        public string worldString { get; set; }
        public byte[] unknownData { get; set; }
        public int compressedBlocks { get; set; }

        public int maxBlockSize { get; set; }
    }


    public void ReadHeader()
    {
        FileHeader.version = helpers.ReadInt(ref file);
        FileHeader.compressed = helpers.ReadBool(ref file);
        FileHeader.worldStringLength = helpers.ReadInt(ref file);
        FileHeader.worldString = helpers.ReadString(ref file, FileHeader.worldStringLength);
        FileHeader.unknownData = helpers.ReadBytesLength(ref file, 32);
        FileHeader.compressedBlocks = helpers.ReadInt(ref file);
        FileHeader.maxBlockSize = helpers.ReadInt(ref file);
    }

    public void ReadDataBlockSizes()
    {
        for(int i = 0; i < FileHeader.compressedBlocks; i++)
        {
            dataBlocks.Add(new DataBlock(helpers.ReadInt(ref file), 0));
        }
        foreach(DataBlock temp in dataBlocks)
        {
            temp.uncompressedSize = helpers.ReadInt(ref file);
        }
    }

    public void UncompressDataBlocks()
    {
        foreach(DataBlock temp in dataBlocks)
        {
            byte[] tempBytes = helpers.ReadBytesLength(ref file, temp.compressedDataLength);

            MemoryStream input = new MemoryStream(tempBytes);
            MemoryStream output = new MemoryStream();


            using (ZlibStream stream = new ZlibStream(input, CompressionMode.Decompress))
                stream.CopyTo(output);

            temp.uncompressedData = output.ToArray();
        }
    }

    public void WriteFile()
    {
        using (var fs = new FileStream("testoutput.ed", FileMode.Create, FileAccess.Write))
        {
            foreach(DataBlock temp in dataBlocks)
            {
                fs.Write(temp.uncompressedData, 0, temp.uncompressedData.Length);
            }

        }
    }
}
