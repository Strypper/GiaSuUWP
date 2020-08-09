using Microsoft.Toolkit.Uwp.UI.Animations.Expressions;
using System;
using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gia_Sư.Components.PopUps
{
    public sealed partial class Settings : ContentDialog
    {
        private Compositor compositor = Window.Current.Compositor;
        private Visual backvisual;
        private ScalarKeyFrameAnimation rotate;
        public Settings()
        {
            this.InitializeComponent();

            backvisual = ElementCompositionPreview.GetElementVisual(Gear);
            ElementCompositionPreview.SetIsTranslationEnabled(Gear, true);
            backvisual.Size = new Vector2(400, 400);
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

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
