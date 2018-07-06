using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizationTestDotNetStd.Services
{
    public interface ILocalizer
    {
        string GetStringForKey(string key);
    }
}
