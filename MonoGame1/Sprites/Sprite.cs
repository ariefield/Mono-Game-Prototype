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

        public Input Input;

        public Animation CurrentAnimation { get; set; }

        public float MoveSpeed { get; set; } = 3f;

        public Vector2 Velocity;

        public bool Moveable { get; set; }

        public bool SwitchingDirections { get; set; }

        private float _elapsedTime;

        public Sprite( Dictionary<AnimationName, Animation> animations, Vector2 pos, Input input = null, bool moveable = false )
        {
            // Input variables
            Animations = animations;
            Position = pos;
            Input = input;
            Moveable = moveable;

            // Public defaults
            Velocity = Vector2.Zero;
            SwitchingDirections = false;
            if (Moveable)
            {
                CurrentAnimation = Animations[AnimationName.DownWalk];
            }
            else
            {
                CurrentAnimation = Animations[AnimationName.Idle];
            }

            // Private Defaults
            _elapsedTime = 0f;
        }

        public void Update( GameTime gameTime )
        {
            SetVelocity();
            HandleAnimation(gameTime);

            Position += Velocity;
            Velocity = Vector2.Zero;
        }

        // -- TODO: Handle multiple inputs
        private void SetVelocity()
        {
            if (Input == null)
            {
                return;
            }

            if (Keyboard.GetState().IsKeyDown(Input.Up))
            {
                Velocity.Y = -MoveSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Input.Down))
            {
                Velocity.Y = MoveSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Input.Left))
            {
                Velocity.X = -MoveSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Input.Right))
            {
                Velocity.X = MoveSpeed;
            }
        }

        // -- TODO: Create 4 more animations for diagonals
        // -- TODO: Handle multiple inputs
        private void HandleAnimation( GameTime gameTime )
        {
            _elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            SetAnimation(gameTime);
            UpdateAnimationFrame();
        }

        private void SetAnimation( GameTime gameTime )
        {
            if (Input == null)
            {
                return;
            }

            SwitchingDirections = false;

            if (Keyboard.GetState().IsKeyDown(Input.Up))
            {
                // When switching from another direction
                if (CurrentAnimation != Animations[AnimationName.UpWalk])
                {
                    SwitchingDirections = true;
                }

                CurrentAnimation = Animations[AnimationName.UpWalk];
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
            {
                // When switching from another direction
                if (CurrentAnimation != Animations[AnimationName.DownWalk])
                {
                    SwitchingDirections = true;
                }

                CurrentAnimation = Animations[AnimationName.DownWalk];
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Left))
            {
                // When switching from another direction
                if (CurrentAnimation != Animations[AnimationName.LeftWalk])
                {
                    SwitchingDirections = true;
                }

                CurrentAnimation = Animations[AnimationName.LeftWalk];
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Right))
            {
                // When switching from another direction
                if (CurrentAnimation != Animations[AnimationName.RightWalk])
                {
                    SwitchingDirections = true;
                }

                CurrentAnimation = Animations[AnimationName.RightWalk];
            }
        }

        // -- TODO: Add Idle animation
        private void UpdateAnimationFrame()
        {
            // Start animation on frame 2 if switching directions
            if (SwitchingDirections)
            {
                CurrentAnimation.CurrentFrame = 2;
                _elapsedTime = 0;  //Potential problem?
            }

            // Set source rect based on current frame
            CurrentAnimation.SourceRect = new Rectangle((int)CurrentAnimation.StartPosition.X + CurrentAnimation.FrameWidth * (CurrentAnimation.CurrentFrame - 1),
                                        (int)CurrentAnimation.StartPosition.Y,
                                        CurrentAnimation.FrameWidth,
                                        CurrentAnimation.FrameHeight);

            // Only update frame if moving
            if (Velocity != Vector2.Zero || !Moveable)
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
            // No input direction - switch back to frame 1 after some delay
            else if (Moveable && _elapsedTime >= CurrentAnimation.MilliSecPerFrame * CurrentAnimation.CurrentFrame)
            {
                CurrentAnimation.CurrentFrame = 1;
                _elapsedTime = 0;
            }
        }
    }
}
