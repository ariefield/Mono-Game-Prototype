using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame1.Sprites
{
    public enum Animation
    {
        UpWalk = 0,
        DownWalk = 1,
        LeftWalk = 2,
        RightWalk = 3
    }

    

    class Sprite
    {
        public const int milliSecPerFrame = 80;

        public Texture2D Texture { get; set; }

        public Vector2 position;

        public Rectangle SourceRect { get; set; }

        public float MoveSpeed { get; set; }

        public int FrameWidth { get { return Texture.Width / 6; } }

        public int FrameHeight { get { return Texture.Height / 10; } }

        public Vector2 velocity;

        public Vector2 animationPosition;

        public Dictionary<Animation, Vector2> AnimationPositions { get; set; }

        private const int _numFrames = 3;

        private Animation _currentAnimation = Animation.DownWalk;

        private int _frame = 0;

        private float _elapsedTime = 0f;

        private bool _moving = false;


        public Sprite( Texture2D texture, Vector2 pos )
        {
            // Input variables
            Texture = texture;
            position = pos;

            // Public defaults
            SourceRect = new Rectangle(0,
                                       0,
                                       FrameWidth,
                                       FrameHeight);

            MoveSpeed = 3f;
            velocity = Vector2.Zero;
            animationPosition = Vector2.Zero;

            // Initialize animation positions dict
            AnimationPositions = new Dictionary<Animation, Vector2>
            {
                [Animation.UpWalk] = new Vector2(FrameWidth * 0, FrameHeight * 1),
                [Animation.DownWalk] = new Vector2(FrameWidth * 0, FrameHeight * 0),
                [Animation.LeftWalk] = new Vector2(FrameWidth * 0, FrameHeight * 3),
                [Animation.RightWalk] = new Vector2(FrameWidth * 0, FrameHeight * 2)
            };

        }

        public void Update( GameTime gameTime )
        {
            SetVelocity();
            SetAnimation(gameTime);

            position += velocity;
            velocity = Vector2.Zero;
        }


        private void SetVelocity()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                velocity.Y = -MoveSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                velocity.Y = MoveSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X = -MoveSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X = MoveSpeed;
            }
        }

        // TODO: Create 4 more animations for diagonals
        private void SetAnimation( GameTime gameTime )
        {
            _elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                // When switching from another direction
                if (_currentAnimation != Animation.UpWalk)
                {
                    _frame = 2;
                }

                _currentAnimation = Animation.UpWalk;
                _moving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                // When switching from another direction
                if (_currentAnimation != Animation.DownWalk)
                {
                    _frame = 2;
                }

                _currentAnimation = Animation.DownWalk;
                _moving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                // When switching from another direction
                if (_currentAnimation != Animation.LeftWalk)
                {
                    _frame = 2;
                }

                _currentAnimation = Animation.LeftWalk;
                _moving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                // When switching from another direction
                if (_currentAnimation != Animation.RightWalk)
                {
                    _frame = 2;
                }

                _currentAnimation = Animation.RightWalk;
                _moving = true;
            }
            else
            {
                _moving = false;
                if (_elapsedTime >= milliSecPerFrame * _frame)
                {
                    _frame = 1;
                    _elapsedTime = 0;
                }
            }

            // Set animation starting positions
            animationPosition = AnimationPositions[_currentAnimation];

            // Set source rect based on current frame
            SourceRect = new Rectangle( (int)animationPosition.X + FrameWidth * (_frame - 1),
                                        (int)animationPosition.Y,
                                        FrameWidth,
                                        FrameHeight );

            if (_moving)
            {
                if (_frame < _numFrames && _elapsedTime >= milliSecPerFrame * _frame)
                {
                    _frame += 1;
                }
                else if (_frame == _numFrames && _elapsedTime >= milliSecPerFrame * _frame)
                {
                    _frame = 1;
                    _elapsedTime = 0;
                }
            }
        }

    }
}
