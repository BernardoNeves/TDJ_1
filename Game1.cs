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
        public SpriteFont Arial;
        public float Distance;
        public int m = 1, Score = 0, Combo = 0, HP = 100, Difficulty = 3;
        Song song;

        List<Notes> fixedNotes = new List<Notes>();
        List<Strings> scoreStrings = new List<Strings>();
        Dictionary<int, Notes> fallingNotes = new Dictionary<int, Notes>();


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
            Arial = Content.Load<SpriteFont>("Font/Arial");

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

            FixedNotes = new Notes(this, "Img/right", new Vector2(-500, -500)); //empty note for when no key is being pressed
            fixedNotes.Add(FixedNotes);

            FixedNotes = new Notes(this, "Img/pressedLeft", new Vector2(-500, -500)); // pressed keys
            fixedNotes.Add(FixedNotes);
            FixedNotes = new Notes(this, "Img/pressedTop", new Vector2(-500, -500));
            fixedNotes.Add(FixedNotes);
            FixedNotes = new Notes(this, "Img/pressedDown", new Vector2(-500, -500));
            fixedNotes.Add(FixedNotes);
            FixedNotes = new Notes(this, "Img/pressedRight", new Vector2(-500, -500));
            fixedNotes.Add(FixedNotes);

            FixedNotes = new Notes(this, "Img/pressedRight", new Vector2(-500, -500));
            fixedNotes.Add(FixedNotes);
            Notes FallingNotes;
            int j = 0;
            for (int i = 0; i < 135; i++)
            {
                Random rand = new Random();
                int number = rand.Next(1, 4);
                switch (number)
                {
                    case 1:
                        j++;
                        FallingNotes = new Notes(this, "Img/fallingLeft", new Vector2(332, -j * 600));
                        fallingNotes.Add(j, FallingNotes);
                        break;

                    case 2:
                        j++;
                        FallingNotes = new Notes(this, "Img/fallingTop", new Vector2(487, -j * 600));
                        fallingNotes.Add(j, FallingNotes);
                        break;

                    case 3:
                        j++;
                        FallingNotes = new Notes(this, "Img/fallingDown", new Vector2(642, -j * 600));
                        fallingNotes.Add(j, FallingNotes);
                        break;
                    case 4:
                        j++;
                        FallingNotes = new Notes(this, "Img/fallingRight", new Vector2(797, -j * 600));
                        fallingNotes.Add(j, FallingNotes);
                        break;

                }
            }
            Strings ScoreStrings = new Strings(this, Arial, "300", new Vector2(0, -500), Color.Blue);
            scoreStrings.Add(ScoreStrings);
            ScoreStrings = new Strings(this, Arial, "200", new Vector2(0, -500), Color.Green);
            scoreStrings.Add(ScoreStrings);
            ScoreStrings = new Strings(this, Arial, "100", new Vector2(0, -500), Color.Yellow);
            scoreStrings.Add(ScoreStrings);
            ScoreStrings = new Strings(this, Arial, "X", new Vector2(0, -500), Color.Red);
            scoreStrings.Add(ScoreStrings);
            ScoreStrings = new Strings(this, Arial, " ", new Vector2(0, -500), Color.Red);
            scoreStrings.Add(ScoreStrings);
            ScoreStrings = new Strings(this, Arial, "0", new Vector2(50, 50), Color.White); // Global Score
            scoreStrings.Add(ScoreStrings);
            ScoreStrings = new Strings(this, Arial, "0X", new Vector2(50, 620), Color.White); // Combo Counter
            scoreStrings.Add(ScoreStrings);
            ScoreStrings = new Strings(this, Arial, "100 HP", new Vector2(1000, 50), Color.White); // HP Counter
            scoreStrings.Add(ScoreStrings);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            int i = 1, j = 4, k = 4;
            foreach (KeyValuePair<int, Notes> note in fallingNotes)
            {
                fallingNotes[i].SetNotePosition(new Vector2(fallingNotes[i].GetNotePosition().X, fallingNotes[i].GetNotePosition().Y + Difficulty * 300 * (float)gameTime.ElapsedGameTime.TotalSeconds));
                i++;
            }
            if (fallingNotes[m].GetNotePosition().Y > 840.0000000f && m < 135)
            {
                if (fallingNotes[m].GetPressedNote() == false)
                {
                    Combo = 0;
                    HP -= 10;
                    scoreStrings[2].SetStringPosition(new Vector2(fixedNotes[k].GetNotePosition().X + 70, fixedNotes[k].GetNotePosition().Y - 230));

                }
                m++;
            }
            for (i = 0; i < 5; i++)
            {
                for (j = 0; j <= 30; j++)
                {
                    scoreStrings[i].SetStringPosition(new Vector2(scoreStrings[i].GetStringPosition().X, scoreStrings[i].GetStringPosition().Y - j * (float)gameTime.ElapsedGameTime.TotalSeconds));
                }
            }
            for (i = 4; i < 9; i++)
            {
                fixedNotes[i].SetNotePosition(new Vector2(-500, -500));

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                k = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                k = 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                k = 2;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                k = 3;
            fixedNotes[k + 5].SetNotePosition(new Vector2(fixedNotes[k].GetNotePosition().X, fixedNotes[k].GetNotePosition().Y));

            if (fixedNotes[k].GetNotePosition().X == fallingNotes[m].GetNotePosition().X && fallingNotes[m].GetPressedNote() == false)
            {
                Distance = Math.Abs((fixedNotes[k].GetNotePosition().Y - fallingNotes[m].GetNotePosition().Y));
                j = 4;
                if (Distance < 20)
                {
                    Combo++;
                    HP += 10;
                    j = 0;
                    Score += 300 * Combo;
                    fallingNotes[m].SetPressedNote(true);
                }
                else if (Distance < 75)
                {
                    Combo++;
                    HP += 5;
                    j = 1;
                    Score += 200 * Combo;
                    fallingNotes[m].SetPressedNote(true);
                }
                else if (Distance < 150)
                {
                    Combo++;
                    HP -= 5;
                    j = 2;
                    Score += 100 * Combo;
                    fallingNotes[m].SetPressedNote(true);
                }
                else if (Distance < 300)
                {
                    HP -= 10;
                    j = 3;
                    fallingNotes[m].SetPressedNote(true);
                    Combo = 0;
                }
                else
                    j = 4;
                scoreStrings[j].SetStringPosition(new Vector2(fixedNotes[k].GetNotePosition().X + 70, fixedNotes[k].GetNotePosition().Y - 230));
            }
            if (HP > 100)
                HP = 100;
            if (HP < 0)
                HP = 0;

            scoreStrings[5].SetStringText(Score.ToString());
            scoreStrings[6].SetStringText(Combo + "X");
            scoreStrings[7].SetStringText(HP + " HP");

            if (HP == 0)
                Exit();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            foreach (Notes note in fixedNotes)
            {
                note.drawNote(_spriteBatch);
            }

            foreach (KeyValuePair<int, Notes> note in fallingNotes)
            {
                note.Value.drawNote(_spriteBatch);
            }
            foreach (Strings String in scoreStrings)
            {
                String.drawString(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
