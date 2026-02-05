using System;
using System.Collections.Generic;
using System.Text;

namespace Horoscopo.Core.Configuration.Interfaces
{
    public interface ICurrentConfiguration
    {
        public IHoroscopoConfig Horoscopo { get; set; }
    }
}
