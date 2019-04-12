using System;
using PersonalPlanung.Core.Model;

namespace PersonalPlanung.Core.Business
{
    public interface IchFindePersonal
    {
        Person FindeFür(DateTime von, DateTime bis, Rolle rolle);
    }
}