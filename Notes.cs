using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheGame{
    public class Notes
    {
        Texture2D texture;
        string Orientation;
        Vector2 position;

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

        public void drawNote(SpriteBatch sb)
        {
            sb.Draw(this.texture, //texture
                this.position, //position
                Color.White// color
                );
        }
    }
}