using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame1.Models;

namespace MonoGame1.Sprites
{
    class Sprite
    {
        public Dictionary<AnimationName, Animation> Animations;

        public Vector2 Position;

        public Animation CurrentAnimation { get; set; }

        public float MoveSpeed { get; set; } = 3f;

        public Vector2 Velocity;

        private float _elapsedTime = 0f;

        public Sprite( Dictionary<AnimationName, Animation> animations, Vector2 pos )
        {
            // Input variables
            Animations = animations;
            Position = pos;

            // Public defaults
            CurrentAnimation = Animations[AnimationName.DownWalk];
            Velocity = Vector2.Zero;

        }

        public void Update( GameTime gameTime )
        {
            SetVelocity();
            SetAnimation(gameTime);

            Position += Velocity;
            Velocity = Vector2.Zero;
        }

        // -- TODO: Handle multiple inputs
        private void SetVelocity()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Velocity.Y = -MoveSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                Velocity.Y = MoveSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Velocity.X = -MoveSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Velocity.X = MoveSpeed;
            }
        }

        // -- TODO: Create 4 more animations for diagonals
        // -- TODO: Handle multiple inputs
        private void SetAnimation( GameTime gameTime )
        {
            bool switchingDirections = false;
            bool moving = false;

            _elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                // When switching from another direction
                if ( CurrentAnimation != Animations[AnimationName.UpWalk])
                {
                    switchingDirections = true;
                }

                CurrentAnimation = Animations[AnimationName.UpWalk];
                moving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                // When switching from another direction
                if (CurrentAnimation != Animations[AnimationName.DownWalk])
                {
                    switchingDirections = true;
                }

                CurrentAnimation = Animations[AnimationName.DownWalk];
                moving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                // When switching from another direction
                if (CurrentAnimation != Animations[AnimationName.LeftWalk])
                {
                    switchingDirections = true;
                }

                CurrentAnimation = Animations[AnimationName.LeftWalk];
                moving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                // When switching from another direction
                if (CurrentAnimation != Animations[AnimationName.RightWalk])
                {
                    switchingDirections = true;
                }

                CurrentAnimation = Animations[AnimationName.RightWalk];
                moving = true;
            }
            // -- TODO: Add Idle animation
            // No input direction - switch back to frame 1 after some delay
            else
            {
                moving = false;
                if (_elapsedTime >= CurrentAnimation.MilliSecPerFrame * CurrentAnimation.CurrentFrame)
                {
                    CurrentAnimation.CurrentFrame = 1;
                    _elapsedTime = 0;
                }
            }

            // Start animation on frame 2 if switching directions
            if (switchingDirections)
            {
                CurrentAnimation.CurrentFrame = 2;
            }

            // Set source rect based on current frame
            CurrentAnimation.SourceRect = new Rectangle((int)CurrentAnimation.StartPosition.X + CurrentAnimation.FrameWidth * (CurrentAnimation.CurrentFrame - 1),
                                        (int)CurrentAnimation.StartPosition.Y,
                                        CurrentAnimation.FrameWidth,
                                        CurrentAnimation.FrameHeight);

            // Only update frame if moving
            if (moving)
            {
                if (CurrentAnimation.CurrentFrame < CurrentAnimation.NumFrames && _elapsedTime >= CurrentAnimation.MilliSecPerFrame * CurrentAnimation.CurrentFrame)
                {
                    CurrentAnimation.CurrentFrame += 1;
                }
                else if (CurrentAnimation.CurrentFrame == CurrentAnimation.NumFrames && _elapsedTime >= CurrentAnimation.MilliSecPerFrame * CurrentAnimation.CurrentFrame)
                {
                    CurrentAnimation.CurrentFrame = 1;
                    _elapsedTime = 0;
                }
            }
        }

    }
}
