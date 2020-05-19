﻿using Microsoft.Toolkit.Uwp.UI.Animations.Expressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Gia_Sư.Components
{
    public sealed partial class Logo : UserControl
    {
        private Compositor compositor = Window.Current.Compositor;
        private Visual backvisual;
        private ScalarKeyFrameAnimation rotate;
        public Logo()
        {
            this.InitializeComponent();

            backvisual = ElementCompositionPreview.GetElementVisual(Gear);
            ElementCompositionPreview.SetIsTranslationEnabled(Gear, true);
            backvisual.Size = new Vector2(40, 40);
            backvisual.CenterPoint = new Vector3(backvisual.Size / 2, 0);

            rotate = compositor.CreateScalarKeyFrameAnimation();

            var linear = compositor.CreateLinearEasingFunction();

            var startingValue = ExpressionValues.StartingValue.CreateScalarStartingValue();

            rotate.InsertExpressionKeyFrame(0.0f, startingValue);
            rotate.InsertExpressionKeyFrame(1.0f, startingValue + 360f, linear);
            rotate.Duration = TimeSpan.FromMilliseconds(1000);
            rotate.IterationBehavior = AnimationIterationBehavior.Forever;
            backvisual.StartAnimation(nameof(Visual.RotationAngleInDegrees), rotate);
        }
    }
}
