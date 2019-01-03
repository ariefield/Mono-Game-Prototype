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
    //public enum TextureKeys
    //{
    //    UpWalk = 0,
    //    DownWalk = 1,
    //    LeftWalk = 2,
    //    RightWalk = 3
    //}

    class Sprite
    {
        public const int milliSecPerFrame = 80;

        public Texture2D Texture { get; set; }

        public Rectangle SourceRect { get; set; }

        public float MoveSpeed { get; set; }

        public Dictionary<string, Texture2D> TextureDict { get; set; }

        public Vector2 position;

        public Vector2 velocity;

        private string _currentAnimation = "DownWalk";

        private int _frame = 0;

        private int _numFrames = 3;

        private float _elapsedTime = 0f;

        private int counter = 0;


        public Sprite( Dictionary<string, Texture2D> textureDict, Vector2 pos )
        {
            // Input variables
            TextureDict = textureDict;
            Texture = TextureDict["DownWalk"];
            position = pos;

            // Public defaults
            SourceRect = new Rectangle((Texture.Width / _numFrames) * _frame,
                                       0,
                                       Texture.Width / _numFrames,
                                       Texture.Height);
            velocity = Vector2.Zero;
            MoveSpeed = 3f;

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
                //Set texture and source rect and frame

            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                // When switching from another direction
                if (_currentAnimation != "DownWalk")
                {
                    _currentAnimation = "DownWalk";
                    _frame = 2;
                }

                // Set source rect based on current frame
                SourceRect = new Rectangle((Texture.Width / _numFrames) * (_frame - 1), 
                                           0, 
                                           Texture.Width / _numFrames,
                                           Texture.Height);

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
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X = -MoveSpeed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X = MoveSpeed;
            }
            else
            {
                if (_elapsedTime >= milliSecPerFrame * _frame)
                {
                    _frame = 1;
                    _elapsedTime = 0;
                }
                // Set source rect based on current frame
                SourceRect = new Rectangle((Texture.Width / _numFrames) * (_frame - 1),
                                           0,
                                           Texture.Width / _numFrames,
                                           Texture.Height);
            }
        }

    }
}
