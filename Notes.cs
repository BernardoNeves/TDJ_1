using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheGame
{
    public class Notes
    {
        Texture2D texture;
        string Orientation;
        Vector2 position;
        bool pressed = false;

        public Notes(Game1 game, string Orientation, Vector2 Position)
        {
            this.texture = game.Content.Load<Texture2D>(Orientation);
            this.Orientation = Orientation;
            this.position = Position;
        }

        public Texture2D GetNoteTexture()
        {
            return this.texture;
        }

        public Vector2 GetNotePosition()
        {
            return this.position;
        }

        public void SetNotePosition(Vector2 newPos)
        {
            this.position = newPos;
        }

        public string GetNoteOrientation()
        {
            return this.Orientation;
        }

        public bool GetPressedNote()
        {
            return this.pressed;
        }
        public void SetPressedNote(bool pressed)
        {
            this.pressed = pressed;
        }

        public void drawNote(SpriteBatch sb)
        {
            if (pressed == false)
            {
                sb.Draw(this.texture, //texture
                    this.position, //position
                    Color.White// color
                    );
            }
        }
    }
    public class Strings
    {
        Vector2 position;
        string text;
        SpriteFont font;
        Color color;

        public Strings(Game1 game, SpriteFont font, string text, Vector2 Position, Color color)
        {
            this.text = text;
            this.font = font;
            this.position = Position;
            this.color = color;
        }

        public Vector2 GetStringPosition()
        {
            return this.position;
        }

        public void SetStringPosition(Vector2 newPos)
        {
            this.position = newPos;
        }

        public void SetStringText(string text)
        {
            this.text = text;
        }
        public string GetStringText()
        {
            return this.text;
        }
        public void drawString(SpriteBatch sb)
        {
            sb.DrawString(this.font, //font
                this.text, //text
                this.position, //position
                this.color// color
                );
        }
    }
}