using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace Piously.Game.Graphics.Containers
{
    public class HexagonGroup : Container<HexagonalContainer>
    {
        //offset variable
        private Vector2 offset;

        public Vector2 Offset
        {
            get => offset;
            set
            {
                if (offset == value) return;

                offset = value;
            }
        }

        //2D array representing hexagons, the length of each dimension representing how many hexagons should exist.
        private HexagonalContainer[,] hexagons;

        public HexagonalContainer[,] Hexagons
        {
            get => hexagons;
            set
            {
                if (hexagons == value) return;

                hexagons = value;
            }
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AutoSizeAxes = Axes.Both;

            for(int i = 0; i < hexagons.GetLength(0); i++)
            {
                for(int j = 0; j < hexagons.GetLength(1); j++)
                {
                    if(hexagons[i,j] != null)
                    {
                        hexagons[i,j].Anchor = Anchor.TopLeft;
                        hexagons[i,j].Origin = Anchor.TopLeft;
                        hexagons[i,j].Position = new Vector2(0.98f * 0.75f * i * hexagons[i,j].Width, 0.98f * ((MathF.Sqrt(3) / 2) * ((j * hexagons[i,j].Width) - (0.5f * i * hexagons[i,j].Width))));
                        Add(hexagons[i,j]);
                    }
                }
            }
        }
    }
}
