﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Platform;
using Piously.Game.Configuration;
using Piously.Game.Graphics.Containers;
using Piously.Game.Graphics.UserInterface;
using osuTK.Graphics;

namespace Piously.Game.Overlays.Settings.Sections.Graphics
{
    public class LayoutSettings : SettingsSubsection
    {
        protected override string Header => "Layout";

        private FillFlowContainer<SettingsSlider<float>> scalingSettings;

        private Bindable<ScalingMode> scalingMode;
        private Bindable<Size> sizeFullscreen;
        private readonly IBindableList<WindowMode> windowModes = new BindableList<WindowMode>();

        [Resolved]
        private PiouslyGameBase game { get; set; }

        private SettingsDropdown<Size> resolutionDropdown;
        private SettingsDropdown<WindowMode> windowModeDropdown;

        private Bindable<float> scalingPositionX;
        private Bindable<float> scalingPositionY;
        private Bindable<float> scalingSizeX;
        private Bindable<float> scalingSizeY;

        private const int transition_duration = 400;

        [BackgroundDependencyLoader]
        private void load(FrameworkConfigManager config, PiouslyConfigManager piouslyConfig, GameHost host)
        {
            scalingMode = piouslyConfig.GetBindable<ScalingMode>(PiouslySetting.Scaling);
            sizeFullscreen = config.GetBindable<Size>(FrameworkSetting.SizeFullscreen);
            scalingSizeX = piouslyConfig.GetBindable<float>(PiouslySetting.ScalingSizeX);
            scalingSizeY = piouslyConfig.GetBindable<float>(PiouslySetting.ScalingSizeY);
            scalingPositionX = piouslyConfig.GetBindable<float>(PiouslySetting.ScalingPositionX);
            scalingPositionY = piouslyConfig.GetBindable<float>(PiouslySetting.ScalingPositionY);

            if (host.Window != null)
                windowModes.BindTo(host.Window.SupportedWindowModes);

            Container resolutionSettingsContainer;

            Children = new Drawable[]
            {
                windowModeDropdown = new SettingsDropdown<WindowMode>
                {
                    LabelText = "Screen mode",
                    Current = config.GetBindable<WindowMode>(FrameworkSetting.WindowMode),
                    ItemSource = windowModes,
                },
                resolutionSettingsContainer = new Container
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y
                },
                new SettingsSlider<float, UIScaleSlider>
                {
                    LabelText = "UI Scaling",
                    TransferValueOnCommit = true,
                    Current = piouslyConfig.GetBindable<float>(PiouslySetting.UIScale),
                    KeyboardStep = 0.01f,
                    Keywords = new[] { "scale", "letterbox" },
                },
                //TO BE FIXED
                /*new SettingsEnumDropdown<ScalingMode>
                {
                    LabelText = "Screen Scaling",
                    Current = piouslyConfig.GetBindable<ScalingMode>(PiouslySetting.Scaling),
                    Keywords = new[] { "scale", "letterbox" },
                },*/
                scalingSettings = new FillFlowContainer<SettingsSlider<float>>
                {
                    Direction = osu.Framework.Graphics.Containers.FillDirection.Vertical,
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    AutoSizeDuration = transition_duration,
                    AutoSizeEasing = Easing.OutQuint,
                    Masking = true,
                    Children = new[]
                    {
                        new SettingsSlider<float>
                        {
                            LabelText = "Horizontal position",
                            Current = scalingPositionX,
                            KeyboardStep = 0.01f,
                            DisplayAsPercentage = true
                        },
                        new SettingsSlider<float>
                        {
                            LabelText = "Vertical position",
                            Current = scalingPositionY,
                            KeyboardStep = 0.01f,
                            DisplayAsPercentage = true
                        },
                        new SettingsSlider<float>
                        {
                            LabelText = "Horizontal scale",
                            Current = scalingSizeX,
                            KeyboardStep = 0.01f,
                            DisplayAsPercentage = true
                        },
                        new SettingsSlider<float>
                        {
                            LabelText = "Vertical scale",
                            Current = scalingSizeY,
                            KeyboardStep = 0.01f,
                            DisplayAsPercentage = true
                        },
                    }
                },
            };

