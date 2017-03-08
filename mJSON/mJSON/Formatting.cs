using System;

namespace mJSON
{
    [Flags]
    public enum Formatting
    {
        None = 0,
        Indented = 1,
        IncludeTypeName = 2,
        BreakReqursion = 4
    }
}
