using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame1.Sprites
{
    public static class GoblinSpriteInfo
    {
        // Variables based on texture
        public static Texture2D Texture;

        // Constants based on spritesheet (row/cols index base 0)
        public static readonly string SPRITESHEET_PATH = "Goblin/GoblinAll";
        public static readonly int ANIMATION_FRAMES = 5;
        public static readonly int SPRITESHEET_COLUMNS = 7;
        public static readonly int SPRITESHEET_ROWS = 9;
        public static readonly int IDLE_ROW = 5;
        public static readonly int IDLE_COLUMN = 0;

        //Constants based on animation
        public static readonly int MILLI_PER_FRAME = 120;

        // texture, frameWidth, frameHeight, and numFrames are assumed to be the same for all animations
        public static Dictionary<AnimationName, Animation> GenerateAnimations()
        {
            if (Texture == null)
            {
                throw new NullReferenceException("Texture was not set, therefore animations cannot be set");
            }

            int frameWidth = Texture.Width / SPRITESHEET_COLUMNS;
            int frameHeight = Texture.Height / SPRITESHEET_ROWS;

            // Define where in spritesheet animations start
            var animationStartPositions = new Dictionary<AnimationName, Vector2>
            {
                [AnimationName.Idle] = new Vector2(frameWidth * IDLE_COLUMN,
                                                     frameHeight * IDLE_ROW)
            };

            var animations = new Dictionary<AnimationName, Animation>();
            foreach (var animationName in animationStartPositions.Keys)
            {
                animations[animationName] = new Animation(Texture,
                                                          animationStartPositions[animationName],
                                                          ANIMATION_FRAMES,
                                                          frameWidth,
                                                          frameHeight,
                                                          MILLI_PER_FRAME);
            }

            return animations;
        }
    }
}
