using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DataBlock
{
    public int compressedDataLength { get; set; }
    public int uncompressedSize { get; set; }
    public byte[] uncompressedData { get; set; }

    public DataBlock(int comp, int uncmp)
    {
        compressedDataLength = comp;
        uncompressedSize = uncmp;
        uncompressedData = null;
    }
}
