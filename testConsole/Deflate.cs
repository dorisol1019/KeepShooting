using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encode
{
    class Deflate
    {
        public static string Encode(string str)
        {
            byte[] source = Encoding.Unicode.GetBytes(str);

            string dest = "";
            // 入出力用のストリームを生成します 
            using (MemoryStream ms = new MemoryStream())
            {
                using (DeflateStream CompressedStream = new DeflateStream(ms, CompressionMode.Compress, true))
                {
                    // ストリームに圧縮するデータを書き込みます 
                    CompressedStream.Write(source, 0, source.Length);

                    // 圧縮されたデータを バイト配列で取得します 
                    byte[] destination = ms.ToArray();
                    while (true)
                    {
                        int i = ms.ReadByte();
                        if (i == -1)
                        {
                            break;
                        }
                        else
                        {
                            dest += (byte)i;
                        }
                    }

                    //                    dest = destination.ToString();//.Select(e=>(char)e).ToArray().ToString();
                }
            }


            return dest;
        }

        public static string Decompress(byte[] destination)
        {
            using (MemoryStream ms = new MemoryStream(destination))
            using (GZipStream gzip = new GZipStream(ms, CompressionMode.Decompress))
            {
                StringBuilder sb = new StringBuilder();
                byte[] buff = new byte[4096];
                int length = 0;
                while ((length = gzip.Read(buff, 0, buff.Length))> 0)
                {
                    sb.Append(Encoding.Unicode.GetString(buff, 0, length));
                }
                return sb.ToString();
            };
        }

        public static byte[] Compress(string text)
        {
            byte[] source = Encoding.Unicode.GetBytes(text);
            List<byte> bytes = new List<byte>();
            // 入出力用のストリームを生成します
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(ms, CompressionMode.Compress))
                {

                    // ストリームに圧縮するデータを書き込みます
                    gzip.Write(source, 0, source.Length);
                }

                // 圧縮されたデータを バイト配列で取得します
                bytes = ms.ToArray().ToList();
            }
            return bytes.ToArray();
        }



        public static string Decode(string str)
        {
            string dest = "";
            using (var mms = new MemoryStream(str.Select(e => (byte)e).ToArray()))
            using (DeflateStream CompressedStream = new DeflateStream(mms, CompressionMode.Decompress))
            {
                using (MemoryStream ms = new MemoryStream())
                {

                    //　MemoryStream に展開します 
                    while (true)
                    {
                        int rb = CompressedStream.ReadByte();
                        // 読み終わったとき while 処理を抜けます 
                        if (rb == -1)
                        {
                            break;
                        }
                        // メモリに展開したデータを読み込みます 
                        ms.WriteByte((byte)rb);

                    }
                    dest = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return dest;
        }
    }
}
