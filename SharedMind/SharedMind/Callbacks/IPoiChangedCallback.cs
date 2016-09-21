[using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedMind.Callbacks
{
    public interface IPoiChangedCallback
    {
        bool PoiChanged();
    }
}
