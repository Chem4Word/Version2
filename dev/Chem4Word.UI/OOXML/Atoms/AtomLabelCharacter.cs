using System;
using System.Windows;

namespace Chem4Word.UI.OOXML.Atoms
{
    public class AtomLabelCharacter
    {
        public Point Position { get; set; }
        public OoXmlCharacter Character { get; set; }
        public String Colour { get; set; }
        public bool IsSubScript { get; set; }
        public Char Ascii { get; set; }

        public AtomLabelCharacter(Point position, OoXmlCharacter character, String colour, Char ascii)
        {
            Position = position;
            Character = character;
            Colour = colour;
            Ascii = ascii;
        }
    }
}
