using Gia_Sư.Models.Person;
using Gia_Sư.Pages.Tutor.CollegeSubject;
using Gia_Sư.Pages.UserInfoPages;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Toolkit.Uwp.UI.Animations.Expressions;
using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using EF = Microsoft.Toolkit.Uwp.UI.Animations.Expressions.ExpressionFunctions;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gia_Sư.Pages.Stuhub
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PersonalProfile : Page
    {
        CompositionPropertySet _props;
        CompositionPropertySet _scrollerPropertySet;
        Compositor _compositor;
        private SpriteVisual _blurredBackgroundImageVisual;
        private UserModel MyProfile = new UserModel();
        public PersonalProfile()
        {
            this.InitializeComponent();
            MyProfile = App.User;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            OverViewProfileFrame.Navigate(typeof(OverViewProfile));
            OverlayRectangle.Fill = new SolidColorBrush(new Windows.UI.Color() { A = 155, R = 78, B = 201, G = 159 });
            // Update the ZIndex of the header container so that the header is above the items when scrolling
            var headerPresenter = (UIElement)VisualTreeHelper.GetParent((UIElement)Header);
            var headerContainer = (UIElement)VisualTreeHelper.GetParent(headerPresenter);
            Canvas.SetZIndex((UIElement)headerContainer, 1);

            // Get the PropertySet that contains the scroll values from the ScrollViewer
            _scrollerPropertySet = ElementCompositionPreview.GetScrollViewerManipulationPropertySet(TheScrollView);
            _compositor = _scrollerPropertySet.Compositor;

            // Create a PropertySet that has values to be referenced in the ExpressionAnimations below
            _props = _compositor.CreatePropertySet();
            _props.InsertScalar("progress", 0);
            _props.InsertScalar("clampSize", 150);
            _props.InsertScalar("scaleFactor", 0.7f);

            // Get references to our property sets for use with ExpressionNodes
            var scrollingProperties = _scrollerPropertySet.GetSpecializedReference<ManipulationPropertySetReferenceNode>();
            var props = _props.GetReference();
            var progressNode = props.GetScalarProperty("progress");
            var clampSizeNode = props.GetScalarProperty("clampSize");
            var scaleFactorNode = props.GetScalarProperty("scaleFactor");

            // Create a blur effect to be animated based on scroll position
            var blurEffect = new GaussianBlurEffect()
            {
                Name = "blur",
                BlurAmount = 0.0f,
                BorderMode = EffectBorderMode.Hard,
                Optimization = EffectOptimization.Balanced,
                Source = new CompositionEffectSourceParameter("source")
            };

            var blurBrush = _compositor.CreateEffectFactory(
                blurEffect,
                new[] { "blur.BlurAmount" })
                .CreateBrush();

            blurBrush.SetSourceParameter("source", _compositor.CreateBackdropBrush());

            // Create a Visual for applying the blur effect
            _blurredBackgroundImageVisual = _compositor.CreateSpriteVisual();
            _blurredBackgroundImageVisual.Brush = blurBrush;
            _blurredBackgroundImageVisual.Size = new Vector2((float)OverlayRectangle.ActualWidth, (float)OverlayRectangle.ActualHeight);

            // Insert the blur visual at the right point in the Visual Tree
            ElementCompositionPreview.SetElementChildVisual(OverlayRectangle, _blurredBackgroundImageVisual);

            // Create and start an ExpressionAnimation to track scroll progress over the desired distance
            ExpressionNode progressAnimation = EF.Clamp(-scrollingProperties.Translation.Y / clampSizeNode, 0, 1);
            _props.StartAnimation("progress", progressAnimation);

            // Create and start an ExpressionAnimation to animate blur radius between 0 and 15 based on progress
            ExpressionNode blurAnimation = EF.Lerp(0, 15, progressNode);
            _blurredBackgroundImageVisual.Brush.Properties.StartAnimation("blur.BlurAmount", blurAnimation);

            // Get the backing visual for the header so that its properties can be animated
            Visual headerVisual = ElementCompositionPreview.GetElementVisual(Header);

            // Create and start an ExpressionAnimation to clamp the header's offset to keep it onscreen
            ExpressionNode headerTranslationAnimation = EF.Conditional(progressNode < 1, 0, -scrollingProperties.Translation.Y - clampSizeNode);
            headerVisual.StartAnimation("Offset.Y", headerTranslationAnimation);

            // Create and start an ExpressionAnimation to scale the header during overpan
            ExpressionNode headerScaleAnimation = EF.Lerp(1, 1.25f, EF.Clamp(scrollingProperties.Translation.Y / 50, 0, 1));
            headerVisual.StartAnimation("Scale.X", headerScaleAnimation);
            headerVisual.StartAnimation("Scale.Y", headerScaleAnimation);

            //Set the header's CenterPoint to ensure the overpan scale looks as desired
            headerVisual.CenterPoint = new Vector3((float)(Header.ActualWidth / 2), (float)Header.ActualHeight, 0);

            // Get the backing visual for the photo in the header so that its properties can be animated
            Visual photoVisual = ElementCompositionPreview.GetElementVisual(BackgroundRectangle);

            // Create and start an ExpressionAnimation to opacity fade out the image behind the header
            ExpressionNode imageOpacityAnimation = 1 - progressNode;
            photoVisual.StartAnimation("opacity", imageOpacityAnimation);

            // Get the backing visual for the profile picture visual so that its properties can be animated
            Visual profileVisual = ElementCompositionPreview.GetElementVisual(ProfileImage);

            // Create and start an ExpressionAnimation to scale the profile image with scroll position
            ExpressionNode scaleAnimation = EF.Lerp(1, scaleFactorNode, progressNode);
            profileVisual.StartAnimation("Scale.X", scaleAnimation);
            profileVisual.StartAnimation("Scale.Y", scaleAnimation);

            // Get backing visuals for the text blocks so that their properties can be animated
            Visual subtitleVisual = ElementCompositionPreview.GetElementVisual(Role);
            Visual blurbVisual = ElementCompositionPreview.GetElementVisual(Life);
            Visual teachVisual = ElementCompositionPreview.GetElementVisual(TeachCountLayout);
            Visual subjectcountVisual = ElementCompositionPreview.GetElementVisual(SubjectCountLayout);
            Visual ratingcountVisual = ElementCompositionPreview.GetElementVisual(RatingLayout);
            Visual ratingVisual = ElementCompositionPreview.GetElementVisual(Rating);

            // Create an ExpressionAnimation that moves between 1 and 0 with scroll progress, to be used for text block opacity
            ExpressionNode textOpacityAnimation = EF.Clamp(1 - (progressNode * 2), 0, 1);

            // Start opacity and scale animations on the text block visuals
            blurbVisual.StartAnimation("Opacity", textOpacityAnimation);
            blurbVisual.StartAnimation("Scale.X", scaleAnimation);
            blurbVisual.StartAnimation("Scale.Y", scaleAnimation);

            subtitleVisual.StartAnimation("Opacity", textOpacityAnimation);
            subtitleVisual.StartAnimation("Scale.X", scaleAnimation);
            subtitleVisual.StartAnimation("Scale.Y", scaleAnimation);

            ratingVisual.StartAnimation("Opacity", textOpacityAnimation);
            ratingVisual.StartAnimation("Scale.X", scaleAnimation);
            ratingVisual.StartAnimation("Scale.Y", scaleAnimation);

            ratingcountVisual.StartAnimation("Opacity", textOpacityAnimation);
            ratingcountVisual.StartAnimation("Scale.X", scaleAnimation);
            ratingcountVisual.StartAnimation("Scale.Y", scaleAnimation);

            teachVisual.StartAnimation("Opacity", textOpacityAnimation);
            teachVisual.StartAnimation("Scale.X", scaleAnimation);
            teachVisual.StartAnimation("Scale.Y", scaleAnimation);

            subjectcountVisual.StartAnimation("Opacity", textOpacityAnimation);
            subjectcountVisual.StartAnimation("Scale.X", scaleAnimation);
            subjectcountVisual.StartAnimation("Scale.Y", scaleAnimation);

            // Get the backing visuals for the text and button containers so that their properites can be animated
            Visual textVisual = ElementCompositionPreview.GetElementVisual(TextContainer);
            Visual buttonVisual = ElementCompositionPreview.GetElementVisual(ButtonPanel);

            // When the header stops scrolling it is 150 pixels offscreen.  We want the text header to end up with 50 pixels of its content
            // offscreen which means it needs to go from offset 0 to 100 as we traverse through the scrollable region
            ExpressionNode contentOffsetAnimation = progressNode * 100;
            textVisual.StartAnimation("Offset.Y", contentOffsetAnimation);

            ExpressionNode buttonOffsetAnimation = progressNode * -100;
            buttonVisual.StartAnimation("Offset.Y", buttonOffsetAnimation);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardConnectedAnimation");
            if (anim != null)
            {
                anim.TryStart(ProfileImage);
            }
        }
        private void ReturnHomeRoot_Click(object sender, RoutedEventArgs e)
        {
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("ForwardConnectedAnimation", ProfileImage);
            Frame.GoBack();
        }

        private void CreateSubject_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CollegeRequestSubject));
        }
    }
}
