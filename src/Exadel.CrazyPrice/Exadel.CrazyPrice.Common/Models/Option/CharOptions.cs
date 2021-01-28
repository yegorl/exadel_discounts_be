using System;

namespace Exadel.CrazyPrice.Common.Models.Option
{
    /// <summary>
    /// Options for checkup a string content.
    /// </summary>
    [Flags]
    public enum CharOptions
    {
        Letter = 1,
        Upper = 2,
        Lower = 4,
        Digit = 8,
        Number = 16,
        Separator = 32,
        WhiteSpace = 64,
        Symbol = 128,
        Control = 256,
        Punctuation = 512
    }
}