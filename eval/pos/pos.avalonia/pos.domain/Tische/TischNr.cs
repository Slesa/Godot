using System;
using ddd.core;

namespace pos.domain.Tische
{
    public class TischNr : ValueObject<TischNr>
    {
        public TischNr(uint tisch, uint partei)
        {
            if(tisch==0) throw new ArgumentException("Tischnummer muss größer 0 sein.");
            if(partei==0) throw new ArgumentException("Parteinummer muss größer 0 sein.");
            Tisch = tisch;
            Partei = partei;
        }
        public uint Tisch { get; }
        public uint Partei { get; }

        public static TischNr TestDefault = new TischNr(42, 1);
    }
}