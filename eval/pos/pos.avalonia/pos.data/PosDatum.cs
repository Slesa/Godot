using System;

namespace pos.data
{
    public class PosDatum
    {
        public uint Id { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is PosDatum))
                return false;
            if (GetType() != obj.GetType())
                return false;
            var datum = (PosDatum)obj;
            return datum.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ 31;
        }

        public static bool operator ==(PosDatum left, PosDatum right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null));
            else
                return left.Equals(right);
        }
        public static bool operator !=(PosDatum left, PosDatum right)
        {
            return !(left == right);
        }
    }
}