﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Ruminate.GUI.Framework;
using Ruminate.GUI.Content;

namespace SpaceIsFun
{
    public partial class Game1 : Game
    {
        List<Vector2> starNodes = new List<Vector2>();
        List<Drawable> starNodeDraws = new List<Drawable>();
        int starNodeSelectedIndex = 0;
        Vector2 starNodeSelected;
        Drawable overworldCursorDraw;
        Vector2 cursorCoords = new Vector2();          //Both the cursor coordinates and the selected node

        void setupOverworld()
        {
            
            overworld.enter += () =>
            {
                // setup gui elements here

                // for now, these probably will just be buttons arranged on a map to spawn certain battles / narrative events

                //

                setNodes();

                foreach (Vector2 item in starNodes) {
                    starNodeDraws.Add(new Drawable(starTexture, item));
                }
            };

            overworld.update += (GameTime gameTime) =>
            {
                #region input handling

                if (currentKeyState.IsKeyUp(Keys.Up) && previousKeyState.IsKeyDown(Keys.Up))
                {
                    traverseStarsUp();
                }
                else if (currentKeyState.IsKeyUp(Keys.Down) && previousKeyState.IsKeyDown(Keys.Down))
                {
                    traverseStarsDown();
                }
                else if (currentKeyState.IsKeyUp(Keys.Left) && previousKeyState.IsKeyDown(Keys.Left))
                {
                    traverseStarsLeft();
                }
                else if (currentKeyState.IsKeyUp(Keys.Right) && previousKeyState.IsKeyDown(Keys.Right))
                {
                    traverseStarsRight();
                }

                if (currentKeyState.IsKeyUp(Keys.R) && previousKeyState.IsKeyDown(Keys.R))
                {
                    setNodes();
                }


                //If user hits down, traverseStarsLeft
                //If user hits right, traverseStarsRight

                #endregion
               
               //This is where it would draw if it weren't dumb

                cursorCoords = starNodes[starNodeSelectedIndex];
                overworldCursorDraw = new Drawable(overworldCursorTexture, cursorCoords);
               

            };

            overworld.leave += () =>
            {
                // remove gui elements here
            };
        }

        /// <summary>
        /// Create a list of vector2's.
        /// Randomly create coordinates in between 0 and screen width/height
        /// </summary>
        private void setNodes()
        {
            Random rand = new Random();
            
            starNodes.Add(new Vector2(rand.Next(50, graphics.PreferredBackBufferWidth), rand.Next(100, graphics.PreferredBackBufferHeight - 100)));
            starNodes.Add(new Vector2(rand.Next(50, graphics.PreferredBackBufferWidth), rand.Next(100, graphics.PreferredBackBufferHeight - 100)));
            starNodes.Add(new Vector2(rand.Next(50, graphics.PreferredBackBufferWidth), rand.Next(100, graphics.PreferredBackBufferHeight - 100)));
            starNodes.Add(new Vector2(rand.Next(50, graphics.PreferredBackBufferWidth), rand.Next(100, graphics.PreferredBackBufferHeight - 100)));

        }

        private void traverseStarsUp()
        {
            //Cut star map in vertical 'halves' 
            List<Vector2> mapHalf = new List<Vector2>();
            foreach (var item in starNodes)
            {
                if (item != cursorCoords)
                {
                    if (item.Y < cursorCoords.Y)
                    {
                        mapHalf.Add(item);
                    }
                }
            }
            //Calculate cartesian distance between selected node and every other node left
            List<double> distances = new List<double>();
            foreach (var item in mapHalf)
            {
                double distance;
                float xDistance = item.X - cursorCoords.X;
                float yDistance = item.Y - cursorCoords.Y;
                distance = Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));
                distances.Add(distance);

            }

