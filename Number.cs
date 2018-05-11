using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DashNSlash.MacOS
{
    /// <summary>
    /// Number is a class that creates a numberline using recursion.
    /// 
    /// The score is displayed by lining up several sprites of numbers.
    /// </summary>
    public class Number : Sprite
    {
        int currentValue;
        int position;

        // The 'next' and 'previous' fields are for the adjacent numbers in the 
        // numberline.
        Number next;
        Number previous;

        public Number(int pos, float tileSize) : base("0", tileSize, new Vector2((tileSize) + (pos * tileSize), 0))
        {
            currentValue = 0;
            position = pos;
        }

        // <summary>
        // This method adds one to the end of the numberline, and uses recursion
        // to find out where to carry any numbers, and add new numbers to the
        // end of the numberline.
        // </summary>
        public bool AddOne(ContentManager content)
        {
            // if the number infront of you says you need to carry the one, do so.
            if (next != null)
            {
                if (!next.AddOne(content))
                    return false;
                // if your current value is nine, you'll have to carry the one
                if (currentValue == 9)
                {
                    // If there is no number to carry to, set value to 1 and 
                    // create a new number at the end, else, you'll need to set 
                    // yourself to 0.
                    if (previous == null)
                    {
                        currentValue = 1;
                        SetNext(content);
                    }
                    else
                    {
                        currentValue = 0;
                    }
                    UpdateTexture(content);
                    return true;
                }
                currentValue++;
                UpdateTexture(content);
                return false;
            }
            // If the value isn't nine, we won't have to carry the one.
            if (currentValue != 9)
            {
                currentValue++;
                UpdateTexture(content);
                return false;
            }
            // If there is no number to carry the one to, add to the numberline.
            if (previous == null)
            {
                SetNext( content); 
                currentValue = 1;
                UpdateTexture(content);
                return true;
            }

            // If there is, add a number to it.
            currentValue = 0;
            UpdateTexture(content);
            return true;
        }

        // <summary>
        // This is a helper method for setting a new Number to the numberline.
        // </summary>
        void SetNext(ContentManager content) 
        {
            if (next == null)
            {
				next = new Number(position + 1, tileSize)
				{
					previous = this
				};
				next.LoadContent(content);
            }
            else
            {
                next.SetNext(content);
            }
        }

        void UpdateTexture(ContentManager content)
        {
            name = currentValue.ToString();
            LoadContent(content);
        }

        public void DrawNumberline(SpriteBatch spriteBatch)
        {
            if (next != null)
                next.DrawNumberline(spriteBatch);
            
            Draw(spriteBatch);
        }

        public String AllNames()
        {
            if (next != null)
			{
				return name + ", " + next.AllNames();
			}
            return name;
        }

		public int CurrentValue { get; }
    }
}
