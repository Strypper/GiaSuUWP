using Microsoft.Toolkit.Uwp.UI.Animations.Expressions;
using System;
using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gia_Sư
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EasterEgg : Page
    {
        private Compositor compositor = Window.Current.Compositor;
        private Visual backvisual;
        private ScalarKeyFrameAnimation rotate;
        public EasterEgg()
        {
            this.InitializeComponent();

            backvisual = ElementCompositionPreview.GetElementVisual(Gear);
            ElementCompositionPreview.SetIsTranslationEnabled(Gear, true);
            backvisual.Size = new Vector2(700, 700);
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
