using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace TheGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        List<Notes> fixedNotes = new List<Notes>();
        Dictionary<int, Notes> fallingNotes = new Dictionary<int, Notes>();

        Song song;

        float spawnY = -150f;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            this.song = Content.Load<Song>("Sound/nevergoing");
            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.005f;

            Notes FixedNotes = new Notes(this, "Img/left", new Vector2(332, 530));
            fixedNotes.Add(FixedNotes);
            FixedNotes = new Notes(this, "Img/top", new Vector2(487, 530));
            fixedNotes.Add(FixedNotes);
            FixedNotes = new Notes(this, "Img/down", new Vector2(642, 530));
            fixedNotes.Add(FixedNotes);
            FixedNotes = new Notes(this, "Img/right", new Vector2(797, 530));
            fixedNotes.Add(FixedNotes);
            
            Notes FallingNotes;
            int j = 1;
            for (int i = 0; i < 100; i++)
            {
            FallingNotes = new Notes(this, "Img/fallingLeft", new Vector2(332, (spawnY-j*600)));
            fallingNotes.Add(j, FallingNotes);
            j++;

            FallingNotes = new Notes(this, "Img/fallingTop", new Vector2(487, (spawnY - j * 600)));
            fallingNotes.Add(j, FallingNotes);
            j++;

            FallingNotes = new Notes(this, "Img/fallingDown", new Vector2(642, (spawnY - j * 600)));
            fallingNotes.Add(j, FallingNotes);
            j++;

            FallingNotes = new Notes(this, "Img/fallingRight", new Vector2(797, (spawnY - j * 600)));
            fallingNotes.Add(j, FallingNotes);
            j++;
            }

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            int i=1;
            foreach(KeyValuePair<int, Notes> note in fallingNotes)
            {
                fallingNotes[i].SetNotePosition(new Vector2(fallingNotes[i].GetNotePosition().X, fallingNotes[i].GetNotePosition().Y + 300 * (float)gameTime.ElapsedGameTime.TotalSeconds)); // test move
                i++;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                   //if(Vector2.Distance()
                   //{
                   //}
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
            }
            
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightPink);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            foreach (Notes note in fixedNotes)
            {
                note.drawNote(_spriteBatch);
            }

            foreach(KeyValuePair<int, Notes> note in fallingNotes)
            {
               note.Value.drawNote(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
