namespace DatLoader
{
    public class Memory
    {
        public static uint FlipDWord(uint value)
        {
            var result =
                ((value & 0xFF000000) >> 24) |
                ((value & 0x00FF0000) >> 8) |
                ((value & 0x0000FF00) << 8) |
                ((value & 0x000000FF) << 24);
            return result;
        }
    }
}