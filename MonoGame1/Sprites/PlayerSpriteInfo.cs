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
    public static class PlayerSpriteInfo
    {
        // Variables based on texture
        public static Texture2D Texture;
        public static int FrameWidth => Texture.Width / SPRITESHEET_COLUMNS;
        public static int FrameHeight => Texture.Height / SPRITESHEET_ROWS;

        // Constants based on spritesheet (row/cols index base 0)
        public static readonly string SPRITESHEET_PATH = "Player/PlayerAll";
        public static readonly int ANIMATION_FRAMES = 3;
        public static readonly int SPRITESHEET_COLUMNS = 6;
        public static readonly int SPRITESHEET_ROWS = 10;
        public static readonly int UPWALK_ROW = 1;
        public static readonly int DOWNWALK_ROW = 0;
        public static readonly int LEFTWALK_ROW = 3;
        public static readonly int RIGHTWALK_ROW = 2;
        public static readonly int ALLWALK_COLUMN = 0;

        // texture, frameWidth, frameHeight, and numFrames are assumed to be the same for all animations
        public static Dictionary<AnimationName, Animation> GenerateAnimations()
        {
            // Define where in spritesheet animations start
            var animationStartPositions = new Dictionary<AnimationName, Vector2>
            {
                [AnimationName.UpWalk] = new Vector2(FrameWidth * ALLWALK_COLUMN,
                                                     FrameHeight * UPWALK_ROW),

                [AnimationName.DownWalk] = new Vector2(FrameWidth * ALLWALK_COLUMN,
                                                       FrameHeight * DOWNWALK_ROW),

                [AnimationName.LeftWalk] = new Vector2(FrameWidth * ALLWALK_COLUMN,
                                                       FrameHeight * LEFTWALK_ROW),

                [AnimationName.RightWalk] = new Vector2(FrameWidth * ALLWALK_COLUMN,
                                                        FrameHeight * RIGHTWALK_ROW)
            };

            var animations = new Dictionary<AnimationName, Animation>();
            foreach (var animationName in animationStartPositions.Keys)
            {
                animations[animationName] = new Animation(Texture,
                                                          animationStartPositions[animationName],
                                                          ANIMATION_FRAMES,
                                                          FrameWidth,
                                                          FrameHeight);
            }

            return animations;
        }
    }
}
