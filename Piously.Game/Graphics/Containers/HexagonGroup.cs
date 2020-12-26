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
        private bool[][] hexagons;

        public bool[][] Hexagons
        {
            get => hexagons;
            set
            {
                if (hexagons == value) return;

                hexagons = value;
            }
        }

        private float hexagonHeight;

        public float HexagonHeight
        {
            get => hexagonHeight;
            set
            {
                if (hexagonHeight == value) return;

                hexagonHeight = value;
            }
        }

        //We want the amount of hexagons to not affect the size of the total HexagonGroup.
        //Hexagons will be oriented with a flat side on the bottom by default.

        [BackgroundDependencyLoader]
        private void load()
        {
            AutoSizeAxes = Axes.Both;

            for(int i = 0; i < hexagons.Length; i++)
            {
                for(int j = 0; j < hexagons.Length; j++)
                {
                    if(hexagons[i][j])
                    {
                        Add(new HexagonalContainer
                        {
                            Size = new Vector2(hexagonHeight),
                            Anchor = Anchor.TopLeft,
                            Origin = Anchor.TopLeft,
                            Position = new Vector2(j * 3 * MathF.Sqrt(3) / 8, hexagonHeight * i - (0.5f * hexagonHeight * j)),
                        });
                    }
                }
            }
        }
    }
}
