using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LTTypes.LTTypes;

public static class helpers
{
    /// <summary >
    /// Get the data length for the property of the Lithtech Object
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static Int16 ReadDataLength(ref FileStream file)
    {
        //Read data length 2 bytes
        byte[] tempByte = new byte[2];
        file.Read(tempByte, 0, 2);
        return BitConverter.ToInt16(tempByte, 0);
    }

    /// <summary>
    /// Get the object transform X, Y, Z of the Lithtech Object
    /// </summary>
    /// <param name="file"></param>
    /// <seealso cref="LTVector">See here</seealso>
    /// <returns></returns>
    public static LTVector ReadLTVector(ref FileStream file)
    {
        //Read data length 12 bytes
        //x - single
        //y - single
        //z - single
        byte[] tempByte = new byte[12];
        file.Read(tempByte, 0, 12);

        float x, y, z;

        x = BitConverter.ToSingle(tempByte, 0);
        y = BitConverter.ToSingle(tempByte, sizeof(Single));
        z = BitConverter.ToSingle(tempByte, sizeof(Single) + sizeof(Single));

        return new LTVector((LTFloat)x, (LTFloat)y, (LTFloat)z);
    }

    /// <summary>
    /// Get the object Rotation X, Y, Z, W of the Lithtech Object
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static LTRotation ReadLTRotation(ref FileStream file)
    {
        //Read data length 12 bytes
        //x - single
        //y - single
        //z - single
        //w - single
        byte[] tempByte = new byte[16];
        file.Read(tempByte, 0, 16);

        float x, y, z, w;

        x = BitConverter.ToSingle(tempByte, 0);
        y = BitConverter.ToSingle(tempByte, sizeof(Single));
        z = BitConverter.ToSingle(tempByte, sizeof(Single) + sizeof(Single));
        w = BitConverter.ToSingle(tempByte, sizeof(Single) + sizeof(Single) + sizeof(Single));

        return new LTRotation((LTFloat)x, (LTFloat)y, (LTFloat)z, (LTFloat)w);
    }

    /// <summary>
    /// Get the objects string, either name or paramters (ie: trigger message) of the Lithtech Object
    /// </summary>
    /// <param name="file"></param>
    /// <param name="stringLength"></param>
    /// <returns></returns>
    public static string ReadString(ref FileStream file, int stringLength)
    {
        //Read the string
        byte[] tempByte = new byte[stringLength];
        file.Read(tempByte, 0, tempByte.Length);
        return System.Text.Encoding.ASCII.GetString(tempByte);
    }

    /// <summary>
    /// Get the objects property type of the Lithtech Object
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static byte ReadPropertyType(ref FileStream file)
    {
        //Read the string
        byte[] tempByte = new byte[1];
        file.Read(tempByte, 0, tempByte.Length);
        return tempByte[0];
    }

    /// <summary>
    /// Get the LongInt used in AllowedGameTypes of the Lithtech Object
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static Int64 ReadLongInt(ref FileStream file)
    {
        //Read the string
        byte[] tempByte = new byte[8];
        file.Read(tempByte, 0, tempByte.Length);
        return BitConverter.ToInt64(tempByte, 0);
    }

    /// <summary>
    /// Get the true or false flag from the property of the Lithtech Object
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static bool ReadBool(ref FileStream file)
    {
        //Read the string
        byte[] tempByte = new byte[1];
        file.Read(tempByte, 0, tempByte.Length);
        return BitConverter.ToBoolean(tempByte, 0);
    }
    /// <summary>
    /// Get the Real used in single float values of the Lithtech Object
    /// </summary>
    /// <param name="file"></param>
    /// <returns>description</returns>
    public static float ReadReal(ref FileStream file)
    {
        //Read the string
        byte[] tempByte = new byte[4];
        file.Read(tempByte, 0, tempByte.Length);
        return BitConverter.ToSingle(tempByte, 0);
    }

    /// <summary>
    /// Get the integer in the bitstream
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static int ReadInt(ref FileStream file)
    {
        //Read the int
        byte[] tempByte = new byte[4];
        file.Read(tempByte, 0, tempByte.Length);
        return BitConverter.ToInt32(tempByte, 0);
    }

    public static byte ReadByte(ref FileStream file)
    {
        return (byte)file.ReadByte();
    }

    public static byte[] ReadBytesLength(ref FileStream file, int length)
    {
        // byte[]
        byte[] tempByte = new byte[length];
        file.Read(tempByte, 0, length);
        return tempByte;
    }
}
