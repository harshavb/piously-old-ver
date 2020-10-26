using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using Piously.Game.Overlays;

namespace Piously.Game.Users.Drawables
{
    public class UpdateableFlag : ModelBackedDrawable<Country>
    {
        public Country Country
        {
            get => Model;
            set => Model = value;
        }

        /// <summary>
        /// Whether to show a place holder on null country.
        /// </summary>
        public bool ShowPlaceholderOnNull = true;

        public UpdateableFlag(Country country = null)
        {
            Country = country;
        }

        protected override Drawable CreateDrawable(Country country)
        {
            if (country == null && !ShowPlaceholderOnNull)
                return null;

            return new DrawableFlag(country)
            {
                RelativeSizeAxes = Axes.Both,
            };
        }

        //TO BE IMPLEMENTED
        //[Resolved(canBeNull: true)]
        //private RankingsOverlay rankingsOverlay { get; set; }

        protected override bool OnClick(ClickEvent e)
        {
            //rankingsOverlay?.ShowCountry(Country);
            return true;
        }
    }
}
