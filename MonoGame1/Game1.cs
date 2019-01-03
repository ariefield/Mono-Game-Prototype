using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame1.Sprites;


namespace MonoGame1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Sprite> sprites;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            sprites = new List<Sprite>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Dictionary<string, Texture2D> textureSets = new Dictionary<string, Texture2D>();
            textureSets["DownWalk"] = Content.Load<Texture2D>("Player/DownWalk");
            textureSets["UpWalk"] = Content.Load<Texture2D>("Player/UpWalk");
            textureSets["LeftWalk"] = Content.Load<Texture2D>("Player/LeftWalk");
            textureSets["RightWalk"] = Content.Load<Texture2D>("Player/RightWalk");


            // TODO: Create sprite object/animation dictionary
            sprites.Add( new Sprite( textureSets, new Vector2(0, 0)) );
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if ( GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) )
                Exit();

            foreach (Sprite sprite in sprites)
            {
                sprite.Update( gameTime );
            }

            //Console.WriteLine("Elapsed Time:" + gameTime.ElapsedGameTime.Milliseconds);
            //Console.WriteLine("Total Time:" + gameTime.TotalGameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            foreach (Sprite sprite in sprites)
            {
                spriteBatch.Draw(sprite.Texture, sprite.position, sprite.SourceRect, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
