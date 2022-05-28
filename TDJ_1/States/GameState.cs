using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.States
{
    public class GameState : State
    {
        List<Notes> fixedNotes = new List<Notes>();
        List<Strings> scoreStrings = new List<Strings>();
        Dictionary<int, Notes> fallingNotes = new Dictionary<int, Notes>();

        SoundEffectInstance hit, cbreak;
        Texture2D bar;

        public float Distance;
        public int m = 1, Score = 0, Combo = 0, HP = 100, Difficulty = 3;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, int difficulty) : base(game, graphicsDevice, content)
        {
            hit = _game.hit.CreateInstance();
            hit.Volume = 0.01f;
            cbreak = _game.Cbreak.CreateInstance();
            cbreak.Volume = 0.01f;

            bar = _game.Content.Load<Texture2D>("Img/bar");

            this.Difficulty = difficulty;
            MediaPlayer.Play(_game.song);
            MediaPlayer.Volume = 0.5f;

            Notes FixedNotes = new Notes(_game, "Img/left", new Vector2(332, 530));
            fixedNotes.Add(FixedNotes);
            FixedNotes = new Notes(_game, "Img/down", new Vector2(487, 530));
            fixedNotes.Add(FixedNotes);
            FixedNotes = new Notes(_game, "Img/top", new Vector2(642, 530));
            fixedNotes.Add(FixedNotes);
            FixedNotes = new Notes(_game, "Img/right", new Vector2(797, 530));
            fixedNotes.Add(FixedNotes);

            FixedNotes = new Notes(_game, "Img/right", new Vector2(-500, -500)); //empty note for when no key is being pressed
            fixedNotes.Add(FixedNotes);

            FixedNotes = new Notes(_game, "Img/pressedLeft", new Vector2(-500, -500)); // pressed keys
            fixedNotes.Add(FixedNotes);
            FixedNotes = new Notes(_game, "Img/pressedDown", new Vector2(-500, -500));
            fixedNotes.Add(FixedNotes);
            FixedNotes = new Notes(_game, "Img/pressedTop", new Vector2(-500, -500));
            fixedNotes.Add(FixedNotes);
            FixedNotes = new Notes(_game, "Img/pressedRight", new Vector2(-500, -500));
            fixedNotes.Add(FixedNotes);

            FixedNotes = new Notes(_game, "Img/pressedRight", new Vector2(-500, -500));
            fixedNotes.Add(FixedNotes);

            Notes FallingNotes;
            int j = 0, k = 100;
            for (int i = 0; i < 135; i++)
            {
                Random rand = new Random();
                int number = rand.Next(1, 4);
                switch (number)
                {
                    case 1:
                        j++;
                        FallingNotes = new Notes(_game, "Img/fallingLeft", new Vector2(332, -j * 600 + k));
                        fallingNotes.Add(j, FallingNotes);
                        break;

                    case 2:
                        j++;
                        FallingNotes = new Notes(_game, "Img/fallingDown", new Vector2(487, -j * 600 + k));
                        fallingNotes.Add(j, FallingNotes);
                        break;

                    case 3:
                        j++;
                        FallingNotes = new Notes(_game, "Img/fallingTop", new Vector2(642, -j * 600 + k));
                        fallingNotes.Add(j, FallingNotes);
                        break;
                    case 4:
                        j++;
                        FallingNotes = new Notes(_game, "Img/fallingRight", new Vector2(797, -j * 600 + k));
                        fallingNotes.Add(j, FallingNotes);
                        break;

                }
            }

            Strings ScoreStrings = new Strings(_game, _game.Arial, "300", new Vector2(0, -500), Color.Blue);
            scoreStrings.Add(ScoreStrings);
            ScoreStrings = new Strings(_game, _game.Arial, "200", new Vector2(0, -500), Color.Green);
            scoreStrings.Add(ScoreStrings);
            ScoreStrings = new Strings(_game, _game.Arial, "100", new Vector2(0, -500), Color.Yellow);
            scoreStrings.Add(ScoreStrings);
            ScoreStrings = new Strings(_game, _game.Arial, "  X", new Vector2(0, -500), Color.Red);
            scoreStrings.Add(ScoreStrings);
            ScoreStrings = new Strings(_game, _game.Arial, " ", new Vector2(0, -500), Color.Red);
            scoreStrings.Add(ScoreStrings);
            ScoreStrings = new Strings(_game, _game.Arial, "0", new Vector2(50, 50), Color.Purple); // Global Score
            scoreStrings.Add(ScoreStrings);
            ScoreStrings = new Strings(_game, _game.Arial, "0X", new Vector2(50, 620), Color.Purple); // Combo Counter
            scoreStrings.Add(ScoreStrings);
            ScoreStrings = new Strings(_game, _game.Arial, "100 HP", new Vector2(1000, 50), Color.Purple); // HP Counter
            scoreStrings.Add(ScoreStrings);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(bar, new Vector2(300, 35), Color.White);
            spriteBatch.Draw(bar, new Vector2(455, 35), Color.White);
            spriteBatch.Draw(bar, new Vector2(610, 35), Color.White);
            spriteBatch.Draw(bar, new Vector2(765, 35), Color.White);
            spriteBatch.Draw(bar, new Vector2(920, 35), Color.White);

            foreach (KeyValuePair<int, Notes> note in fallingNotes)
            {
                note.Value.drawNote(spriteBatch);
            }
            foreach (Notes note in fixedNotes)
            {
                note.drawNote(spriteBatch);
            }
            foreach (Strings String in scoreStrings)
            {
                String.drawString(spriteBatch);
            }

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                RestartGame();
            }

            int i = 1, j = 4, k = 4;
            foreach (KeyValuePair<int, Notes> note in fallingNotes)
            {
                fallingNotes[i].SetNotePosition(new Vector2(fallingNotes[i].GetNotePosition().X, fallingNotes[i].GetNotePosition().Y + Difficulty * 300 * (float)gameTime.ElapsedGameTime.TotalSeconds));
                i++;
            }
            if (fallingNotes[m].GetNotePosition().Y > 840.00f && m < 135)
            {
                if (fallingNotes[m].GetPressedNote() == false)
                {
                    if (Combo > 10)
                        cbreak.Play();
                    Combo = 0;
                    HP -= 10;

                    switch (fallingNotes[m].GetNoteOrientation())
                    {
                        case "Img/fallingLeft":
                            i = 0;
                            break;
                        case "Img/fallingDown":
                            i = 1;
                            break;
                        case "Img/fallingTop":
                            i = 2;
                            break;
                        case "Img/fallingRight":
                            i = 3;
                            break;
                    }

                    scoreStrings[3].SetStringPosition(new Vector2(fixedNotes[i].GetNotePosition().X, fixedNotes[i].GetNotePosition().Y - 230));

                }
                m++;
            }
            if (m == 135 && fallingNotes[m].GetNotePosition().Y > 1280.00f)
            {
                _game.scores.Add(Score);
                _game.ChangeState(new Scoreboard(_game, _graphicsDevice, _content));
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
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                k = 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
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
                    hit.Play();

                }
                else if (Distance < 75)
                {
                    Combo++;
                    HP += 5;
                    j = 1;
                    Score += 200 * Combo;
                    fallingNotes[m].SetPressedNote(true);
                    hit.Play();
                }
                else if (Distance < 150)
                {
                    Combo++;
                    HP -= 5;
                    j = 2;
                    Score += 100 * Combo;
                    fallingNotes[m].SetPressedNote(true);
                    hit.Play();
                }
                else if (Distance < 300)
                {
                    HP -= 10;
                    j = 3;
                    fallingNotes[m].SetPressedNote(true);
                    if (Combo > 10)
                        cbreak.Play();
                    Combo = 0;
                }
                else
                    j = 4;
                scoreStrings[j].SetStringPosition(new Vector2(fixedNotes[k].GetNotePosition().X, fixedNotes[k].GetNotePosition().Y - 230));
            }
            if (HP > 100)
                HP = 100;
            if (HP < 0)
                HP = 0;

            scoreStrings[5].SetStringText(Score.ToString());
            scoreStrings[6].SetStringText(Combo + "X");
            scoreStrings[7].SetStringText(HP + " HP");

            if (HP == 0)
            {
                MediaPlayer.Stop();
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
            }
        }

        public void RestartGame()
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, Difficulty));
        }
    }
}
