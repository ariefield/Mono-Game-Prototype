using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame1.Models
{
    public class Input
    {
        public Keys Up { get; set; }

        public Keys Down { get; set; }

        public Keys Left { get; set; }

        public Keys Right { get; set; }

        public Input(Keys up, Keys down, Keys left, Keys right)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }
    }
}
