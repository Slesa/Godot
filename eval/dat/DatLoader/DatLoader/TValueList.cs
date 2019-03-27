using System;
using System.Collections.Generic;
using System.IO;

namespace DatLoader
{
    public class TValueList : Dictionary<int, TValue>
    {
        public void load(string fn/*, string path*/)
        {
            //if (!path.EndsWith("\\"))
            //    fn = path + "\\" + defPath + "\\" + fn;
            //else
            //    fn = path + defPath + "\\" + fn;

            if (!File.Exists(fn))
                return;
            using (var fh = new FileStream(fn, FileMode.Open, FileAccess.Read))
            {
                using (var st = new BinaryReader(fh, System.Text.Encoding.BigEndianUnicode))
                {
                    ReadElements(fh, st);
                }
            }
        }

        void ReadElements(FileStream fh, BinaryReader st)
        {
            while(fh.Position < fh.Length)
            try
            {
                TValue value = new TValue();
                uint size = Memory.FlipDWord((uint)st.ReadInt32());
                for (uint i = 0; i < size; i++)
                {
                    var keyLength = Memory.FlipDWord((uint)st.ReadInt32());
                    /*char[]*/
                    var key = new string(st.ReadChars((int)(keyLength/2))); // Unicode Big Endian!
                    var valueLength = Memory.FlipDWord((uint)st.ReadInt32());
                    /*char[]*/
                    var val = new string(st.ReadChars((int)(valueLength/2))); // Unicode Big Endian!
                    value.SetValue(key, val);
                }
                Add(value.GetId(), value);
            }
            catch (IOException ex)
            {
                System.Console.Error.WriteLine($"Error reading DAT file: {ex.Message}");
                break;
            }

        }
    }
}