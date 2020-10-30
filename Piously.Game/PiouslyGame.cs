using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;
using LogLevel = osu.Framework.Logging.LogLevel;

namespace Piously.Game
{
    //The actual game, specifically loads the UI
    public class PiouslyGame : PiouslyGameBase
    {
        private Box box;

        [BackgroundDependencyLoader]
        private void load()
        {
            if (!Host.IsPrimaryInstance)
            {
                Logger.Log(@"Piously does not support multiple running instances.", LoggingTarget.Runtime, LogLevel.Error);
                Environment.Exit(0);
            }

            Add(box = new Box
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(150, 150),
                Colour = Color4.Tomato
            });

            Add(new SpriteText
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Text = "Testing font",
                Font = new FontUsage(family: "InkFree-Bold", size: 40f)
            });
        }

        protected override void Update()
        {
            base.Update();
            box.Rotation += (float)Time.Elapsed / 10;
        }
    }
}