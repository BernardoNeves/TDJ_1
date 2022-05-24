using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Controls;

namespace TheGame.States
{
    public class MenuState : State
    {
        private List<Component> _components;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/SimpleButton");
            var playBtnTexture = _content.Load<Texture2D>("Controls/StartBtn");
            var buttonFont = _content.Load<SpriteFont>("Font/Font");

            var newEasyGameButton = new Button(playBtnTexture, buttonFont)
            {
                Position = new Vector2(480, 200),
                Text = "Easy",
            };

            newEasyGameButton.Click += NewEasyGameButton_Click;

            var newNormalGameButton = new Button(playBtnTexture, buttonFont)
            {
                Position = new Vector2(590, 200),
                Text = "Normal",
            };

            newNormalGameButton.Click += NewNormalGameButton_Click;

            var newHardGameButton = new Button(playBtnTexture, buttonFont)
            {
                Position = new Vector2(700, 200),
                Text = "Hard",
            };

            newHardGameButton.Click += NewHardGameButton_Click;

            var scoreBoardButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(490, 320),
                Text = "Scoreboard",
            };

            scoreBoardButton.Click += ScoreBoardButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(490, 440),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
      {
        newEasyGameButton,
        newNormalGameButton,
        newHardGameButton,
        scoreBoardButton,
        quitGameButton,
      };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void ScoreBoardButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new Scoreboard(_game, _graphicsDevice, _content));
        }

        private void NewEasyGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, 3));
        }

        private void NewNormalGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, 4));
        }

        private void NewHardGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, 5));
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
