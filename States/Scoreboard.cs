using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Controls;

namespace TheGame.States
{
    public class Scoreboard : State
    {
        Component backBtn;

        List<Strings> scoreBoard = new List<Strings>();

        public Scoreboard(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/SimpleButton");
            var buttonFont = _content.Load<SpriteFont>("Font/Font");

            var backButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(490, 600),
                Text = "Back",
            };

            backButton.Click += GoBackButton_Click;

            backBtn = backButton;

            _game.scores.Sort();

            Strings ScoreStrings = new Strings(_game, _game.Arial, "ScoreBoard", new Vector2(490, 100), Color.Black); //scoreboard
            scoreBoard.Add(ScoreStrings);
            ScoreStrings = new Strings(_game, _game.Arial, _game.scores[_game.scores.Count - 1].ToString(), new Vector2(490, 200), Color.White); //scoreboard
            scoreBoard.Add(ScoreStrings);
            ScoreStrings = new Strings(_game, _game.Arial, _game.scores[_game.scores.Count - 2].ToString(), new Vector2(490, 260), Color.White); //scoreboard
            scoreBoard.Add(ScoreStrings);
            ScoreStrings = new Strings(_game, _game.Arial, _game.scores[_game.scores.Count - 3].ToString(), new Vector2(490, 320), Color.White); //scoreboard
            scoreBoard.Add(ScoreStrings);
            ScoreStrings = new Strings(_game, _game.Arial, _game.scores[_game.scores.Count - 4].ToString(), new Vector2(490, 380), Color.White); //scoreboard
            scoreBoard.Add(ScoreStrings);
            ScoreStrings = new Strings(_game, _game.Arial, _game.scores[_game.scores.Count - 5].ToString(), new Vector2(490, 440), Color.White); //scoreboard
            scoreBoard.Add(ScoreStrings);
        }

        private void GoBackButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            backBtn.Draw(gameTime, spriteBatch);

            foreach (Strings String in scoreBoard)
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
            backBtn.Update(gameTime);
        }
    }
}
