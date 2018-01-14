using System;
using System.IO;
using System.IO.Compression;

namespace TiledJsonUtility
{
    public class TiledBase64Deflate
    {
        public Stream Result { get; private set; }

        public const string BASE64 = "base64";
        public const string GZIP = "gzip";
        public const string ZLIB = "zlib";
        private const uint Adler32modulo = 65521;

        public TiledBase64Deflate(string encoding, string compression, string data)
        {
            if (!encoding.Equals(BASE64))
            {
                throw new Exception("Only encoding=base64 data is supported.");
            }

            byte[] bytes = Convert.FromBase64String(data);

            if (compression == null || compression.Length == 0)
            {
                // no compression
                Result = new MemoryStream(bytes, false);
            }
            if (compression.Equals(GZIP))
            {
                MemoryStream compressedStream = new MemoryStream(bytes, false);
                Result = new GZipStream(compressedStream, CompressionMode.Decompress);
            }
            else if (compression.Equals(ZLIB))
            {
                Result = DeflateZLIB(bytes);
            }
            else
            {
                throw new Exception("Only compression=" + GZIP + " or compression=" + ZLIB + " are supported");
            }
        }

        public static bool ValidateZLIBHeader(byte[] data)
        {
            // RFC1950 - validate two header bytes (CMF and FLG)
            byte CMF = data[0];
            if (CMF != 0x78)
            {
                // CINFO (Compression info)  = 7 32KB window 
                // CM (Compression method)   = 8 deflate
                return false;
            }
            byte FLG = data[1];
            if ((CMF * 256 + FLG) % 31 != 0)
            {
                // The FCHECK value must be such that CMF and FLG, when viewed as
                // a 16 - bit unsigned integer stored in MSB order (CMF * 256 + FLG),
                // is a multiple of 31.
                return false;
            }
            return true;
        }

        public static Stream DeflateZLIB(byte[] data)
        {
            if (data.Length <= 6)
            {
                // too short to contain header, data, and Adler32 checksum
                throw new Exception("data length is too short to contain header, data and checksum");
            }
            // Check the header
            if (!ValidateZLIBHeader(data))
            {
                throw new Exception("Unable to verify zlib header");
            }
            // Length check already 
            uint expected_checksum = (uint)((data[data.Length - 4] << 24) |
                                            (data[data.Length - 3] << 16) |
                                            (data[data.Length - 2] << 8) |
                                            (data[data.Length - 1]));
            // typical zlib compression ratios are on the order of 2:1 to 5:1
            MemoryStream outputStream = new MemoryStream(data.Length * 5);
            // skip over 2-byte header and 4-byte checksum at the end
            MemoryStream inputStream = new MemoryStream(data, 2, data.Length - 6);

            using (DeflateStream deflateStream = new DeflateStream(inputStream, CompressionMode.Decompress))
            {
                // Adler32 checksum values - see RFC1950
                uint a = 1;
                uint b = 0;

                byte[] outBuffer = new byte[2048]; // cannot be so large that a or b overrun
                int length = 0;
                while ((length = deflateStream.Read(outBuffer, 0, outBuffer.Length)) != 0)
                {
                    outputStream.Write(outBuffer, 0, length);
                    for (int i = 0; i < length; i++)
                    {
                        a += outBuffer[i];
                        b += a;
                    }
                    a %= Adler32modulo;
                    b %= Adler32modulo;
                }
                uint actual_checksum = ((b * 65536) + a);
                if (actual_checksum != expected_checksum)
                {
                    throw new Exception("Unable to deflate, checksum failure expected_checksum=" + expected_checksum + " actual_checksum=" + actual_checksum);
                }

                outputStream.Seek(0, SeekOrigin.Begin);
                return outputStream;
            }
        }
    }
}