            //Whichever has the lowest cartesian distance is the one we reassign the select to
            //Recalc all distances, if the distance is equal to the min distance in distances, the set cursor coords to it.
            distances.Sort();
            Vector2 temp = new Vector2();
            foreach (var item in mapHalf)
            {
                double distance;
                float xDistance = item.X - cursorCoords.X;
                float yDistance = item.Y - cursorCoords.Y;
                distance = Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));

                if (distances[0] == distance)
                {
                    temp = item;
                }
            }

            for (int i = 0; i < starNodes.Count; i++)
            {
                if (temp == starNodes[i])
                {
                    starNodeSelectedIndex = i;
                }
            }
        }



        private void traverseStarsDown()
        {
            //Cut star map in vertical 'halves' 
            List<Vector2> mapHalf = new List<Vector2>();
            foreach (var item in starNodes)
            {
                if (item != cursorCoords)
                {
                    if (item.Y > cursorCoords.Y)
                    {
                        mapHalf.Add(item);
                    }
                }
            }
            //Calculate cartesian distance between selected node and every other node left
            List<double> distances = new List<double>();
            foreach (var item in mapHalf)
            {
                double distance;
                float xDistance = item.X - cursorCoords.X;
                float yDistance = item.Y - cursorCoords.Y;
                distance = Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));
                distances.Add(distance);

            }

            //Whichever has the lowest cartesian distance is the one we reassign the select to
            //Recalc all distances, if the distance is equal to the min distance in distances, the set cursor coords to it.
            distances.Sort();
            Vector2 temp = new Vector2();
            foreach (var item in mapHalf)
            {
                double distance;
                float xDistance = item.X - cursorCoords.X;
                float yDistance = item.Y - cursorCoords.Y;
                distance = Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));

                if (distances[0] == distance)
                {
                    temp = item;
                }
            }

            for (int i = 0; i < starNodes.Count; i++)
            {
                if (temp == starNodes[i])
                {
                    starNodeSelectedIndex = i;
                }
            }
        }



        private void traverseStarsLeft()
        {
            //Cut star map in horizontal 'halves' 
            List<Vector2> mapHalf = new List<Vector2>();
            foreach (var item in starNodes)
            {
                if (item != cursorCoords)
                {
                    if (item.X < cursorCoords.X)
                    {
                        mapHalf.Add(item);
                    }
                }
            }
            //Calculate cartesian distance between selected node and every other node left
            List<double> distances = new List<double>();
            foreach (var item in mapHalf)
            {
                double distance;
                float xDistance = item.X - cursorCoords.X;
                float yDistance = item.Y - cursorCoords.Y;
                distance = Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));
                distances.Add(distance);

            }

            //Whichever has the lowest cartesian distance is the one we reassign the select to
            //Recalc all distances, if the distance is equal to the min distance in distances, the set cursor coords to it.
            distances.Sort();
            Vector2 temp = new Vector2();
            foreach (var item in mapHalf)
            {
                double distance;
                float xDistance = item.X - cursorCoords.X;
                float yDistance = item.Y - cursorCoords.Y;
                distance = Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));

                if (distances[0] == distance)
                {
                    temp = item;
                }
            }

            for (int i = 0; i < starNodes.Count; i++)
            {
                if (temp == starNodes[i])
                {
                    starNodeSelectedIndex = i;
                }
            }
        }

        private void traverseStarsRight()
        {
            //Cut star map in vertical 'halves' 
            List<Vector2> mapHalf = new List<Vector2>();
            foreach (var item in starNodes)
            {
                if (item != cursorCoords)
                {
                    if (item.X > cursorCoords.X)
                    {
                        mapHalf.Add(item);
                    }
                }
            }
            //Calculate cartesian distance between selected node and every other node left
            List<double> distances = new List<double>();
            foreach (var item in mapHalf)
            {
                double distance;
                float xDistance = item.X - cursorCoords.X;
                float yDistance = item.Y - cursorCoords.Y;
                distance = Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));
                distances.Add(distance);

            }

            //Whichever has the lowest cartesian distance is the one we reassign the select to
            //Recalc all distances, if the distance is equal to the min distance in distances, the set cursor coords to it.
            distances.Sort();
            Vector2 temp = new Vector2();
            foreach (var item in mapHalf)
            {
                double distance;
                float xDistance = item.X - cursorCoords.X;
                float yDistance = item.Y - cursorCoords.Y;
                distance = Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));

                if (distances[0] == distance)
                {
                    temp = item;
                }
            }

            for (int i = 0; i < starNodes.Count; i++)
            {
                if (temp == starNodes[i])
                {
                    starNodeSelectedIndex = i;
                }
            }
        }
    }
}