            scalingSettings.ForEach(s => bindPreviewEvent(s.Current));

            var resolutions = getResolutions();

            if (resolutions.Count > 1)
            {
                resolutionSettingsContainer.Child = resolutionDropdown = new ResolutionSettingsDropdown
                {
                    LabelText = "Resolution",
                    ShowsDefaultIndicator = false,
                    Items = resolutions,
                    Current = sizeFullscreen
                };

                windowModeDropdown.Current.BindValueChanged(mode =>
                {
                    if (mode.NewValue == WindowMode.Fullscreen)
                    {
                        resolutionDropdown.Show();
                        sizeFullscreen.TriggerChange();
                    }
                    else
                        resolutionDropdown.Hide();
                }, true);
            }

            scalingMode.BindValueChanged(mode =>
            {
                scalingSettings.ClearTransforms();
                scalingSettings.AutoSizeAxes = mode.NewValue != ScalingMode.Off ? Axes.Y : Axes.None;

                if (mode.NewValue == ScalingMode.Off)
                    scalingSettings.ResizeHeightTo(0, transition_duration, Easing.OutQuint);

                scalingSettings.ForEach(s => s.TransferValueOnCommit = mode.NewValue == ScalingMode.Everything);
            }, true);

            windowModes.CollectionChanged += (sender, args) => windowModesChanged();

            windowModesChanged();
        }

        private void windowModesChanged()
        {
            if (windowModes.Count > 1)
                windowModeDropdown.Show();
            else
                windowModeDropdown.Hide();
        }

        /// <summary>
        /// Create a delayed bindable which only updates when a condition is met.
        /// </summary>
        /// <param name="bindable">The config bindable.</param>
        /// <returns>A bindable which will propagate updates with a delay.</returns>
        private void bindPreviewEvent(Bindable<float> bindable)
        {
            bindable.ValueChanged += _ =>
            {
                switch (scalingMode.Value)
                {
                    case ScalingMode.Gameplay:
                        showPreview();
                        break;
                }
            };
        }

        private Drawable preview;

        private void showPreview()
        {
            if (preview?.IsAlive != true)
                game.Add(preview = new ScalingPreview());

            preview.FadeOutFromOne(1500);
            preview.Expire();
        }

        private IReadOnlyList<Size> getResolutions()
        {
            var resolutions = new List<Size> { new Size(9999, 9999) };
            var currentDisplay = game.Window?.CurrentDisplayBindable.Value;

            if (currentDisplay != null)
            {
                resolutions.AddRange(currentDisplay.DisplayModes
                                                   .Where(m => m.Size.Width >= 800 && m.Size.Height >= 600)
                                                   .OrderByDescending(m => m.Size.Width)
                                                   .ThenByDescending(m => m.Size.Height)
                                                   .Select(m => m.Size)
                                                   .Distinct());
            }

            return resolutions;
        }

        private class ScalingPreview : ScalingContainer
        {
            public ScalingPreview()
            {
                Child = new Box
                {
                    Colour = Color4.White,
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0.5f,
                };
            }
        }

        private class UIScaleSlider : PiouslySliderBar<float>
        {
            public override string TooltipText => base.TooltipText + "x";
        }

        private class ResolutionSettingsDropdown : SettingsDropdown<Size>
        {
            protected override PiouslyDropdown<Size> CreateDropdown() => new ResolutionDropdownControl();

            private class ResolutionDropdownControl : DropdownControl
            {
                protected override string GenerateItemText(Size item)
                {
                    if (item == new Size(9999, 9999))
                        return "Default";

                    return $"{item.Width}x{item.Height}";
                }
            }
        }
    }
}
