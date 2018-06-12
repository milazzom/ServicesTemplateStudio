using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizationTest.Services
{
    public interface ILocalizer
    {
        string GetStringForKey(string key);

    }
}
